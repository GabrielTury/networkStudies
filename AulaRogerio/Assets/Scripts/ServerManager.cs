using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerManager : NetworkBehaviour
{
    public int hostScore { get; private set; }
    public int serverScore { get; private set; }
    private void Awake()
    {
        if(!NetworkManager.Singleton.IsHost)
        {
            Destroy(this.gameObject);
        }
    }
}
