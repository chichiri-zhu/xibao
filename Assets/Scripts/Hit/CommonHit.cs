using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonHit : HitBase
{
    public override void OnStart()
    {

    }

    public override void OnUpdate()
    {

    }

    protected Transform ArmL;

    public override void DoHit()
    {
        if (canHit == true)
        {
            StartCoroutine(Hit());
        }
    }
    [SerializeField] Transform arm;
    protected IEnumerator Hit()
    {
        canHit = false;
        soldier.SetHit();
        if (hitType == HitType.Event)
        {
            //Debug.Log("Hit");
            //soldier.animatorController.SetTrigger("Hit");
        }
        else
        {
            DoDamage();
        }
        

        yield return new WaitForSeconds(hitTimer);

        canHit = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
