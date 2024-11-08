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
                UnityEngine.Debug.Log("R key pressed. Executing Python script...");

                // 파이썬 실행 준비
                string pythonPath = @"C:\Users\User\AppData\Local\Microsoft\WindowsApps\python.exe";  // 시스템에 설치된 Python 경로
                string scriptPath = @"C:\Users\User\Desktop\test\AI-NPC\Please\main.py";  // 실행할 스크립트 경로

                Process psi = new Process();
                psi.StartInfo.FileName = @"C:\Users\User\AppData\Local\Microsoft\WindowsApps\python.exe";
                psi.StartInfo.Arguments = @"C:\Users\User\Desktop\test\AI-NPC\Please\main.py";
                //psi.StartInfo.FileName = "C:/Users/User/Desktop/test/AI-NPC/Please/dist/main.exe";


                // 유니티에서 받은 입력값을 인자로 전달
                string userInput = "test_input";  
                psi.StartInfo.Arguments = psi.StartInfo.Arguments = $"\"{pythonPath}\"";

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
                        UnityEngine.Debug.Log("Output from Python: " + e.Data);
                    }
                };

                psi.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        UnityEngine.Debug.LogError("Error from Python: " + e.Data);
                    }
                };

                psi.Start();
                psi.BeginOutputReadLine();
                psi.BeginErrorReadLine();

                // 종료 이벤트 처리
                psi.EnableRaisingEvents = true;
                psi.Exited += (sender, e) =>
                {
                    UnityEngine.Debug.Log("Python process exited with code: " + psi.ExitCode);
                    psi.Dispose();
                };
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("Unable to launch Python script: " + e.Message);
            }
        }
    }
}
