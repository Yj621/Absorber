using UnityEngine;

public class FlyEnemy : BaseEnemy
{

    public float speed = 2f; // �̵� �ӵ�
    public float distance = 5f; // �Դ� ���� �ϴ� �Ÿ�
    private Vector3 startPosition; // ���� ��ġ
    private float time;

    protected override void Start()
    {
        base.Start();
        // ������ ���� �ʱ� ���¸� idleState�� ����
        stateMachine.Initalize(stateMachine.idleState);
        startPosition = transform.position; // ���� ��ġ ���
    }

    protected override void PerformMovement()
    {
        Fly();
    }

    void Fly()
    {
        time += Time.deltaTime * speed; // �ð��� ������Ű�� �̵�
        float offset = Mathf.Sin(time) * distance; // ���� �Լ��� �̿��� �Դ� ���� �ϴ� �� ���
        transform.position = startPosition + new Vector3(offset, 0, 0); // ��ġ ������Ʈ
    }
}
