using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    public Animator animator;
    [SerializeField] private Rigidbody rb;


    private void FixedUpdate()
    {
        switch (GameManager.instance.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
                break;
            case GameManager.GameState.Task1:
                InputManager.instance.Move(speed);
                break;
            case GameManager.GameState.Task2:
                break;
            case GameManager.GameState.FinishGame:
                break;
        }
    }

    public void GameStart()
    {
        GameManager.instance.CurrentGameState = GameManager.GameState.Task1;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Stationary"))
        {
        }

        if (other.transform.CompareTag("Stick"))
        {
            print("stick");
            rb.AddForce(Vector3.back * 1000, ForceMode.Force);
        }

        if (other.transform.CompareTag("Donut"))
        {
            if (transform.position.x > 0)
            {
                rb.AddForce(new Vector3(-1, 0, -1) * 500, ForceMode.Force);
            }
            else
            {
                rb.AddForce(new Vector3(1, 0, -1) * 500, ForceMode.Force);
            }
        }

        if (other.gameObject.TryGetComponent(out RotatingPlatform RP))
        {
            //transform.SetParent(other.transform);
            // horizontalSpeed = horizontalSpeed * 1.5f;
            transform.rotation = Quaternion.Euler(0, 0, -5 * transform.position.x);
            // _rb.AddForce();
        }
    }

    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.TryGetComponent(out RotatingPlatform RP))
    //     {
    //         other.transform.DetachChildren();
    //     }
    // }

    private void MoveWithPlatform(Transform platform, bool isRight, float friction)
    {
        //_rb.AddForce(new Vector3(-1,0,-1) * 250, ForceMode.Acceleration);
    }
}