using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaojixibaojisuTalent : TalentBase
{
    private Player player;
    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
        if(player != null)
        {
            AttributeSystem attributeSystem = player.GetAttributeSystem();
            attributeSystem.AddAttributeAmount(Attribute.Dmg, 100);
            attributeSystem.AddAttributeAmountPercent(Attribute.Hp, 100);
        }
    }
}
