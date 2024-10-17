using System;
using System.Diagnostics;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        UnityEngine.Debug.Log("Hello");
    }

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
                psi.Start();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("Catch");
                UnityEngine.Debug.LogError("Unable to launch app: " + e.Message);
            }
        }
    }
}
