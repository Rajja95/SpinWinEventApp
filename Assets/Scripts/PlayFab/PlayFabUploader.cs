using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using RUS95.SpinWinEventApp.Data;

namespace RUS95.SpinWinEventApp.Systems.Persistence
{
    public class PlayFabUploader
    {
        public void Upload(GameSessionData data)
        {
            var request = new WriteClientPlayerEventRequest
            {
                EventName = "spin_result",
                Body = new Dictionary<string, object>
        {
            { "DateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
            { "Result", data.IsWin ? "WIN" : "LOSE" },
            { "Name", data.Name },
            { "Email", data.Email },
            { "Phone", data.Phone }
        }
            };

            PlayFabClientAPI.WritePlayerEvent(request,
                result => Debug.Log("PlayFab Event Upload Success"),
                error => Debug.LogError(error.GenerateErrorReport()));
        }
    }
}