using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SoldierBase
{
    public PlayerController playerController;
    private Rigidbody2D rigidbody2d;
    public Vector2 movePositionDir;

    private bool isBuilding = false;
    private BuildingPlace currentBuildingPlace;

    public override void Awake()
    {
        base.Awake();
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    public BuildingPlace GetOverBuildingPlace()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Place"));

        //if(hit.collider != null)
        //{
        //    return hit.collider.GetComponent<BuildingPlace>();
        //}
        //else
        //{
        //    return null;
        //}
        return triggerPlace;
    }

    public BuildingBase GetOverBuilding()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Building"));

        //if (hit.collider != null)
        //{
        //    return hit.collider.GetComponent<BuildingBase>();
        //}
        //else
        //{
        //    return null;
        //}
        return triggerBuilding;
    }

    private BuildingBase triggerBuilding;
    private BuildingPlace triggerPlace;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            triggerBuilding = collision.GetComponent<BuildingBase>();
        }else if (collision.gameObject.CompareTag("Place"))
        {
            triggerPlace = collision.GetComponent<BuildingPlace>();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        triggerBuilding = null;
        triggerPlace = null;
    }

    public bool IsOverPlace()
    {
        return GetOverBuildingPlace() != null;
    }

    public void SetMoveDir(Vector2 moveInput)
    {
        if(moveInput == Vector2.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
            movePositionDir = moveInput;
            moveInput.Normalize();
            Turn(moveInput.x);
        }
    }

    public void BuildingStart(BuildingPlace buildingPlace)
    {
        isBuilding = true;
        currentBuildingPlace = buildingPlace;
    }

    public void BuildingCancel()
    {
        isBuilding = false;
        if (currentBuildingPlace != null)
        {
            //判断是否完成 等待选择天赋
            if (!currentBuildingPlace.isBuildingFinish)
            {
                currentBuildingPlace.CancelBuilding();
            }
        }
    }

    public void Turn(float direction)
    {
        transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }

    public override void SetIdle()
    {
        animator.SetInteger("State", 0);
    }

    public override void SetWalk()
    {
        animator.SetInteger("State", 1);
    }

    public override void SetHit()
    {
        animator.SetTrigger("Hit");
    }

    protected override void _DoHit()
    {
        UnitBase tUnit = GetTargetUnit();
        if(tUnit != null)
        {
            GetComponent<HitBase>().DoDamage(tUnit);
        }
    }

    public override UnitBase GetTargetUnit()
    {
        AttributeParam attributeParam = attributeSystem.GetAttributeParam();
        LayerMask layerMask = LayerMask.GetMask("Enemy");
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, attributeParam.Rof, layerMask);
        if (collider2DArray.Length > 0)
        {
            UnitBase unit = null;
            foreach (Collider2D collider in collider2DArray)
            {
                if (unit != null)
                {
                    if (Vector2.Distance(transform.position, collider.transform.position) <
                       Vector2.Distance(transform.position, unit.transform.position))
                    {
                        unit = collider.GetComponent<UnitBase>();
                    }
                }
                else
                {
                    unit = collider.GetComponent<UnitBase>();
                }
            }
            return unit;
        }
        else
        {
            return null;
        }
    }
}
