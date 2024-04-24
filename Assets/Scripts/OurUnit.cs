using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurUnit : UnitBase
{
    private SoldierBase soldier;
    public override void OnStart()
    {
        soldier = GetComponent<SoldierBase>();
    }

    public override void OnUpdate()
    {

    }

    public override bool CanLookFor()
    {
        return soldier.GetSoldierStatus() != SoldierStatus.Death;
    }
}
