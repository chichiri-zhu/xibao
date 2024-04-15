using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineBuilding : BuildingBase
{
    private int resourceAmount;
    protected override void OnStart()
    {
        resourceAmount = 6;
        GameManager.Instance.OnBattleEnd += Instance_OnBattleEnd;
    }

    private void Instance_OnBattleEnd(object sender, System.EventArgs e)
    {
        if (buildingStatus == BuildingStatus.Default)
        {
            ResourceManager.Instance.AddGold(resourceAmount);
        }
        resourceAmount--;
        resourceAmount = Mathf.Clamp(resourceAmount, 1, resourceAmount);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.Instance.OnBattleEnd -= Instance_OnBattleEnd;
    }
}
