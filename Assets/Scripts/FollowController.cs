using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowController : MonoBehaviour
{
    private Transform target;
    private SoldierBase soldierbase;

    private void Start()
    {
        soldierbase = GetComponent<SoldierBase>();
    }

    private void Update()
    {
        HandleFollow();
    }

    private void HandleFollow()
    {
        if(target != null)
        {
            soldierbase.Move(target.transform.position);
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnDestroy()
    {
        soldierbase.navMeshAgent.isStopped = true;
        //soldierbase.Move(transform.position);
    }
}
