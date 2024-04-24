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
        BuildingManager.Instance.Build(AssetManager.Instance.buildingListSO.buildingList.FirstOrDefault(obj => obj.name == "RedBuildingTwo"), transform.position);
        BuildingManager.Instance.RemoveBuilding(GetComponent<BuildingBase>());
        yield return null;
    }
}
