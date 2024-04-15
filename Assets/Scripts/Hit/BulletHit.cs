using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : CommonHit
{
    [SerializeField] private Transform sendTransform;
    public override void DoDamage(UnitBase unit)
    {
        Debug.Log("do damage");
        Bullet.Create(sendTransform.position, unit, 10, GetComponent<UnitBase>());
    }
}
