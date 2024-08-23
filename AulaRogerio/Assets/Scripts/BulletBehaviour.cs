using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletBehaviour : NetworkBehaviour
{
    
    float timeAlive;

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
        if(!NetworkManager.Singleton.IsHost)
        {
            GetComponent<NetworkObject>().Despawn();
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
