using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HongxueqiuxiaoyingTalent : TalentBase
{
    private void Start()
    {
        GameManager.Instance.OnPrepareStart += Instance_OnPrepareStart;
    }

    private void Instance_OnPrepareStart(object sender, System.EventArgs e)
    {
        List<BuildingBase> buildingList = BuildingManager.Instance.GetBuildingList();
        BuildingBase redBuilding = buildingList.FirstOrDefault(obj => obj != null && obj.GetComponent<BuildingBase>().GetBuildingType().name == "RedBuilding");
        Debug.Log(redBuilding);
        if(redBuilding != null)
        {
            UpgradeBase upgrade = redBuilding.GetComponent<UpgradeBase>();
            if (upgrade != null)
            {
                StartCoroutine(upgrade._DoUpgrade());
            }
        }
    }
}
