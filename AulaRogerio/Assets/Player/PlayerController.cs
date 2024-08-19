using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction move, turn;

    Animator animator;

    private void Awake()
    {
        InputActionsGame iGame = new InputActionsGame();

        move = iGame.Game.Move;
        turn = iGame.Game.Turn;

        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
    }

    private void OnEnable()
    {
        move.Enable();
        turn.Enable();
    }

    // Update is called once per frame
    void Update()
    {


        transform.Translate(0,0,
            move.ReadValue<float>() * 10 * Time.deltaTime);


        transform.Rotate(0,
            turn.ReadValue<float>() * 180 * Time.deltaTime,0);

        animator.SetFloat("MoveValue", Mathf.Abs(move.ReadValue<float>()));
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
            transform.Translate(0, 1f * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }


    }

    private IEnumerator Down()
    {
        while (true)
        {
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
    }
}
