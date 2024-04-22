using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public PlayerStateMachine StateMachine;
    private Player player;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        //这里直接把info绑玩家身上了，数据类还有其他获取方法
        player = GetComponent<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        StateMachine = new(player, this);
        //添加状态类
        StateMachine.AddState((int)PlayerStateEnum.Move, new PlayerState_Move(StateMachine));
        StateMachine.AddState((int)PlayerStateEnum.Idle, new PlayerState_Idle(StateMachine));
        StateMachine.AddState((int)PlayerStateEnum.Building, new PlayerState_Building(StateMachine));
        //记得设置初始状态机
        StateMachine.Begin((int)PlayerStateEnum.Idle);
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
        //Debug.Log(Time.fixedDeltaTime);
        StateMachine.FixedUpdate();
    }

    public void SetPosition(Vector3 position)
    {
        if(navMeshAgent != null)
        {
            navMeshAgent.Warp(position);
        }
        else
        {
            player.transform.position = position;
        }
    }

    public void BuildingStart()
    {
        BuildingPlace buildingPlace = player.GetOverBuildingPlace();
        if (buildingPlace != null)
        {
            player.BuildingStart(buildingPlace);
            buildingPlace.StartBuilding();
        }
    }

    public void BuildingCancel()
    {
        player.BuildingCancel();
    }
}
