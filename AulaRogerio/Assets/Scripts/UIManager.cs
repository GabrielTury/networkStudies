using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class UIManager : NetworkBehaviour
{
    #region NetworkVariables
    private NetworkVariable<int> playerNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    #endregion
    #region Variables
    [SerializeField]
    private TMP_Text clientText;
    #endregion

    public void StartHost()
    {
        if (!NetworkManager.Singleton.StartHost())
        {
            Debug.LogWarning("Failed to Start Host");
        }
        else
        {
            Debug.Log("Host Started");

            if(NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().IsSpawned)
            {
                NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().GetComponent<PlayerController>().StartLocation();
                Debug.Log("Spawned");
            }
        }

        StartCoroutine(CheckPlayers());

    }

    public void StartClient()
    {
        if(!NetworkManager.Singleton.StartClient())
        {
            Debug.LogWarning("Failed to Start Cliente");
        }
        else
        {
            Debug.Log("Client Started");

            if (NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().IsSpawned)
            {
                NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().GetComponent<PlayerController>().StartLocation();
                Debug.Log("Spawned");
            }
        }

        StartCoroutine(CheckPlayers());
    }

    private IEnumerator CheckPlayers()
    {
        Debug.Log("Started Check Players");
        //clientText.text = string.Format("Clients: {0}", playerNum.Value);
        while (IsServer)
        {
            clientText.text = string.Format("Clients: {0}", playerNum.Value);
            playerNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
            
            yield return new WaitForSeconds(0.5f);

        }

        while (!IsServer)
        {
            clientText.text = string.Format("Clients: {0}", playerNum.Value);

            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }


}
