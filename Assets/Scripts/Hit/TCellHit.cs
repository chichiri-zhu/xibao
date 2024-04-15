using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCellHit : CommonHit
{
    [SerializeField] private Transform sendTransform;
    public override void DoDamage(UnitBase unit)
    {
        TCellBullet.Create(sendTransform.position, unit, 10, GetComponent<UnitBase>());
    }
}
