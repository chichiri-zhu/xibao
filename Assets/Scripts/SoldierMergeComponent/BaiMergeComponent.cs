using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaiMergeComponent : SoldierMergeBase
{
    private AttributeSystem attributeSystem;
    public override void OnMerge()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.AddAttributeAmount(Attribute.Dmg, 20f);
    }

    public override void OnRemove()
    {
        attributeSystem.AddAttributeAmount(Attribute.Dmg, -20f);
    }
}
