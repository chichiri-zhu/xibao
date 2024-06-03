public enum GameStatus
{
    Padding,
    Transition, // 过渡
    Prepare,    // 准备
    Battle,     // 战斗
    Cleared,    // 通关
    GameOver
}

public enum GameDifficulty
{
    Easy,   //简单
    Hard,   //困难
    Hell,   //地狱
}

public enum BuildingType
{
    Asset,  //资源型
    Soldier //士兵型
}

public enum PlayerStateEnum
{
    None,
    Idle,
    Move,
    Attack,
    Building
}

public enum EnemyStateEnum
{
    None,
    Idle,
    Move,
    Attack,
    Building
}

public enum Attribute
{
    Atk,
    AtkSpeed,
    Def,
    Hp,
    MoveSpeed,
    Rof,    //攻击范围
    Crt,    //暴击
    Cdmg,   //暴击伤害
    Dmg,    //最终伤害（百分比）
    ParasiteDmgAmount,  //寄生虫附加伤害
    RemoteDmgDec,   //远程伤害减免（百分比）
}

public enum DamageRes
{
    Default,        //正常伤害
    Crt,            //暴击
    Charge,         //冲锋攻击（特殊攻击）
    Block,          //部分格挡
    FullBlock,      //完全格挡
}

public enum BuildingStatus
{
    Default,    //正常状态
    Destroy     //毁灭状态
}

public enum ArmsType
{
    Cell,   //细胞
    Germ,    //细菌
    Parasite,//寄生虫
    Virus,  //病毒
}

public enum HitType
{
    Default,
    Event
}