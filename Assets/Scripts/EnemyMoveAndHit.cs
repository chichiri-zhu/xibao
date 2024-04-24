using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveAndHit : MoveAndHit
{
    private LayerMask layerMask;
    protected override void OnStart()
    {
        targetUnit = GameManager.Instance.GetMainBuilding()?.GetComponent<UnitBase>();
        InitLayerMask();
        //HealthSystem healthSystem = GetComponent<HealthSystem>();
    }

    public override void LookForTarget()
    {
        AttributeParam attributeParam = soldier.GetAttributeSystem().GetAttributeParam();
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, defaultFindRof + attributeParam.Rof, layerMask);
        foreach (var collider in collider2DArray)
        {
            if(collider.gameObject == gameObject)
            {
                continue;
            }
            UnitBase unit = collider.GetComponent<UnitBase>();
            if (!unit.CanLookFor())
            {
                continue;
            }
            if(targetUnit == null || !targetUnit.CanLookFor())
            {
                targetUnit = unit;
            }
            else
            {
                if (unit != null)
                {
                    if (Vector3.Distance(transform.position, unit.transform.position) <
                        Vector3.Distance(transform.position, targetUnit.transform.position))
                    {
                        targetUnit = unit;
                    }
                }
            }
        }

        if (targetUnit == null || !targetUnit.CanLookFor())
        {
            targetUnit = GameManager.Instance.GetMainBuilding()?.GetComponent<UnitBase>();
        }
    }

    private void InitLayerMask()
    {
        string[] layerNames;
        if(lookForTargetType == LookForTargetType.All)
        {
            layerNames = new string[] { "Soldier", "Building", "Player" };
        }else if(lookForTargetType == LookForTargetType.Soldier)
        {
            layerNames = new string[] { "Soldier", "Player" };
        }else if(lookForTargetType == LookForTargetType.Building)
        {
            layerNames = new string[] { "Building" };
        }
        else
        {
            layerNames = new string[] { "Soldier", "Building", "Player" };
        }
        

        // 创建一个LayerMask变量
        layerMask = 0;

        // 遍历layerNames数组，将每个Layer的索引添加到LayerMask中
        foreach (string layerName in layerNames)
        {
            int layerIndex = LayerMask.NameToLayer(layerName);
            layerMask |= 1 << layerIndex;
        }
    }
}
