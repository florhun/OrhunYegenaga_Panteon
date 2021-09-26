using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotator : MonoBehaviour
{
    public float speed;
    public float dir;

    private void Awake()
    {
        Move();
    }

    public void Move()
    {
        transform.DORotate(Vector3.up * (dir * 360), speed, RotateMode.LocalAxisAdd)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }
}
