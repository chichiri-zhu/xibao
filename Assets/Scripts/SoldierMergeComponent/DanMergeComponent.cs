using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanMergeComponent : SoldierMergeBase
{
    private AttributeSystem attributeSystem;
    public override void OnMerge()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.AddAttributeAmountPercent(Attribute.Hp, 20f);
    }

    public override void OnRemove()
    {
        attributeSystem.AddAttributeAmountPercent(Attribute.Hp, -20f);
    }
}
