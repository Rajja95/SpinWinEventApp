using RUS95.SpinWinEventApp.Data;
using System.Collections.Generic;
using UnityEngine;

namespace RUS95.SpinWinEventApp.Systems.Persistence
{
    public class GameDataService
    {
        #region Fields

        private readonly CsvDataSaver _csvSaver;
        private readonly PlayFabUploader _playFabUploader;

        private List<GameSessionData> _sessionDataList = new List<GameSessionData>();
        private List<GameSessionData> _pendingUploadList = new List<GameSessionData>();

        private const string SaveKey = "SpinWin_Data";
        private const string PendingKey = "SpinWin_Pending";

        #endregion

        #region Constructor

        public GameDataService()
        {
            _csvSaver = new CsvDataSaver();
            _playFabUploader = new PlayFabUploader();

            LoadFromLocal(); // restore saved data
        }

        #endregion

        #region Public Methods

        public void Save(GameSessionData data)
        {
            // 1. Store in memory
            _sessionDataList.Add(data);

            // 2. Persist locally
            SaveToLocal();

            // 3. Save CSV backup (optional but useful)
            _csvSaver.Save(data);

            // 4. Upload or queue
            if (InternetChecker.IsOnline())
            {
                UploadToPlayFab(data);
            }
            else
            {
                AddToPending(data);
            }
        }

        public List<GameSessionData> GetAllSessionData()
        {
            return _sessionDataList;
        }

        public void RetryPendingUploads()
        {
            if (!InternetChecker.IsOnline()) return;

            for (int i = _pendingUploadList.Count - 1; i >= 0; i--)
            {
                var data = _pendingUploadList[i];

                _playFabUploader.Upload(data,
                    onSuccess: () =>
                    {
                        _pendingUploadList.RemoveAt(i);
                        SavePending(); // update storage
                    },
                    onError: () =>
                    {
                        // keep it
                    });
            }
        }

        #endregion

        #region Upload

        private void UploadToPlayFab(GameSessionData data)
        {
            _playFabUploader.Upload(data,
                onSuccess: () =>
                {
                    Debug.Log("Uploaded successfully");
                },
                onError: () =>
                {
                    AddToPending(data);
                });
        }

        private void AddToPending(GameSessionData data)
        {
            if (!_pendingUploadList.Contains(data))
            {
                _pendingUploadList.Add(data);
                SavePending();
            }
        }

        #endregion

        #region Persistence

        private void SaveToLocal()
        {
            Wrapper wrapper = new Wrapper { list = _sessionDataList };

            string json = JsonUtility.ToJson(wrapper);

            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        private void SavePending()
        {
            Wrapper wrapper = new Wrapper { list = _pendingUploadList };

            string json = JsonUtility.ToJson(wrapper);

            PlayerPrefs.SetString(PendingKey, json);
            PlayerPrefs.Save();
        }

        private void LoadFromLocal()
        {
            // Load main data
            if (PlayerPrefs.HasKey(SaveKey))
            {
                string json = PlayerPrefs.GetString(SaveKey);
                Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);

                if (wrapper != null && wrapper.list != null)
                    _sessionDataList = wrapper.list;
            }

            // Load pending uploads
            if (PlayerPrefs.HasKey(PendingKey))
            {
                string json = PlayerPrefs.GetString(PendingKey);
                Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);

                if (wrapper != null && wrapper.list != null)
                    _pendingUploadList = wrapper.list;
            }
        }

        #endregion

        #region Helper Class

        [System.Serializable]
        private class Wrapper
        {
            public List<GameSessionData> list;
        }

        #endregion
    }
}