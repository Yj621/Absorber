using UnityEngine;

public class BossEnemy : BaseEnemy
{
    protected override void PerformMovement()
    {
        
    }

    protected override void Start()
    {
        base.Start();
        // ������ ���� �ʱ� ���¸� idleState�� ����
        stateMachine.Initalize(stateMachine.idleState);
    }
}
