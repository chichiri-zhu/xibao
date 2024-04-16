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
        Transform buildingTransform = Instantiate(buildingType.prefab, buildingPlace.transform.position, Quaternion.identity);
        buildingTransform.SetParent(buildingTransform);
        BuildingBase buildingBase = buildingTransform.GetComponent<BuildingBase>();
        if(buildingBase != null && !buildingList.Contains(buildingBase))
        {
            buildingList.Add(buildingBase);
        }
        OnBuildEnd?.Invoke(this, new OnBuildEndArgs { buildingPlace = buildingPlace });
        return buildingBase;
    }
}
