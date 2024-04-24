using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerUpgrade : UpgradeBase
{
    private void Awake()
    {
        upgradeAmount = 5;
    }
    public override IEnumerator _DoUpgrade()
    {
        BuildingManager.Instance.Build(AssetManager.Instance.buildingListSO.buildingList.FirstOrDefault(obj => obj.name == "TowerTwo"), transform.position);
        BuildingManager.Instance.RemoveBuilding(GetComponent<BuildingBase>());
        yield return null;
    }
}
