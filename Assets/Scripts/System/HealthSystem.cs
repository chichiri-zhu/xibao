using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int healthAmountBase;
    private int healthAmountMax;
    private int healthAmount;
    private bool isBuilding;

    private SoldierBase soldierBase;
    private UnitBase unit;

    public event EventHandler<OnDamagedArgs> OnDamaged;
    public event EventHandler OnDied;
    public event EventHandler OnHpRecover;

    private AttributeSystem attributeSystem;

    private void Awake()
    {
        soldierBase = GetComponent<SoldierBase>();
        unit = GetComponent<UnitBase>();
        BuildingBase buildingbase = GetComponent<BuildingBase>();
        if (buildingbase != null)
        {
            //建筑
            Initialize((int)buildingbase.GetBuildingType().attributeParam.Hp, true);
        }
        else
        {
            //士兵
            SoldierBase soldier = GetComponent<SoldierBase>();
            if(soldier != null)
            {
                Initialize((int)soldier.GetArms().attributeParam.Hp, false);
            }
        }
    }

    private void Start()
    {
        attributeSystem = GetComponent<AttributeSystem>();
    }

    public void Initialize(int Hp, bool isBuilding)
    {
        healthAmountBase = Hp;
        healthAmountMax = Hp;
        this.healthAmount = Hp;
        this.isBuilding = isBuilding;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }

    public bool IsFullHealth()
    {
        return healthAmount >= healthAmountMax;
    }

    public void Damage(int damageAmount, UnitBase sourceUnit = null, DamageRes damageRes = DamageRes.Default)
    {
        //// 伤害飘字
        //_FontPoint(damageAmount, damageRes);

        _DoDamage(damageAmount);

        OnDamaged?.Invoke(this, new OnDamagedArgs { damageAmount = damageAmount, damageRes = damageRes, sourceUnit = sourceUnit });

        _VerifyDead(sourceUnit);
    }

    public void HpRecover(int amount, bool isPoint = true)
    {
        //float recoverEffectPercent = 0;
        //float recoverExt = 0;
        //if (fightAttributeSystem != null)
        //{
        //    recoverEffectPercent = fightAttributeSystem.GetFightAttributeAmount(FightAttributeType.RecoverEffectPercent);
        //    recoverExt = fightAttributeSystem.GetFightAttributeAmount(FightAttributeType.RecoverExt);
        //}

        //amount += (int)(recoverEffectPercent / 100 * amount);
        //amount += (int)recoverExt;

        healthAmount += amount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        //if (isPoint)
        //{
        //    _RecoverPoint(amount);
        //}

        OnHpRecover?.Invoke(this, EventArgs.Empty);
    }

    private void _FontPoint(int damageAmount, DamageRes damageRes = DamageRes.Default)
    {
        if (damageAmount <= 0 && damageRes != DamageRes.FullBlock)
        {
            return;
        }
        Transform fontPointTransform = Instantiate(AssetManager.Instance.fontPointPF, transform.position, Quaternion.identity);
        FontPoint fontPoint = fontPointTransform.GetComponent<FontPoint>();
        String textString = damageAmount.ToString();

        //fontPoint.text.text = textString;
        fontPoint.text.SetText(textString);
        // 我放受伤红色 敌方受伤白色 暴击黄色
        if (unit != null && unit.GetType() == typeof(EnemyUnit))
        {
            //敌方受伤
            fontPoint.text.color = Color.white;
        }
        else
        {
            //我方受伤
            fontPoint.text.color = Color.red;
        }

        if (damageRes == DamageRes.Crt)
        {
            //暴击
            fontPoint.text.color = Color.yellow;
            //fontPoint.text.characterSize = 1;
            fontPoint.text.fontSize = 1;
        }
    }

    private void _DoDamage(int damageAmount)
    {
        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
    }

    private void _VerifyDead(UnitBase sourceUnit = null)
    {
        if (IsDead())
        {
            //判断是否复活
            //TODO

            //
            Dead();
        }
    }

    public bool IsDead()
    {
        return healthAmount <= 0;
    }

    public void Dead()
    {
        if(soldierBase != null && soldierBase.GetSoldierStatus() == SoldierStatus.Death)
        {
            return;
        }
        OnDied?.Invoke(this, EventArgs.Empty);
    }

    public void Revive()
    {
        HpRecover(healthAmountMax);
    }
}
