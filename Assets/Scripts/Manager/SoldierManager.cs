using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoldierManager : SingleBase<SoldierManager>
{
    [SerializeField] private List<Soldier> soldierList;
    [SerializeField] private Transform soldiersTransform;

    public event EventHandler OnSoldierAdd;

    public Transform AddSoldier(ArmsSO soldierType, Vector2 position)
    {
        Transform soldier = Instantiate(soldierType.prefab, position, Quaternion.identity);
        soldier.SetParent(soldiersTransform);
        Soldier soldierBase = soldier.GetComponent<Soldier>();
        soldierList.Add(soldierBase);
        OnSoldierAdd?.Invoke(this, EventArgs.Empty);
        return soldier;
    }

    public void RemoveSoldier(Soldier soldier)
    {
        if(soldier != null && soldierList.Contains(soldier))
        {
            soldierList.Remove(soldier);
        }
    }

    public List<Soldier> GetSoldierList()
    {
        return soldierList;
    }
}
