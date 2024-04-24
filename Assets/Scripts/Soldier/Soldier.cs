using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : SoldierBase
{
    private Vector3 patrolPosition = Vector3.zero;//巡逻position

    public override void OnStart()
    {
        //enemyController = GetComponent<EnemyController>();

        if (healthSystem != null)
        {
            healthSystem.OnDied += HealthSystem_OnDied;
        }
        patrolPosition = transform.position;
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    public override void SetIdle()
    {
        if (animator != null)
        {
            animator?.SetBool("Walk", true);
        }
    }

    public override void SetWalk()
    {
        animator?.SetBool("Walk", true);
    }

    public override void SetHit()
    {
        animator?.SetBool("Walk", false);
        animator?.SetTrigger("Hit");
    }

    public void SetPatrolPosition(Vector3 pos)
    {
        patrolPosition = pos;
    }
}
