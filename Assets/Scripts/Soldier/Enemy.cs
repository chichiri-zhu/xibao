using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SoldierBase
{
    public static Enemy Create(ArmsSO arms, Vector3 position)
    {
        Transform soldierTransform = Instantiate(arms.prefab, position, Quaternion.identity);

        Enemy enemy = soldierTransform.GetComponent<Enemy>();

        return enemy;
    }

    public EnemyController enemyController;//暂时不用
    

    public override void OnStart()
    {
        //enemyController = GetComponent<EnemyController>();
        
        if(healthSystem != null)
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
        if(animator != null)
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
