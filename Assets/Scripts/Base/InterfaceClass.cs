public interface IState
{
    public void OnEnter(); // 进入该状态时调用
    public void OnExit(); // 离开该状态时调用
    public void OnUpdate(); // 逻辑更新（Update）
    public void OnPhysicsUpdate(); // 物理更新（FixedUpdate）
}

public interface IShowUI
{
    void Show();
    void Hide();
}