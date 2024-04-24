using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingManager : SingleBase<BuildingManager>
{
    [SerializeField] private Transform buildingTransform;
    private List<BuildingBase> buildingList;

    public event EventHandler<OnBuildEndArgs> OnBuildEnd;
    private void Start()
    {
        buildingList = new List<BuildingBase>();
    }

    public BuildingBase Build(BuildingTypeSO buildingType, BuildingPlace buildingPlace)
    {
        Transform building = Instantiate(buildingType.prefab, buildingPlace.transform.position, Quaternion.identity);
        building.SetParent(buildingTransform);
        BuildingBase buildingBase = building.GetComponent<BuildingBase>();
        if(buildingBase != null && !buildingList.Contains(buildingBase))
        {
            buildingList.Add(buildingBase);
        }
        OnBuildEnd?.Invoke(this, new OnBuildEndArgs { buildingPlace = buildingPlace });
        return buildingBase;
    }

    public BuildingBase Build(BuildingTypeSO buildingType, Vector3 pos)
    {
        Transform building = Instantiate(buildingType.prefab, pos, Quaternion.identity);
        building.SetParent(buildingTransform);
        BuildingBase buildingBase = building.GetComponent<BuildingBase>();
        if (buildingBase != null && !buildingList.Contains(buildingBase))
        {
            buildingList.Add(buildingBase);
        }
        OnBuildEnd?.Invoke(this, new OnBuildEndArgs { buildingPlace = null });
        return buildingBase;
    }

    public List<BuildingBase> GetBuildingList()
    {
        return buildingList;
    }

    public void RemoveBuilding(BuildingBase building)
    {
        if (buildingList.Contains(building))
        {
            buildingList.Remove(building);
        }

        Destroy(building.gameObject);
    }
}
