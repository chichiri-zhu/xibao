using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMoveAndHit : MoveAndHit
{
    private LayerMask layerMask;

    protected override void OnStart()
    {
        layerMask = LayerMask.GetMask("Enemy");
    }

    public override void LookForTarget()
    {
        AttributeParam attributeParam = soldier.GetAttributeSystem().GetAttributeParam();
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, defaultFindRof + attributeParam.Rof, layerMask);
        foreach (var collider in collider2DArray)
        {
            if (collider.gameObject == gameObject)
            {
                continue;
            }
            UnitBase unit = collider.GetComponent<UnitBase>();
            if (!unit.CanLookFor())
            {
                continue;
            }
            if(targetUnit == null || !targetUnit.CanLookFor())
            {
                targetUnit = unit;
            }
            else
            {
                if (unit != null)
                {
                    if (Vector3.Distance(transform.position, unit.transform.position) <
                        Vector3.Distance(transform.position, targetUnit.transform.position))
                    {
                        targetUnit = unit;
                    }
                }
            }
        }
    }
}
