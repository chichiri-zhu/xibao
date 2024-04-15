using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBuilding : BuildingBase
{
    [SerializeField] private int resourceAmount;
    protected override void OnStart()
    {
        GameManager.Instance.OnBattleEnd += Instance_OnBattleEnd;
    }

    private void Instance_OnBattleEnd(object sender, System.EventArgs e)
    {
        if(buildingStatus == BuildingStatus.Default)
        {
            ResourceManager.Instance.AddGold(resourceAmount);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.Instance.OnBattleEnd -= Instance_OnBattleEnd;
    }
}
