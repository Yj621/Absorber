using UnityEngine;

public class E_IdleState : IState
{
    BaseEnemy enemy;
    public E_IdleState(BaseEnemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        enemy.GetComponent<Animator>().SetTrigger("Idle");
    }

    public void Exit()
    {
    }

    public void Execute()
    {
    }
}
