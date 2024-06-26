using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum SoldierStatus
{
    Default,
    Death
}

public enum SoldierSource
{
    Owner,
    Monster
}

public class SoldierBase : MonoBehaviour
{
    [SerializeField] protected ArmsSO soldier;
    public Transform body;
    public SoldierSource soldierSource;
    [SerializeField] public Collider2D collider2d;
    protected AttributeSystem attributeSystem;
    protected HealthSystem healthSystem;
    public bool isMoving;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    protected SoldierStatus soldierStatus;

    public event EventHandler<DoHitArgs> OnDoHit;

    public virtual void Awake()
    {
        attributeSystem = GetComponent<AttributeSystem>();
        if(attributeSystem == null)
        {
            attributeSystem = AttributeSystem.Create(this);
        }
        if(animator != null)
        {
            if(soldierSource == SoldierSource.Owner)
            {
                AnimationEvents animationEvents = animator.GetComponent<AnimationEvents>();
                animationEvents.OnCustomEvent += AnimationEvents_OnCustomEvent;
            }
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        if(navMeshAgent != null)
        {
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
        }

        if(collider2d == null)
        {
            collider2d = GetComponent<Collider2D>();
            if(collider2d == null)
            {
                collider2d = GetComponentInChildren<Collider2D>();
            }
        }
    }

    private void Start()
    {
        healthSystem = transform.GetComponent<HealthSystem>();
        if(attributeSystem != null)
        {
            attributeSystem.OnAttributeAmountUpdate += AttributeSystem_OnAttributeAmountUpdate;
        }
        OnStart();
        InitRof(attributeSystem.GetAttributeParam().Rof);
    }

    private void AttributeSystem_OnAttributeAmountUpdate(object sender, OnAttributeAmountUpdateArgs e)
    {
        if(e.attribute == Attribute.Rof)
        {
            InitRof(e.amount);
        }
    }

    public virtual void OnStart()
    {

    }

    public void InitRof(float rof)
    {
        if(navMeshAgent != null)
        {
            navMeshAgent.stoppingDistance = rof;
        }
    }

    public float GetMoveSpeed()
    {
        return attributeSystem.GetMoveSpeed();
    }

    public float GetAtk()
    {
        return attributeSystem.GetAtk();
    }

    public void SetAttributeSystem(AttributeSystem attributeSystem)
    {
        this.attributeSystem = attributeSystem;
    }

    public AttributeSystem GetAttributeSystem()
    {
        return attributeSystem;
    }

    public ArmsSO GetArms()
    {
        return soldier;
    }

    public virtual UnitBase GetTargetUnit()
    {
        MoveAndHit moveAndHit = GetComponent<MoveAndHit>();
        if(moveAndHit != null)
        {
            return moveAndHit.GetTargetUnit();
        }
        else
        {
            return null;
        }
    }

    public void DoHit(UnitBase targetUnit)
    {
        if(soldierStatus == SoldierStatus.Death)
        {
            return;
        }
        OnDoHit?.Invoke(this, new DoHitArgs { targetUnit = targetUnit });
    }

    public virtual void SetIdle()
    {
        
    }

    public virtual void SetWalk()
    {
        
    }

    public virtual void SetHit()
    {

    }

    private void AnimationEvents_OnCustomEvent(string obj)
    {
        if (obj == "Hit")
        {
            _DoHit();
        }
    }

    protected virtual void _DoHit()
    {
        GetComponent<HitBase>().DoDamage();
    }

    public SoldierStatus GetSoldierStatus()
    {
        return soldierStatus;
    }

    public void Move(Vector3 pos)
    {
        float agentOffset = 0.01f;
        Vector3 agentPos = (Vector3)(agentOffset * UnityEngine.Random.insideUnitCircle) + pos;
        if(navMeshAgent != null)
        {
            if (navMeshAgent.isStopped)
            {
                navMeshAgent.isStopped = false;
            }
            navMeshAgent.SetDestination(agentPos);
        }
    }

    protected virtual void OnDestroy()
    {
        //SoldierManager.Instance.RemoveSoldier(this);
        if (animator != null)
        {
            if (soldierSource == SoldierSource.Owner)
            {
                AnimationEvents animationEvents = animator.GetComponent<AnimationEvents>();
                animationEvents.OnCustomEvent -= AnimationEvents_OnCustomEvent;
            }
        }
    }
}
