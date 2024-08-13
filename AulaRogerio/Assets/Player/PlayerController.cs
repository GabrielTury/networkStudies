using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction move, turn;

    private void Awake()
    {
        InputActionsGame iGame = new InputActionsGame();

        move = iGame.Game.Move;
        turn = iGame.Game.Turn;
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
    }

    private void OnDisable()
    {
        move.Disable();
        turn.Disable();
    }
}
