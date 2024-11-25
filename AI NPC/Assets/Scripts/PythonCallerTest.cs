using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PythonCallerTest : MonoBehaviour
{
    public const bool I_HAVE_PYTHON_EXE = true;

    private System.Diagnostics.Process pythonProcess;
    private System.IO.StreamWriter pythonInput;
    private System.IO.StreamReader pythonOutput;
    private System.Threading.CancellationTokenSource cancellationTokenSource;



    public static string GetPath(string fileName)
        => $"Assets/PythonFiles/{fileName}.py";


    // Start is called before the first frame update
    void Start()
    {
        StartPythonProcess();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SendInputToPython(1, 2); // 예시 입력
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FinishPython(); // 프로세스 종료
        }
    }

    void OnDestroy()
    {
        FinishPython();
    }

    // Python 프로세스를 시작
    private void StartPythonProcess()
    {
        if (pythonProcess != null)
        {
            Debug.LogWarning("Python process is already running.");
            return;
        }

        pythonProcess = new System.Diagnostics.Process()
        {
            StartInfo = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = "python", // 이건 무슨 프로그램이냐?
                Arguments = GetPath("PythonAsyncTest"), // 파이썬 파일 이름
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };

        pythonProcess.Start();
        pythonInput = pythonProcess.StandardInput;
        pythonOutput = pythonProcess.StandardOutput;
        cancellationTokenSource = new System.Threading.CancellationTokenSource();
        Task.Run(() => M_ReadPythonOutputAsync(cancellationTokenSource.Token));
    }

    // Python 프로세스에서 출력 읽기 (비동기)
    private async Task M_ReadPythonOutputAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested && !pythonProcess.HasExited)
        {
            try
            {
                string line = await pythonOutput.ReadLineAsync();
                if (!string.IsNullOrEmpty(line))
                {
                    Debug.Log($"Python Output: {line}");
                }
            }
            catch
            {
                break;
            }
        }
    }

    public void SendInputToPython(int num1, int num2)
    {
        if (pythonInput == null || pythonProcess.HasExited)
        {
            Debug.LogError("Python process is not running.");
            return;
        }

        string input = $"{num1} {num2}";
        pythonInput.WriteLine(input);
        pythonInput.Flush();
        Debug.Log($"Sent to Python: {input}");
    }

    // Python 프로세스 종료
    public void FinishPython()
    {
        if (pythonProcess == null)
        {
            Debug.LogWarning("Python process is not running.");
            return;
        }

        cancellationTokenSource.Cancel(); // 출력 읽기 작업 취소

        // Python 프로세스 종료
        pythonInput?.Close();
        pythonOutput?.Close();
        pythonProcess.Kill();
        pythonProcess.Dispose();
        pythonProcess = null;

        Debug.Log("Python process has been terminated.");
    }

}
