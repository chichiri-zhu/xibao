using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NKMergeComponent : SoldierMergeBase
{
    private Tower tower;
    public override void OnMerge()
    {
        tower = GetComponent<Tower>();
        if(tower != null)
        {
            tower.OnHit += Tower_OnHit;
        }
    }

    private void Tower_OnHit(object sender, DoHitArgs e)
    {
        float hitRof = tower.GetHitRof();
        UnitBase targetUnit = tower.GetTargetEnemy();
        Collider2D[] enemyList = Physics2D.OverlapCircleAll(transform.position, hitRof);
        //List<Transform> enemyList = BattleManager.Instance.GetEnemyList();
        if (enemyList == null)
        {
            return;
        }


        //foreach (Transform obj in enemyList)
        foreach (Collider2D obj in enemyList)
        {
            if (obj == null)
            {
                continue;
            }

            EnemyUnit enemy = obj.GetComponent<EnemyUnit>();
            if(enemy == targetUnit)
            {
                continue;
            }

            tower.Arrow(enemy);
            break;
        }
    }

    public override void OnRemove()
    {
        if(tower != null)
        {
            tower.OnHit -= Tower_OnHit;
        }
    }
}
