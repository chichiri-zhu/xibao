using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerUpgrade : UpgradeBase
{
    private int defalutUpgradeAmount;
    private void Awake()
    {
        upgradeAmount = 8;
        defalutUpgradeAmount = upgradeAmount;
        upgradeAmount = defalutUpgradeAmount - GameManager.Instance.towerUpgradeDec;
    }

    public override void Start()
    {
        base.Start();
        TalentManager.Instance.OnTalentAdd += Instance_OnTalentAdd;
    }

    private void Instance_OnTalentAdd(object sender, OnTalentAddArgs e)
    {
        upgradeAmount = defalutUpgradeAmount - GameManager.Instance.towerUpgradeDec;
        InitExpendResource();
    }

    public override IEnumerator _DoUpgrade()
    {
        talentChosen = TalentChosenEnum.done;
        yield return null;
    }

    public override void UpgradeDone()
    {
        BuildingManager.Instance.Build(AssetManager.Instance.buildingListSO.buildingList.FirstOrDefault(obj => obj.name == "TowerTwo"), transform.position);
        BuildingManager.Instance.RemoveBuilding(GetComponent<BuildingBase>());
    }

    private void OnDestroy()
    {
        TalentManager.Instance.OnTalentAdd -= Instance_OnTalentAdd;
    }
}
