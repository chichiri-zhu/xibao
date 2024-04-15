using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBase : MonoBehaviour
{
    [SerializeField] private ArmsSO soldier;
    protected AttributeSystem attributeSystem;
    protected HealthSystem healthSystem;
    public bool isMoving;
    public Animator animator;

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
            AnimationEvents animationEvents = animator.GetComponent<AnimationEvents>();
            animationEvents.OnCustomEvent += AnimationEvents_OnCustomEvent;
        }
    }

    private void Start()
    {
        healthSystem = transform.GetComponent<HealthSystem>();
        OnStart();
    }

    public virtual void OnStart()
    {

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

    private void OnDestroy()
    {
        SoldierManager.Instance.RemoveSoldier(this);
        if (animator != null)
        {
            AnimationEvents animationEvents = animator.GetComponent<AnimationEvents>();
            animationEvents.OnCustomEvent -= AnimationEvents_OnCustomEvent;
        }
    }
}
