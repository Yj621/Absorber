using UnityEngine;

public class EnemyStateMachine 
{
    public IState CurrentState { get; private set; }
    EnemyController enemy;

    public E_IdleState idleState;
    public E_WalkState walkState;
    public E_AttackState attackState;
    public E_HitState hitState;
    public E_DieState dieState;

    public EnemyStateMachine(EnemyController enemy)
    {
        this.enemy = enemy;
        idleState = new E_IdleState(enemy);
        walkState = new E_WalkState(enemy);
        attackState = new E_AttackState(enemy);
        hitState = new E_HitState(enemy);
        dieState = new E_DieState(enemy);
    }

    //���� state�� �޾� �̸� CurrentState�� �ְ� Enter
    public void Initalize(IState state)
    {
        CurrentState = state;
        state.Enter();
    }

    //�ٲ� state�� �޾� ���� state�� ���ؼ��� Exit�� �����ϰ� CurrentState�� �ٲٸ� 
    //�ٲ� state�� Enter�� ����
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }

    public void Execute()
    {
        CurrentState.Execute();
    }
}
