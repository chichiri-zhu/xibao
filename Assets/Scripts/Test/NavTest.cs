using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        if(target != null)
        {
            Move();
        }
    }

    private void Move()
    {
        navMeshAgent.SetDestination(target.transform.position);
    }
}
