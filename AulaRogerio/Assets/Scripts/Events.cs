using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Events
{
    public static event UnityAction Server_Started;
    public static void OnServerStarted() => Server_Started?.Invoke();

    public static event UnityAction Player_Connected;
    public static void OnPlayerConnected() => Player_Connected?.Invoke();
}
