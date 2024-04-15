using UnityEngine;

public class Buff001 : BuffBase
{
    // Buff每秒钟恢复的生命值
    private float m_HealingPerSecond = 20f;

    // 作用目标，即被添加Buff的角色
    private PlayerController playerController;

    // 初始化Buff的属性和状态
    public override void Initialize(GameObject owner, string provider)
    {
        base.Initialize(owner, provider);

        // 获取作用目标的PlayerController组件
        //playerController = owner.GetComponent<PlayerController>();

        // 设置Buff的基本属性
        MaxDuration = 15; // 最大持续时间为15秒
        TimeScale = 1f;   // 时间流失速度为正常值
        MaxLevel = 5;     // 最大等级为5级
        BuffType = BuffType.Buff; // Buff类型为增益效果
        ConflictResolution = ConflictResolution.combine; // Buff冲突时采用合并方式
        Dispellable = false; // 不可被驱散
        Name = "生命值";   // Buff的名称
        Description = $"每秒恢复{m_HealingPerSecond}点生命值"; // Buff的描述
        Demotion = 1; // 每次Buff持续时间结束时降低的等级
        IconPath = "Icon/2003"; // Buff的图标路径
    }

    // 在固定时间间隔内更新Buff的效果
    public override void FixedUpdate()
    {
        // 每秒钟恢复指定的生命值
        //playerController.HP += m_HealingPerSecond * BuffManager.FixedDeltaTime;
    }
}
