using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletBehaviour : NetworkBehaviour
{
    
    float timeAlive;

    public enum BulletOwner
    {
        Host,
        Client
    };

    public BulletOwner owner;

    private void OnEnable()
    {
        timeAlive = 0;
        StartCoroutine(Travel());
    }

    private IEnumerator Travel()
    {
        while (timeAlive < 5)
        {

            transform.Translate(0, 0, 2 * Time.deltaTime);

            timeAlive += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        OnDisable();
    }
    private void OnDisable()
    {
        timeAlive = 0;

        Destroy(gameObject);
    }

    [ServerRpc]
    private void AddServerRpc()
    {
        UIManager.instance.AddScore(10);
    }

    [ClientRpc]
    private void AddClientRpc()
    {
        if (IsHost) return;
        Debug.LogError("Should Only Call on client");
        UIManager.instance.AddScore(10);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit");

            if(IsHost && owner == BulletOwner.Host)
                AddServerRpc();
                
            if (owner == BulletOwner.Client)
                AddClientRpc();

            Destroy(gameObject);

        }
    }
}
