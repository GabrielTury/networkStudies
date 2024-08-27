using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    InputAction move, turn, shoot;

    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public float movementAxis;

    [SerializeField]
    private GameObject bullet;

    private GameObject bul;

    [SerializeField]
    private Transform bulletPoint;

    private void Awake()
    {
        InputActionsGame iGame = new InputActionsGame();

        move = iGame.Game.Move;
        turn = iGame.Game.Turn;
        shoot = iGame.Game.Shoot;

        animator = GetComponent<Animator>();


        animator.applyRootMotion = false;
    }


    private void OnEnable()
    {
        move.Enable();
        turn.Enable();
        shoot.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        transform.Translate(0,0,
            move.ReadValue<float>() * 10 * Time.deltaTime);
        movementAxis = move.ReadValue<float>();

        transform.Rotate(0,
            turn.ReadValue<float>() * 180 * Time.deltaTime,0);

        animator.SetFloat("MoveValue", Mathf.Abs(move.ReadValue<float>()));

        if(shoot.WasPressedThisFrame())
        {
            
            
            if(IsHost)
            {
                bullet.GetComponent<BulletBehaviour>().owner = BulletBehaviour.BulletOwner.Host;
            }
            else
            {
                SetClientOwnerServerRpc();
            }
                ShootServerRpc();                           
        }
    }


    [ServerRpc]
    public void ShootServerRpc()
    {
        GameObject a = Instantiate(bullet, bulletPoint.transform.position, bulletPoint.transform.rotation);
        Debug.LogWarning(a.GetComponent<BulletBehaviour>().owner);

        NetworkObject no = a.GetComponent<NetworkObject>();
        if(no != null)
        {
            no.gameObject.GetComponent<BulletBehaviour>().owner = bullet.GetComponent<BulletBehaviour>().owner;
            no.Spawn();
        }
    }

    [ServerRpc]
    public void SetClientOwnerServerRpc()
    {
        bullet.GetComponent<BulletBehaviour>().owner = BulletBehaviour.BulletOwner.Client;
    }

    public void StartLocation()
    {
        transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, -5));
    }

    public void UpAnim()
    {
        StopAllCoroutines();

        StartCoroutine(Up());
    }

    public void DownAnim()
    {
        StopAllCoroutines();

        StartCoroutine(Down());
    }

    private IEnumerator Up()
    {
        while(true)
        {
            if(transform.position.y <= 1)            
                transform.Translate(0, 1f * Time.deltaTime, 0);

            yield return new WaitForEndOfFrame();
        }


    }

    private IEnumerator Down()
    {
        while (true)
        {
            if (transform.position.y >= 0)
                transform.Translate(0, -1f * Time.deltaTime, 0);

            yield return new WaitForEndOfFrame();
        }


    }

    public void StopWalkAnim()
    {
        StopAllCoroutines();
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void OnDisable()
    {
        move.Disable();
        turn.Disable();
        shoot.Disable();
    }
}
