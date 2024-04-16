using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCellUpgradeTwo : UpgradeBase
{
    private void Awake()
    {
        upgradeAmount = 8;
    }

    public override IEnumerator _DoUpgrade()
    {
        GenerativeCell generativeCell = transform.gameObject.GetComponent<GenerativeCell>();
        if (generativeCell == null)
        {
            Destroy(this);
        }
        else
        {
            generativeCell.ResetAmount(8);
            talentChosen = TalentChosenEnum.done;
        }
        yield return null;
    }
}
