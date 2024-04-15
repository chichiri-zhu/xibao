using UnityEngine;

public class EnemyState_Move : EnemyState
{
    public EnemyState_Move(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void OnEnter()
    {
        stateMachine.enemy.SetWalk();
    }
    public override void OnPhysicsUpdate()
    {

    }
    public override void OnUpdate()
    {
        if (!stateMachine.enemy.isMoving) stateMachine.SwitchState((int)EnemyStateEnum.Idle);
    }
    //没有离开方法，因为在移动状态中，进入会直接切换动画
    //一般来说，离开和进入方法只要有一个存在就行了，如果有一些专属的参数之类的需要清理则会用离开方法来清理
}