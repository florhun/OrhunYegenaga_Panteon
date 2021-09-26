using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] private Transform player;
    [SerializeField] private float platformWidth;

    private static Vector2 _startTouch;
    private Vector2 _swipeDelta;
    private PlayerController _pc;
    private bool _isMoving;


    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private void Start()
    {
        _pc = player.GetComponent<PlayerController>();
    }

    //This function used to gather input information and sends them to movement function.
    public void Move(float speed)
    {
        #region UNITYEDITOR

        if (Input.GetMouseButtonDown(0))
        {
            _startTouch = Input.mousePosition;
            _pc.animator.SetTrigger(Run);
        }

        if (Input.GetMouseButton(0))
        {
            _swipeDelta = (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _startTouch).normalized;

            if (_swipeDelta.magnitude > .2f)
            {
                PlayerController.isMoving = true;
            }
            StartCoroutine(MoveToTarget(_swipeDelta.x, _swipeDelta.y, speed, platformWidth * Math.Sign(_swipeDelta.x)));
            SetRotation();
        }

        if (Input.GetMouseButtonUp(0))
        {
            PlayerController.isMoving = false;
            _swipeDelta = Vector2.zero;
            _pc.animator.SetTrigger(Idle);
        }

        #endregion

        #region MOBILE

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startTouch = touch.position;
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    _swipeDelta = (touch.position - _startTouch).normalized;
                    StartCoroutine(MoveToTarget(_swipeDelta.x, _swipeDelta.y, speed,
                        platformWidth * Math.Sign(_swipeDelta.x)));
                    SetRotation();
                    _pc.animator.SetTrigger(Run);

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _startTouch = Vector2.zero;
                    _pc.animator.SetTrigger(Idle);

                    break;
            }
        }

        #endregion
    }

    //This function uses MoveTowards property to move player in a constant speed.
    IEnumerator MoveToTarget(float xTarget, float zTarget, float speed, float limit)
    {
        Vector3 current = player.position;

        //Checks the current X position to prevent reaching platforms edges. Moves vertically if true.
        if ((Math.Abs(current.x - limit) > .1f))
        {
            player.position = Vector3.MoveTowards(current, new Vector3(xTarget + current.x, current.y, current.z),
                speed * Time.deltaTime);
        }

        //Adds horizontal movement.
        player.position = Vector3.MoveTowards(current, new Vector3(player.position.x, current.y, current.z + zTarget),
            speed * Time.deltaTime);
        yield return new WaitForEndOfFrame();
    }

    //Sets the rotation of the player.
    private void SetRotation()
    {
        if (_swipeDelta.y > 0)
        {
            player.rotation = Quaternion.Euler(0, 90f * _swipeDelta.x, 0);
        }
        else
        {
            player.rotation = Quaternion.Euler(0, 180 - (90f * _swipeDelta.x), 0);
        }
    }
}