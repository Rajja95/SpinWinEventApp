using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField] private string titleId = "BE276";

    private void Awake()
    {
        PlayFabSettings.staticSettings.TitleId = titleId;
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("PlayFab Login Success");
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}