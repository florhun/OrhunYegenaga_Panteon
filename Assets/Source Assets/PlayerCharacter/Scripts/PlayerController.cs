using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    public Animator animator;
    [SerializeField] private Rigidbody rb;
    public static bool isMoving;
    public List<Transform> startPoints = new List<Transform>();


    private void FixedUpdate()
    {
        switch (GameManager.instance.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
                if (Input.GetMouseButtonDown(0))
                {
                    GameStart();
                }

                if (Input.touches.Length > 0)
                {
                    GameStart();
                }

                break;
            case GameManager.GameState.Task1:
                InputManager.instance.Move(speed);
                break;
            case GameManager.GameState.Task2:
                if (painter.GetComponent<TexturePainter>()._percantage >= 99)
                {
                    StartCoroutine(FinishSecondTask());
                }

                break;
            case GameManager.GameState.Task3:
                InputManager.instance.Move(speed);
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
            Repeat();
        }

        if (other.transform.CompareTag("Platform"))
        {
            CancelInvoke(nameof(MoveWithPlatform));
        }

        if (other.transform.CompareTag("Stick"))
        {
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
            _currentPlatform = RP;
            transform.rotation = Quaternion.Euler(0, 0, -5 * transform.position.x);
            InvokeRepeating(nameof(MoveWithPlatform), 1, 1);
        }

        if (other.transform.CompareTag("Task1"))
        {
            StartCoroutine(FinishFirstTask());
        }
        if (other.transform.CompareTag("FinishLine"))
        {
            GameManager.instance.CurrentGameState = GameManager.GameState.FinishGame;
            animator.SetTrigger("Celebrate");
        }
    }

    [SerializeField] private GameObject painter;

    IEnumerator FinishFirstTask()
    {
        GameManager.instance.CurrentGameState = GameManager.GameState.Task2;
        animator.SetTrigger("Idle");
        CameraManager.instance.BehindCam();
        yield return new WaitForSeconds(2f);
        painter.SetActive(true);
    }

    [SerializeField] private GameObject wall;

    IEnumerator FinishSecondTask()
    {
        GameManager.instance.CurrentGameState = GameManager.GameState.Transition;
        CameraManager.instance.UpCam();
        wall.transform.DOMoveY(-4.98f, 2, false);
        yield return new WaitForSeconds(2f);
        painter.SetActive(false);
        animator.SetTrigger("Run");
        rb.mass = 100f;
        transform.DOMoveZ(165, 2, false);
        yield return new WaitForSeconds(2);
        GameManager.instance.CurrentGameState = GameManager.GameState.Task3;
    }

    private int _count;
    private RotatingPlatform _currentPlatform;

    private void MoveWithPlatform()
    {
        print(_count);
        rb.velocity = Vector3.left * (_currentPlatform.dir * 2);
        rb.AddForce(Vector3.left * (_currentPlatform.dir * _currentPlatform.speed * 2f), ForceMode.Force);
        _count++;
        //rb.AddTorque(Vector3.left, ForceMode.Impulse);
    }


    void Repeat()
    {
        switch (GameManager.instance.CurrentGameState)
        {
            case GameManager.GameState.Task1:
                transform.position = startPoints[0].position;
                break;
            case GameManager.GameState.Task3:
                transform.position = startPoints[1].position;
                break;
        }
    }
}