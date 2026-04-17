using UnityEngine;

public static class InternetChecker
{
    public static bool IsOnline()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}