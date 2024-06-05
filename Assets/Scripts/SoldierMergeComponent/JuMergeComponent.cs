using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuMergeComponent : SoldierMergeBase
{
    //对周围的敌人造成持续伤害
    private Tower tower;
    private AttributeSystem attributeSystem;
    private float rof = 1f;
    public override void OnMerge()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        tower = GetComponent<Tower>();
        if (tower != null)
        {
            tower.OnHit += Tower_OnHit;
        }
    }

    private void Tower_OnHit(object sender, DoHitArgs e)
    {
        AttributeParam attributeParam = attributeSystem.GetAttributeParam();
        LayerMask layerMask = LayerMask.GetMask("Enemy");
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, rof, layerMask);
        foreach (Collider2D collider in collider2DArray)
        {
            HealthSystem healthSystem = collider?.GetComponent<HealthSystem>();
            if(healthSystem != null)
            {
                healthSystem.Damage((int)attributeParam.Atk);
            }
        }
    }

    public override void OnRemove()
    {
        if (tower != null)
        {
            tower.OnHit -= Tower_OnHit;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rof);
    }
}
