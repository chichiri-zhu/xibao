using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RedBuildingUpgrade : UpgradeBase
{
    private void Awake()
    {
        upgradeAmount = 3;
    }
    public override IEnumerator _DoUpgrade()
    {
        Transform towerTwoTransform = AssetManager.Instance.buildingListSO.buildingList.FirstOrDefault(obj => obj.name == "RedBuildingTwo").prefab;
        Instantiate(towerTwoTransform, transform.position, Quaternion.identity);
        Destroy(transform.gameObject);
        yield return null;
    }
}
