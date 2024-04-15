using UnityEngine;

public class Buff003 : BuffBase
{
    PlayerController playerController;
    public override void Initialize(GameObject owner, string provider)
    {
        base.Initialize(owner, provider);
        TimeScale = 0f;// 时间缩放为0，暂停游戏中的时间流逝
        MaxLevel = int.MaxValue;// 最大等级设置为int的最大值，表示无限等级
        BuffType = BuffType.Buff;// Buff类型为增益效果
        ConflictResolution = ConflictResolution.separate;// Buff冲突时采用分离方式
        Dispellable = false;// 不可被驱散
        Name = "盛宴";
        Description = "增加生命值";
        IconPath = "Icon/Feast";
        Demotion = 0;// 每次Buff持续时间结束时降低的等级
        playerController = owner.GetComponent<PlayerController>();
    }

    // 当Buff等级发生变化时触发
    protected override void OnLevelChange(int change)
    {
        // 根据变化的等级调整角色的生命值
        //playerController.HP += change;
    }
}
