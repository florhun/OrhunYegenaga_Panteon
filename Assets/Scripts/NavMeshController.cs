using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NavMeshController : MonoBehaviour
{
    [SerializeField] private Transform dest;
    private NavMeshAgent _navMesh;
    private Animator _anim;
    private Rigidbody _rb;
    public Transform startPoint;


    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _navMesh.speed = Random.Range(3, 8);
    }

    private void Update()
    {
        if (GameManager.instance.CurrentGameState == GameManager.GameState.Task3)
        {
            _navMesh.SetDestination(dest.position);
            _anim.SetTrigger("Run");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Stationary"))
        {
            transform.position = startPoint.position;
        }

        if (other.transform.CompareTag("Stick"))
        {
            _rb.AddForce(Vector3.back * 1000, ForceMode.Force);
        }

        if (other.transform.CompareTag("Donut"))
        {
            if (transform.position.x > 0)
            {
                _rb.AddForce(new Vector3(-1, 0, -1) * 750, ForceMode.Force);
            }
            else
            {
                _rb.AddForce(new Vector3(1, 0, -1) * 750, ForceMode.Force);
            }
        }

        if (other.transform.CompareTag("Finish"))
        {
            _anim.SetTrigger("Idle");
        }
    }
}