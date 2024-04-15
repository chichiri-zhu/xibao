using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//冠状病毒自爆
public class SelfExplosionHit : HitBase
{
    public override void OnStart()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void DoHit()
    {
        _Explosion();
        Destroy(gameObject);
    }

    private void _Explosion()
    {
        LayerMask layerMask = 0;
        string[] layerNames = { "Soldier", "Building", "Player" };
        foreach (string layerName in layerNames)
        {
            int layerIndex = LayerMask.NameToLayer(layerName);
            layerMask |= 1 << layerIndex;
        }
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, 3f, layerMask);
        foreach (Collider2D collider in collider2DArray)
        {
            UnitBase unit = collider.GetComponent<UnitBase>();
            if(unit != null)
            {
                DoDamage(unit);
            }
        }
    }
}
