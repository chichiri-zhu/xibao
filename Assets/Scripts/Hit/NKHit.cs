using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NKHit : CommonHit
{
    public override void DoDamage(UnitBase unit)
    {
        base.DoDamage(unit);
        AttributeParam attributeParam = attributeSystem.GetAttributeParam();
        LayerMask layerMask = LayerMask.GetMask("Enemy");
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, 3 + attributeParam.Rof, layerMask);
        foreach (Collider2D collider in collider2DArray)
        {
            if(collider.gameObject == unit.gameObject)
            {
                continue;
            }

            UnitBase u = collider.GetComponent<UnitBase>();
            if(u != null)
            {
                base.DoDamage(u);
                break;
            }
        }
    }
}
