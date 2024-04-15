using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttributeSystem : MonoBehaviour
{
    public static AttributeSystem Create(SoldierBase soldier)
    {
        AttributeSystem attributeSystem = soldier.gameObject.AddComponent<AttributeSystem>();
        soldier.SetAttributeSystem(attributeSystem);
        attributeSystem.SetSoldier(soldier);
        return attributeSystem;
    }

    public static AttributeSystem Create(BuildingBase building)
    {
        AttributeSystem attributeSystem = building.gameObject.AddComponent<AttributeSystem>();
        attributeSystem.SetBuilding(building);
        return attributeSystem;
    }

    private SoldierBase soldier;
    private BuildingBase building;
    private AttributeParam defaultAttributeParam;
    private AttributeParam attributeParam;

    public event EventHandler<OnAttributeAmountUpdateArgs> OnAttributeAmountUpdate;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        building = GetComponent<BuildingBase>();
        if(building != null)
        {
            SetBuilding(building);
        }
        else
        {
            soldier = GetComponent<SoldierBase>();
            if(soldier != null)
            {
                SetSoldier(soldier);
            }
        }
    }

    public void SetSoldier(SoldierBase soldier)
    {
        this.soldier = soldier;
        defaultAttributeParam = soldier.GetArms().attributeParam;
        attributeParam = defaultAttributeParam;
    }

    public void SetBuilding(BuildingBase building)
    {
        this.building = building;
        defaultAttributeParam = building.GetBuildingType().attributeParam;
        attributeParam = defaultAttributeParam;
    }

    public float GetMoveSpeed()
    {
        return attributeParam.MoveSpeed;
    }

    public float GetAtk()
    {
        return attributeParam.Atk;
    }

    public void AddAttributeAmount(Attribute attributeKey, float amount)
    {
        switch (attributeKey)
        {
            case Attribute.Hp:
                attributeParam.Hp += amount;
                break;
            case Attribute.Atk:
                attributeParam.Atk += amount;
                break;
            case Attribute.Def:
                attributeParam.Def += amount;
                break;
            case Attribute.Rof:
                attributeParam.Rof += amount;
                break;
            case Attribute.AtkSpeed:
                attributeParam.AtkSpeed += amount;
                break;
            case Attribute.MoveSpeed:
                attributeParam.MoveSpeed += amount;
                break;
            case Attribute.Dmg:
                attributeParam.Dmg += amount;
                break;
            case Attribute.ParasiteDmgAmount:
                attributeParam.ParasiteDmgAmount += amount;
                break;
            case Attribute.RemoteDmgDec:
                attributeParam.RemoteDmgDec += amount;
                break;
        }

        OnAttributeAmountUpdate?.Invoke(this, new OnAttributeAmountUpdateArgs { attribute = attributeKey, amount = amount });
    }

    public void AddAttributeAmountPercent(Attribute attributeKey, float amount)
    {
        float aAmount = 0;
        switch (attributeKey)
        {
            case Attribute.Hp:
                aAmount += defaultAttributeParam.Hp * amount / 100;
                break;
            case Attribute.Atk:
                aAmount += defaultAttributeParam.Atk * amount / 100;
                break;
            case Attribute.Def:
                aAmount += defaultAttributeParam.Def * amount / 100;
                break;
            case Attribute.Rof:
                aAmount += defaultAttributeParam.Rof * amount / 100;
                break;
            case Attribute.AtkSpeed:
                aAmount += defaultAttributeParam.AtkSpeed * amount / 100;
                break;
            case Attribute.MoveSpeed:
                aAmount += defaultAttributeParam.MoveSpeed * amount / 100;
                break;
            case Attribute.Dmg:
                aAmount += defaultAttributeParam.Dmg * amount / 100;
                break;
            case Attribute.ParasiteDmgAmount:
                attributeParam.ParasiteDmgAmount += defaultAttributeParam.ParasiteDmgAmount * amount / 100; ;
                break;
            case Attribute.RemoteDmgDec:
                attributeParam.RemoteDmgDec += defaultAttributeParam.RemoteDmgDec * amount / 100; ;
                break;
        }

        if(aAmount != 0)
        {
            AddAttributeAmount(attributeKey, aAmount);
        }
    }

    public AttributeParam GetAttributeParam()
    {
        return attributeParam;
    }
}
