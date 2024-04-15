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
    public override IEnumerator UpgradeDone()
    {
        Transform towerTwoTransform = AssetManager.Instance.buildingListSO.buildingList.FirstOrDefault(obj => obj.name == "TowerTwo").prefab;
        Instantiate(towerTwoTransform, transform.position, Quaternion.identity);
        Destroy(transform.gameObject);
        yield return null;
    }
}
