using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using Newtonsoft.Json;
using System.Text;

public class Please : MonoBehaviour
{
    public TMP_InputField userInputField;
    public TextMeshProUGUI npcResponseText;
    private string apiUrl = "http://127.0.0.1:5000/npc_interaction";

    public void OnSubmitButtonClicked()
    {
        string userInput = userInputField.text;

        // 사용자 입력을 서버에 보내기 위한 요청
        StartCoroutine(SendUserInputToServer(userInput));
    }

    private IEnumerator SendUserInputToServer(string userInput)
    {
        var jsonData = new { user_input = userInput };
        string json = JsonConvert.SerializeObject(jsonData);

        using (UnityWebRequest www = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // 서버 요청 보내기
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseBody = www.downloadHandler.text;
                var npcResponse = JsonConvert.DeserializeObject<NPCResponse>(responseBody);
                npcResponseText.text = npcResponse.npc_answer;
            }
            else
            {
                Debug.LogError("Request failed: " + www.error);
                Debug.LogError("Response body: " + www.downloadHandler.text);
            }
        }
    }


    [System.Serializable]
    public class NPCResponse
    {
        public string npc_answer;
    }
}
