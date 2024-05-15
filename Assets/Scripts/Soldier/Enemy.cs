using System.Collections;
using System.Collections.Generic;
using Assets.FantasyMonsters.Scripts;
using UnityEngine;

public class Enemy : SoldierBase
{
    private Monster monster;
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
        if(soldierSource == SoldierSource.Monster)
        {
            monster = GetComponentInChildren<Monster>();
        }
        
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
            if(soldierSource == SoldierSource.Owner)
            {
                animator?.SetBool("Walk", true);
            }
            else
            {
                monster.SetState(MonsterState.Idle);
            }
        }
    }

    public override void SetWalk()
    {
        if (soldierSource == SoldierSource.Owner)
        {
            animator?.SetBool("Walk", true);
        }
        else
        {
            monster.SetState(MonsterState.Walk);
        }
    }

    public override void SetHit()
    {
        if (soldierSource == SoldierSource.Owner)
        {
            animator?.SetBool("Walk", false);
            animator?.SetTrigger("Hit");
        }
        else
        {
            monster.Attack();
        }
    }
}
