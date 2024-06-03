using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMergeComponent : SoldierMergeBase
{
    private Tower tower;
    public override void OnMerge()
    {
        if(tower != null)
        {
            tower.OnHit += Tower_OnHit;
        }
    }

    private void Tower_OnHit(object sender, DoHitArgs e)
    {
        BuffManager.Instance.AddBuff<TCellBuff>(e.targetUnit.gameObject, gameObject.name);
    }

    public override void OnRemove()
    {
        if (tower != null)
        {
            tower.OnHit -= Tower_OnHit;
        }
    }
}
