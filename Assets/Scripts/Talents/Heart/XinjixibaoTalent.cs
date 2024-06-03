using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XinjixibaoTalent : TalentBase
{
    private AttributeSystem attributeSystem;

    private void Start()
    {
        GameObject heartBuilding = GameManager.Instance.GetMainBuilding();
        if(heartBuilding != null)
        {
            attributeSystem = heartBuilding.GetComponent<AttributeSystem>();
            if(attributeSystem != null)
            {
                attributeSystem.AddAttributeAmountPercent(Attribute.Hp, 300f);
            }
        }
    }
}
