using UnityEngine;

public class EnemyState_Hit : EnemyState
{
    public EnemyState_Hit(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void OnEnter()
    {
        stateMachine.enemy.SetHit();
    }
    public override void OnPhysicsUpdate()
    {

    }
    public override void OnUpdate()
    {

    }
}