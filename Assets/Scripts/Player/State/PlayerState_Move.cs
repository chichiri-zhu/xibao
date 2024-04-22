using UnityEngine;

public class PlayerState_Move : PlayerState
{
    public PlayerState_Move(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void OnEnter()
    {
        Debug.Log("Move begin");
        stateMachine.player.SetWalk();
    }
    public override void OnPhysicsUpdate()
    {
        //物理上，在这个状态，玩家不动，速度为0
        //stateMachine.playerController.SetVelocity(Vector2.zero);
        Vector3 position = stateMachine.playerController.transform.position;
        position = position + stateMachine.player.GetMoveSpeed() * stateMachine.player.movePositionDir * Time.fixedDeltaTime;
        stateMachine.playerController.SetPosition(position);
    }
    public override void OnUpdate()
    {
        //检测到玩家移动了，那么切换到移动状态
        if (!stateMachine.player.isMoving) stateMachine.SwitchState((int)PlayerStateEnum.Idle);
    }
}