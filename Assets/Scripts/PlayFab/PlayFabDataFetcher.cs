using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using RUS95.SpinWinEventApp.Data;

namespace RUS95.SpinWinEventApp.Systems.Persistence
{
    public class PlayFabDataFetcher
    {
        public void FetchAll(Action<List<GameSessionData>> onSuccess)
        {
            var request = new GetUserDataRequest();

            PlayFabClientAPI.GetUserData(request, result =>
            {
                var list = new List<GameSessionData>();

                foreach (var item in result.Data)
                {
                    try
                    {
                        var data = JsonUtility.FromJson<GameSessionData>(item.Value.Value);
                        list.Add(data);
                    }
                    catch
                    {
                        Debug.LogWarning("Invalid data skipped");
                    }
                }

                onSuccess?.Invoke(list);

            }, error =>
            {
                Debug.LogError("Fetch failed: " + error.GenerateErrorReport());
            });
        }
    }
}