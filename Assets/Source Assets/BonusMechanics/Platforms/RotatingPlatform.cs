using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatingPlatform : MonoBehaviour, IMovable
{
    public float speed;
    public float dir;

    private void Awake()
    {
        Move();
    }

    public void Move()
    {
        transform.DORotate(Vector3.forward * (dir * 360), speed, RotateMode.FastBeyond360)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }
}