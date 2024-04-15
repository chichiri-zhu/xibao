using UnityEngine;

public class Buff002 : BuffBase
{
    // 攻击力增加的数值
    private float m_ADUp = 10f;
    private PlayerController playerController;
    public override void Initialize(GameObject owner, string provider)
    {
        base.Initialize(owner, provider);
        MaxDuration = 5f;// 最大持续时间为5秒
        MaxLevel = 10;// 最大等级为10级
        BuffType = BuffType.Buff;// Buff类型为增益效果
        ConflictResolution = ConflictResolution.combine;// Buff冲突时采用合并方式
        Dispellable = false;// 不可被驱散
        Name = "借来的短剑";// Buff的名称
        Description = "每层增加10点攻击力";// Buff的描述
        IconPath = "Icon/1036";// Buff的图标路径
        Demotion = MaxLevel;// 每次Buff持续时间结束时降低的等级
        playerController = Owner.GetComponent<PlayerController>();
    }

    //当等级改变时调用
    protected override void OnLevelChange(int change)
    {
        // 根据变化的等级调整角色的攻击力
        //playerController.AD += m_ADUp * change;
        //每次升级，重置Buff的当前剩余时间
        ResidualDuration = MaxDuration;
    }
}
