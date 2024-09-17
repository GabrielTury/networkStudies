using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AIController : MonoBehaviour
{
    [SerializeField]
    GameObject debugCube;

    InputAction moveAi;

    List<Vector3> path;

    float speed = 5;

    List<NetworkObject> players;
    // Start is called before the first frame update
    private void Awake()
    {
        InputActionsGame iGame = new InputActionsGame();
        players = new List<NetworkObject>();

        moveAi = iGame.Game.MoveAi;
    }
    void OnEnable()
    {
        moveAi.Enable();
    }

    void OnDisable()
    {
        moveAi.Disable();
    }

    Vector3 NearestPlayerPosition()
    {
        NetworkObject nearest = null;
        float distance = 1000;
        foreach (var player in players)
        {
            if(Vector3.Distance(player.transform.position, transform.position) < distance)
            {
                nearest = player;
            }
        }
        //Instantiate(debugCube, nearest.transform.position, Quaternion.identity);

        return nearest.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (moveAi.WasPressedThisFrame())
        {
            
            foreach(PlayerController p in FindObjectsOfType<PlayerController>())
            {
                players.Add(p.gameObject.GetComponent<NetworkObject>());
            }
            Debug.Log("Called path");
            path = GridManager.instance.CreatePath(transform.position, NearestPlayerPosition());

            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        while(path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[0], Time.deltaTime * speed);

            if(Vector3.Distance(transform.position, path[0]) < 0.1f)
            {
                path.RemoveAt(0);
            }
            

            Debug.Log("Moving to: " + path[0]);
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Endend Path");
    }
}
