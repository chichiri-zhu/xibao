public class EnemyState : IState
{
    //玩家的状态机
    protected EnemyStateMachine stateMachine;

    public EnemyState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
    public virtual void OnPhysicsUpdate() { }
}