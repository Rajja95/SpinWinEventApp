using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLFileSaver
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void DownloadFile(string filename, string content);
#endif

    public void Save(string content)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        DownloadFile("SpinWinData.csv", content);
#else
        // Save locally in Editor for testing
        string path = System.IO.Path.Combine(
            UnityEngine.Application.dataPath,
            "SpinWinData_Test.csv"
        );

        System.IO.File.WriteAllText(path, content);

        Debug.Log("Saved locally at: " + path);
#endif
    }
}