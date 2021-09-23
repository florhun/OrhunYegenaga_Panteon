using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatingPlatform : MonoBehaviour, IMovable
{
    public float friction;
    public float speed;
    public float dir;
    public Vector3 force;

    [SerializeField] private Rigidbody body;

    private void Awake()
    {
        Move();
        force = Vector3.right * Mathf.Sign(dir);
    }

    public void Move()
    {
        transform.DORotate(new Vector3(0, 0, 360), speed, RotateMode.FastBeyond360)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }

    // public float gravity = -9.8f;
    // public void Attract() {
    //     Vector3 gravityUp = (body.position - transform.position).normalized;
    //     
    //     print("gravity up is : " + gravityUp);
    //     Vector3 localUp = body.transform.up;
    //
    //     // Apply downwards gravity to body
    //     body.AddForce(gravityUp * gravity);
    //     // Allign bodies up axis with the centre of planet
    //     body.rotation = Quaternion.Euler(0,0,-5 * body.position.x);
    // }
    //
    // public void StartAttract(Rigidbody rb)
    // {
    //     body = rb;
    //     InvokeRepeating(nameof(Attract), 1,1/60f);
    // }
    //
    // public void StopAttract()
    // {
    //     CancelInvoke(nameof(Attract));
    // }
}