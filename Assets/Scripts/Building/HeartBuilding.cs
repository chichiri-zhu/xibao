using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class HeartBuilding : BuildingBase
{
    [SerializeField] private List<string> upgradeStringList;
    public List<TalentSO> upgradeTalentList;

    protected override void Start()
    {
        base.Start();
        InitUpgrade();
    }

    public void InitUpgrade()
    {
        if (upgradeStringList != null && upgradeStringList.Count > 0)
        {
            Type upgradeCompent = Type.GetType(upgradeStringList[0]);
            upgradeStringList.RemoveAt(0);
            UpgradeBase upgradeComponent = gameObject.AddComponent(upgradeCompent) as UpgradeBase;
            upgradeComponent.OnUpgradeDone += UpgradeComponent_OnUpgrade;
        }
    }

    private void UpgradeComponent_OnUpgrade(object sender, EventArgs e)
    {
        InitUpgrade();
    }

    public override void Died()
    {
        base.Died();
        GameManager.Instance.GameOver();
    }
}
