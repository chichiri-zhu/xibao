using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitBase : MonoBehaviour
{
    public HitType hitType = HitType.Default;
    protected SoldierBase soldier;
    protected UnitBase targetUnit;
    //protected float atk;
    protected float hitTimer;
    protected bool canHit;
    protected AttributeSystem attributeSystem;

    public event EventHandler<OnDoDamageArgs> OnDoDamage;

    private void Awake()
    {
        canHit = true;

        OnAwake();
    }

    public virtual void OnAwake()
    {

    }

    private void Start()
    {
        soldier = GetComponent<SoldierBase>();
        targetUnit = soldier.GetTargetUnit();
        attributeSystem = GetComponent<AttributeSystem>();
        UpdateAtkSpeed();
        if (soldier != null)
        {
            soldier.OnDoHit += Soldier_OnDoHit;
        }

        if (attributeSystem != null)
        {
            attributeSystem.OnAttributeAmountUpdate += FightAttributeSystem_OnAttributeUpdate;
        }

        OnStart();
    }

    private void FightAttributeSystem_OnAttributeUpdate(object sender, OnAttributeAmountUpdateArgs e)
    {
        if(e.attribute == Attribute.AtkSpeed)
        {
            UpdateAtkSpeed();
        }
    }

    private void Soldier_OnDoHit(object sender, System.EventArgs e)
    {
        targetUnit = soldier.GetTargetUnit();
        DoHit();
    }

    public virtual void OnStart()
    {

    }

    private void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void DoHit()
    {

    }

    private void OnDestroy()
    {
        if (soldier != null)
        {
            soldier.OnDoHit -= Soldier_OnDoHit;
        }

        if (attributeSystem != null)
        {
            attributeSystem.OnAttributeAmountUpdate -= FightAttributeSystem_OnAttributeUpdate;
        }
    }

    public void UpdateAtkSpeed()
    {
        AttributeParam soldierParam = attributeSystem.GetAttributeParam();
        float fixAtkSpeed = soldierParam.AtkSpeed;
        hitTimer = 1 / fixAtkSpeed;
        Debug.Log(hitTimer);
    }

    public int GetFinalDmg(UnitBase unit, out DamageRes damageRes, bool isReal = false, bool isCrt = false)
    {
        damageRes = DamageRes.Default;
        if (unit == null)
        {
            return 0;
        }
        AttributeParam soldierParam = attributeSystem.GetAttributeParam();
        SoldierBase targetSoldier = unit.GetComponent<SoldierBase>();
        float defaultDmg = 0;
        float finalDmg = 0;
        defaultDmg += (int)soldierParam.Atk;


        finalDmg += defaultDmg;
        if (targetSoldier != null)
        {
            ArmsSO targetSoldierArms = targetSoldier.GetArms();
            
            if (soldierParam.ParasiteDmgAmount > 0)
            {
                if (targetSoldierArms != null && targetSoldierArms.armsType == ArmsType.Parasite)
                {
                    finalDmg += soldierParam.ParasiteDmgAmount;
                }
            }
            if (soldierParam.RemoteDmgDec > 0)
            {
                //远程伤害减免
                if (targetSoldierArms != null && targetSoldierArms.attributeParam.Rof >= 4)
                {
                    finalDmg -= finalDmg * soldierParam.RemoteDmgDec / 100;
                }
            }
        }
        
        finalDmg = Mathf.Clamp(finalDmg, 0, 9999);
        return (int)finalDmg;
    }

    public int GetFinalDmg(out DamageRes damageRes, bool isReal = false, bool isCrt = false)
    {
        return GetFinalDmg(targetUnit, out damageRes, isReal, isCrt);
    }

    public bool IsCrt()
    {
        return false;
    }

    public void HandleOnDoDamage(UnitBase unit = null)
    {
        OnDoDamage?.Invoke(this, new OnDoDamageArgs() { soldier = this.soldier, targetUnit = this.targetUnit });
    }

    public virtual void DoDamage()
    {
        DoDamage(targetUnit);
    }

    public virtual void DoDamage(UnitBase unit)
    {
        if (unit != null)
        {
            HealthSystem targetHealthSystem = unit.GetComponent<HealthSystem>();
            if (targetHealthSystem != null)
            {
                DamageRes damageRes;
                int damageAmount = GetFinalDmg(unit, out damageRes);
                PlayHitSound(damageRes);
                Debug.Log(gameObject + ":" + damageAmount);
                targetHealthSystem.Damage(damageAmount, soldier.GetComponent<UnitBase>(), damageRes);
                HandleOnDoDamage(unit);
            }
        }
    }

    public void PlayHitSound(DamageRes damageRes)
    {
        if (soldier == null)
        {
            return;
        }
    }

    public UnitBase GetHitTarget()
    {
        return targetUnit;
    }
}
