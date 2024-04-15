using System;
[System.Serializable]
public struct AttributeParam
{
    public float Hp;
    public float Atk;           //攻击
    public float Def;           //防御
    public float Rof;           //攻击距离
    public float AtkSpeed;      //攻速
    public float MoveSpeed;     //移动速度
    public float Dmg;           //伤害加成(百分比)
    public float ParasiteDmgAmount;//寄生虫伤害加成
    public float RemoteDmgDec;  //远程伤害减免
}
