using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HorizontalObject : MonoBehaviour, IMovable
{
    [SerializeField] private float speed;
    [SerializeField] private float dir;

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (Mathf.Abs(transform.position.x) > 4.8f)
        {
            dir = -dir;
        }

        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3((5 * dir), transform.position.y, transform.position.z),
            speed * Time.deltaTime);
    }
}