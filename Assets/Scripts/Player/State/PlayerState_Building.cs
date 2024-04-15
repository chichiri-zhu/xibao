using UnityEngine;

public class PlayerState_Building : PlayerState
{
    public PlayerState_Building(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void OnEnter()
    {
        //进入状态，玩家开始播放待机动画
        //stateMachine.playerController.PlayAnimation("Idle");
        Debug.Log("Building enter");
        stateMachine.player.SetIdle();
    }
    public override void OnPhysicsUpdate()
    {
    }
    public override void OnUpdate()
    {

    }
}