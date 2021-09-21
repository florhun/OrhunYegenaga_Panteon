using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float xLimit;

    [SerializeField] private Animator _animator;
    
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Idle = Animator.StringToHash("Idle");


    private void FixedUpdate()
    {
        switch (GameManager.instance.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
                if (Input.GetMouseButtonDown(0))
                {
                    GameStart();
                }
                break;
            case GameManager.GameState.MainGame:
                transform.Translate(Vector3.forward * (forwardSpeed * Time.deltaTime));
                InputManager.instance.Move(transform, xLimit, forwardSpeed);
                break;
            case GameManager.GameState.Jumping:
                break;
            case GameManager.GameState.FinishGame:
                break;
        }
    }

    private void GameStart()
    {
        GameManager.instance.CurrentGameState = GameManager.GameState.MainGame;
        _animator.SetTrigger(Run);
    }
}
