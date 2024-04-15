using System;

public class EnemyStateMachine : FSM
{
    //玩家信息
    //假如我们没有子类，为了获取这些信息，我们不得不直接在基类声明它
    //但是我们的敌怪也要用FSM，如果没有子类，那是不是还要声明个EnemyInfo？
    public Enemy enemy;
    //玩家控制，同理，子类优势
    public EnemyController enemyController;
    public EnemyStateMachine(Enemy enemy, EnemyController controller)
    {
        this.enemy = enemy;
        enemyController = controller;
    }
}