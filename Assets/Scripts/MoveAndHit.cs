using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum LookForTargetType
{
    All,
    Soldier,
    Building
}

public class MoveAndHit : MonoBehaviour
{
    protected SoldierBase soldier;
    protected UnitBase targetUnit;
    protected UnitBase hitTargetUnit;
    protected float defaultFindRof = 3;
    [SerializeField] protected LookForTargetType lookForTargetType = LookForTargetType.All;
    public int turnAmend = 1;
    protected NavMeshAgent navMeshAgent;

    public Vector2 lookDirection;

    protected void Awake()
    {
        soldier = GetComponent<SoldierBase>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        OnAwake();
    }

    protected virtual void OnAwake()
    {

    }

    protected void Start()
    {
        //targetUnit = GameManager.Instance.GetMainBuilding().GetComponent<UnitBase>();
        OnStart();
    }

    protected virtual void OnStart()
    {

    }

    protected void Update()
    {
        HandleFindTarget();
        HandleMoveAndHit();
    }

    protected float lookForTargetTimer;
    protected float lookForTargetTimerMax = .2f;
    public void HandleFindTarget()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTarget();
        }
    }

    public virtual void LookForTarget()
    {
        if (targetUnit == null)
        {
            targetUnit = GameManager.Instance.GetMainBuilding()?.GetComponent<UnitBase>();
        }
    }

    public virtual void HandleMoveAndHit()
    {
        if(targetUnit != null)
        {
            float distance = UtilsClass.ColliderDistance(targetUnit.collider2d, soldier.collider2d);
            lookDirection = targetUnit.transform.position - transform.position;
            lookDirection.Normalize();

            AttributeParam attributeParam = soldier.GetAttributeSystem().GetAttributeParam();
            if (distance <= attributeParam.Rof)
            {
                //Hit
                DoHit();
            }
            else
            {
                //Move
                soldier.SetWalk();
                if(navMeshAgent != null)
                {
                    //navMeshAgent.SetDestination(targetUnit.transform.position);
                    soldier.Move(targetUnit.transform.position);
                }
                else
                {
                    Vector2 position = transform.position;
                    position = position + soldier.GetMoveSpeed() * lookDirection * Time.deltaTime;
                    transform.position = position;
                }
            }
            Turn(lookDirection.x);
        }
    }

    protected void DoHit()
    {
        hitTargetUnit = targetUnit;
        soldier.DoHit(targetUnit);
    }

    public void Turn(float direction)
    {
        //transform.localScale = new Vector3(Mathf.Sign(direction) * turnAmend, 1, 1);
        Vector3 localScale = soldier.body.localScale;
        soldier.body.localScale = new Vector3(Mathf.Sign(direction) * turnAmend * Mathf.Abs(localScale.x), localScale.y, localScale.z);
    }

    public UnitBase GetTargetUnit()
    {
        return targetUnit;
    }
}
