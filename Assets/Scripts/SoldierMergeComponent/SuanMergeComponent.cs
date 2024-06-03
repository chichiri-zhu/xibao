using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuanMergeComponent : SoldierMergeBase
{
    private AttributeSystem attributeSystem;
    public override void OnMerge()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        attributeSystem.AddAttributeAmount(Attribute.ParasiteDmgAmount, 50f);
    }

    public override void OnRemove()
    {
        attributeSystem.AddAttributeAmount(Attribute.ParasiteDmgAmount, -50f);
    }
}
