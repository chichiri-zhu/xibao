using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerUpgradeTwo : UpgradeBase
{
    private void Awake()
    {
        upgradeAmount = 8;
    }
    public override IEnumerator UpgradeDone()
    {
        Transform towerTwoTransform = AssetManager.Instance.buildingListSO.buildingList.FirstOrDefault(obj => obj.name == "TowerThree").prefab;
        Instantiate(towerTwoTransform, transform.position, Quaternion.identity);
        Destroy(transform.gameObject);
        yield return null;
    }
}
