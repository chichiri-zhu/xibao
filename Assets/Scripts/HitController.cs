using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    private Player player;
    private HitBase hitBase;

    private void Awake()
    {
        hitBase = GetComponent<HitBase>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        _HandleHit();
    }

    private void _HandleHit()
    {
        AttributeParam attributeParam = player.GetAttributeSystem().GetAttributeParam();
        LayerMask layerMask = LayerMask.GetMask("Enemy");
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position,  attributeParam.Rof, layerMask);
        if(collider2DArray.Length > 0)
        {
            //hitBase.DoHit();
            player.DoHit(player.GetTargetUnit());
        }
    }
}
