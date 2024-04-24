using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoldierManager : SingleBase<SoldierManager>
{
    [SerializeField] private List<SoldierBase> soldierList;
    [SerializeField] private Transform soldiersTransform;

    public event EventHandler OnSoldierAdd;

    public Transform AddSoldier(ArmsSO soldierType, Vector2 position)
    {
        Transform soldier = Instantiate(soldierType.prefab, position, Quaternion.identity);
        soldier.SetParent(soldiersTransform);
        SoldierBase soldierBase = soldier.GetComponent<SoldierBase>();
        soldierList.Add(soldierBase);
        OnSoldierAdd?.Invoke(this, EventArgs.Empty);
        return soldier;
    }

    public void RemoveSoldier(SoldierBase soldier)
    {
        if(soldier != null && soldierList.Contains(soldier))
        {
            soldierList.Remove(soldier);
        }
    }
}
