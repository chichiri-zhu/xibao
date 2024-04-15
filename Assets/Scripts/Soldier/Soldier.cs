using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : SoldierBase
{
    public override void OnStart()
    {
        //enemyController = GetComponent<EnemyController>();

        if (healthSystem != null)
        {
            healthSystem.OnDied += HealthSystem_OnDied;
        }
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
}
