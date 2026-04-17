using RUS95.SpinWinEventApp.Data;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Systems.Persistence
{
    public class GameDataService
    {
        private readonly CsvDataSaver _csvSaver;
        private readonly PlayFabUploader _playFabUploader;

        public GameDataService()
        {
            _csvSaver = new CsvDataSaver();
            _playFabUploader = new PlayFabUploader();
        }

        public void Save(GameSessionData data)
        {
            // 1. ALWAYS save locally
            _csvSaver.Save(data);

            // 2. Upload if online
            if (InternetChecker.IsOnline())
            {
                _playFabUploader.Upload(data);
            }
            else
            {
                Debug.Log("Offline → Saved only to CSV");
            }
        }
    }
}