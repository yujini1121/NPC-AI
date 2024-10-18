using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            try
            {
                UnityEngine.Debug.Log("Try");

                Process psi = new Process();
                psi.StartInfo.FileName = "C:/Users/User/pythonProject/dist/main/main.exe";
                psi.StartInfo.Arguments = "C:/Users/User/pythonProject/main.py";
                psi.StartInfo.CreateNoWindow = true;
                psi.StartInfo.UseShellExecute = false;
                psi.StartInfo.RedirectStandardOutput = true;
                psi.StartInfo.RedirectStandardError = true;
                psi.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                psi.StartInfo.StandardErrorEncoding = Encoding.UTF8;

                // 비동기 출력 처리
                psi.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        // 시스템 메시지 필터링
                        if (!e.Data.Contains("�ƹ� Ű�� �����ʽÿ�")) // 특정 문자열 필터링
                        {
                            UnityEngine.Debug.Log("Output: " + e.Data);
                        }
                    }
                };

                psi.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        UnityEngine.Debug.LogError("Error: " + e.Data);
                    }
                };

                psi.Start(); // 프로세스 시작
                psi.BeginOutputReadLine(); // 비동기 읽기 시작
                psi.BeginErrorReadLine();

                // 종료 이벤트 처리
                psi.EnableRaisingEvents = true;
                psi.Exited += (sender, e) =>
                {
                    UnityEngine.Debug.Log("Process exited with code: " + psi.ExitCode);
                    psi.Dispose(); // 종료 후 프로세스 자원 해제
                };
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("Catch");
                UnityEngine.Debug.LogError("Unable to launch app: " + e.Message);
            }
        }
    }
}
