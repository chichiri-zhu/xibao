using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMergeComponent : SoldierMergeBase
{
    private AttributeSystem attributeSystem;
    public override void OnMerge()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.AddAttributeAmountPercent(Attribute.Rof, 20f);
    }

    public override void OnRemove()
    {
        attributeSystem.AddAttributeAmountPercent(Attribute.Rof, -20f);
    }
}
