using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStateMachine StateMachine;
    private Enemy enemy;

    private void Start()
    {
        //这里直接把info绑玩家身上了，数据类还有其他获取方法
        enemy = GetComponent<Enemy>();
        StateMachine = new(enemy, this);
        //添加状态类
        StateMachine.AddState((int)EnemyStateEnum.Idle, new EnemyState_Idle(StateMachine));

        //记得设置初始状态机
        StateMachine.Begin((int)EnemyStateEnum.Idle);
    }
    //言简意赅，将状态机的更新提上来
    public void Update()
    {
        StateMachine.Update();
    }
    //如果你的状态机有物理更新，不要忘了他
    //我是把移动速度赋值逻辑写在了移动状态，所以这里没有任何移动逻辑
    public void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }
}
