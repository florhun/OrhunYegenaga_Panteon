using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    private static Vector2 startTouch;
    private Vector2 swipeDelta;


    public void Move(Transform _transform, float width, float speed)
    {

        #region UNITYEDITOR
        
            if (Input.GetMouseButtonDown(0))
            {
                startTouch = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                swipeDelta = (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - startTouch).normalized;
                MoveToTarget(_transform, swipeDelta.x * width, speed * Time.deltaTime);
            }
            
        #endregion

        #region MOBILE
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)            
                {
                    case TouchPhase.Began:
                        startTouch = touch.position;
                        break;
                    case TouchPhase.Moved: case TouchPhase.Stationary:
                        swipeDelta = (touch.position - startTouch).normalized;
                        StartCoroutine(MoveToTarget(_transform, swipeDelta.x * width, speed));
                        break;
                    case TouchPhase.Ended: case TouchPhase.Canceled:
                        startTouch = Vector2.zero;
                        break;
                }
            }
        #endregion
        
    }

    IEnumerator MoveToTarget(Transform _transform, float xTarget, float speed)
    {
        Vector3 current = _transform.position;

        if (current.x != xTarget)
        {
            _transform.position = Vector3.MoveTowards(current, new Vector3(xTarget, current.y, current.z),
                speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
        yield break;
    }
}
