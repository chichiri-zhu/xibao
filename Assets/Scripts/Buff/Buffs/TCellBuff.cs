using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCellBuff : BuffBase
{

    // 作用目标，即被添加Buff的角色
    private Enemy enemy;

    // 初始化Buff的属性和状态
    public override void Initialize(GameObject owner, string provider)
    {
        base.Initialize(owner, provider);

        // 获取作用目标的PlayerController组件
        //playerController = owner.GetComponent<PlayerController>();
        enemy = owner.GetComponent<Enemy>();

        // 设置Buff的基本属性
        MaxDuration = 2; // 最大持续时间为15秒
        TimeScale = 1f;   // 时间流失速度为正常值
        MaxLevel = int.MaxValue;     // 最大等级为5级
        BuffType = BuffType.Debuff; // Buff类型为增益效果
        ConflictResolution = ConflictResolution.combine; // Buff冲突时采用合并方式
        Dispellable = false; // 不可被驱散
        Name = "辅助T细胞";   // Buff的名称
        Description = $"降低30%的攻击速度和移动速度，持续2秒"; // Buff的描述
        Demotion = int.MaxValue; // 每次Buff持续时间结束时降低的等级
        IconPath = "Icon/2003"; // Buff的图标路径
    }

    private float atkSpeedAmount = 0.3f;
    private float moveSpeedAmount = 3f;
    public override void OnGet()
    {
        AttributeSystem attributeSystem = enemy.GetAttributeSystem();
        //attributeSystem.AddAttributeAmount(Attribute.AtkSpeed, -1f * atkSpeedAmount);
        //attributeSystem.AddAttributeAmount(Attribute.MoveSpeed, -1f * moveSpeedAmount);
        attributeSystem.AddAttributeAmountPercent(Attribute.AtkSpeed, -30f);
        attributeSystem.AddAttributeAmountPercent(Attribute.MoveSpeed, -30f);
    }

    public override void OnLost()
    {
        AttributeSystem attributeSystem = enemy.GetAttributeSystem();
        //attributeSystem.AddAttributeAmount(Attribute.AtkSpeed, +1f * atkSpeedAmount);
        //attributeSystem.AddAttributeAmount(Attribute.MoveSpeed, +1f * moveSpeedAmount);
        attributeSystem.AddAttributeAmountPercent(Attribute.AtkSpeed, 30f);
        attributeSystem.AddAttributeAmountPercent(Attribute.MoveSpeed, 30f);
    }
}
