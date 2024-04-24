using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuansuTalent : TalentBase
{
    private Player player;
    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
        HitBase hit = player.GetComponent<HitBase>();
        if(hit != null)
        {
            hit.OnDoDamage += Hit_OnDoDamage;
        }
    }

    private void Hit_OnDoDamage(object sender, OnDoDamageArgs e)
    {
        BuffManager.Instance.AddBuff<HuansuBuff>(e.targetUnit.gameObject, player.gameObject.name);
    }
}
