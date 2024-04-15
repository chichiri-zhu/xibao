using UnityEngine;
public class Buff005 : BuffBase
{
    PlayerController playerController;

    // 每秒受到的伤害值
    float m_DamagePerSeconds = 10;

    public override void Initialize(GameObject owner, string provider)
    {
        base.Initialize(owner, provider);

        playerController = owner.GetComponent<PlayerController>();

        MaxDuration = 1f;// Buff的最大持续时间为1秒
        TimeScale = 1f;// 时间缩放为1，正常流逝时间
        MaxLevel = int.MaxValue;// 最大等级设置为int.MaxValue，即无限大
        BuffType = BuffType.Debuff;// Buff类型为减益效果
        ConflictResolution = ConflictResolution.combine;// Buff冲突时采用合并方式
        Dispellable = true;// 可以被驱散
        Name = "被点燃";
        Description = "每秒受到10点伤害,首次受到该BUFF伤害,一次叠加2层,后续叠加1层";
        IconPath = "Icon/Darius_PassiveBuff";
        Demotion = 1;// 每次Buff持续时间结束时降低的等级
    }

    public override void FixedUpdate()
    {
        // 根据每秒伤害值和固定时间步长来计算角色受到的伤害
        //playerController.HP -= m_DamagePerSeconds * BuffManager.FixedDeltaTime;
    }
}
