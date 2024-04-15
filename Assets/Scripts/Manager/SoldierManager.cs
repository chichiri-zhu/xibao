using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoldierManager : SingleBase<SoldierManager>
{
    [SerializeField] List<SoldierBase> soldierList;

    public event EventHandler OnSoldierAdd;

    public Transform AddSoldier(ArmsSO soldierType, Vector2 position)
    {
        Transform soldier = Instantiate(soldierType.prefab, position, Quaternion.identity);
        SoldierBase soldierBase = soldier.GetComponent<SoldierBase>();
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
