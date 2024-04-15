using System;
using UnityEngine;

//public class OnEquipSetEventArgs : EventArgs
//{
//    public SoldierData soldier;
//    public EquipData equip;
//}

public class OnDamagedArgs : EventArgs
{
    public int damageAmount;
    public DamageRes damageRes;
    public UnitBase sourceUnit;
}

public class OnBuildEndArgs : EventArgs
{
    public BuildingPlace buildingPlace;
}

public class OnChangePageArgs : EventArgs
{
    public int currentPage;
}

public class OnTalentChooseArgs : EventArgs
{
    public TalentSO talent;
}

public class OnTalentAddArgs : EventArgs
{
    public TalentSO talent;
}

public class OnAttributeAmountUpdateArgs : EventArgs
{
    public Attribute attribute;
    public float amount;
}

public class OnDoDamageArgs : EventArgs
{
    public SoldierBase soldier;
    public UnitBase targetUnit;
}

public class DoHitArgs : EventArgs
{
    public UnitBase targetUnit;
}

public class OnSoldierCreateArgs : EventArgs
{
    public SoldierBase soldierBase;
}


