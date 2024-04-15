using UnityEngine;
public class Buff004 : BuffBase
{
    PlayerController playerController;
    // 每秒受到的伤害值
    float m_DamagePerSeconds = 30;
    public override void Initialize(GameObject owner, string provider)
    {
        base.Initialize(owner, provider);

        playerController = owner.GetComponent<PlayerController>();

        MaxDuration = 5f;// Buff的最大持续时间为5秒
        TimeScale = 1f;// 时间缩放为1，正常流逝时间
        MaxLevel = 5;// 最大等级设置为5
        BuffType = BuffType.Debuff;// Buff类型为减益效果
        ConflictResolution = ConflictResolution.separate;// Buff冲突时采用分离方式
        Dispellable = true;// 可以被驱散
        Name = "流血";
        Description = "每层每秒受到30点伤害";
        IconPath = "Icon/Darius_PassiveBuff";
        Demotion = MaxLevel;// 每次Buff持续时间结束时降低的等级
    }

    // 当Buff等级发生变化时触发
    protected override void OnLevelChange(int change)
    {
        //每次升级，重置Buff的当前剩余时间
        ResidualDuration = MaxDuration;
    }
    public override void FixedUpdate()
    {
        // 根据当前等级、每秒伤害值和固定时间步长来计算角色受到的伤害
        //playerController.HP -= m_DamagePerSeconds * CurrentLevel * BuffManager.FixedDeltaTime;
    }
}
