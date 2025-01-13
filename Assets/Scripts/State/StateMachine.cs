using UnityEngine;

public class StateMachine
{
    public IState CurrentState { get; private set; }
    PlayerController controller;

    public IdleState idleState;
    public JumpState jumpState;
    public DashState dashState; 
    public WalkState walkState;

    public StateMachine(PlayerController player)
    {
        controller = player;
        idleState = new IdleState(player);
        walkState = new WalkState(player);
        dashState = new DashState(player);
        jumpState = new JumpState(player);
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
