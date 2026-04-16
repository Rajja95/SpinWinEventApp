using System.Runtime.InteropServices;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Systems.Persistence
{
    public class WebGLFileSaver
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void DownloadFile(string filename, string content);
#endif

        public void Save(string csvContent)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            DownloadFile("SpinWinData.csv", csvContent);
#else
            Debug.Log("WebGL save skipped (not running in WebGL)");
#endif
        }
    }
}