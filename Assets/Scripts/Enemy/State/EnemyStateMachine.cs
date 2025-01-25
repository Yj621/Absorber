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
    public A_AngryState a_AngryState;
    public A_StompState a_StompState;
    public A_SpineState a_SpineState;
    public C_StompState c_StompState;
    public C_DropState c_DropState;
    public C_JumpState c_JumpState;
    public F_JumpState f_JumpState;
    public F_ToungeState f_ToungeState;
    public F_SpwanState f_SpwanState;
    public EnemyStateMachine(EnemyController enemy)
    {
        this.enemy = enemy;
        idleState = new E_IdleState(enemy);
        walkState = new E_WalkState(enemy);
        attackState = new E_AttackState(enemy);
        hitState = new E_HitState(enemy);
        dieState = new E_DieState(enemy);
        a_AngryState = new A_AngryState(enemy);
        a_StompState = new A_StompState(enemy);
        a_SpineState = new A_SpineState(enemy);
        c_StompState = new C_StompState(enemy);
        c_DropState = new C_DropState(enemy);
        c_JumpState = new C_JumpState(enemy);
        f_JumpState = new F_JumpState(enemy);
        f_ToungeState = new F_ToungeState(enemy);
        f_SpwanState = new F_SpwanState(enemy);
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
