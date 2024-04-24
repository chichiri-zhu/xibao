using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUnit : UnitBase
{
    private BuildingBase building;

    public override void OnStart()
    {
        building = GetComponent<BuildingBase>();
    }

    public override void OnUpdate()
    {

    }

    public override bool CanLookFor()
    {
        return building.GetBuildingStatus() != BuildingStatus.Destroy;
    }
}
