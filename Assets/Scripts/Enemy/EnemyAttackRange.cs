using UnityEngine;
using System.Collections;

public class EnemyAttackRange : MonoBehaviour
{
    private BaseEnemy enemy; // ���� �⺻ ������ �����ϴ� BaseEnemy Ŭ����
    private FlyEnemy flyEnemy; // FlyEnemy Ŭ������ ���� ������ �߰�
    private MovingEnemy movingEnemy;

    public EnemyStateMachine a_stateMachine; // ���� ���¸� �����ϴ� ���� �ӽ�

   // private bool isPlayerInRange = false; // �÷��̾ ���� ������ �ִ��� Ȯ���ϴ� ����

    void Start()
    {
        // BaseEnemy Ŭ���� ��������
        enemy = GetComponentInParent<BaseEnemy>();

        // FlyEnemy Ŭ������ �����ϸ� ��������
        flyEnemy = GetComponentInParent<FlyEnemy>();

        movingEnemy = GetComponentInParent<MovingEnemy>();

        // ���� �ӽ� �ʱ�ȭ
        a_stateMachine = enemy.stateMachine;

        // ���� ���� �ڷ�ƾ ����
        StartCoroutine(AttackLoop());
    }

    // �÷��̾ ���� ���� �ȿ� ���Դ��� Ȯ��
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemy.isDie == false)
        {
            Debug.Log($"{transform.parent.gameObject}��(��) ���� ������ �÷��̾ �����߽��ϴ�.");
           // isPlayerInRange = true;
        }
    }

/*    // �÷��̾ ���� ������ ����� �� ȣ��
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false; // �÷��̾� ���� �ʱ�ȭ
        }
    }*/

    // �ݺ����� ���� ������ ó���ϴ� �ڷ�ƾ
    private IEnumerator AttackLoop()
    {
        while (true)
        {
            // ���� ��� �ð� (3�ʿ��� 5�� ���� ����)
            yield return new WaitForSeconds(Random.Range(3f, 5f));

            if (enemy.isDie == false)
            {
                // ���� ���·� ��ȯ
                a_stateMachine.TransitionTo(a_stateMachine.attackState);

                // movingEnemy ��� ���� �� 1�� �� walk ���·� ��ȯ
                if (movingEnemy != null)
                {
                    yield return new WaitForSeconds(1.5f); // ���� �ִϸ��̼� �ð� ��� (1��)
                    a_stateMachine.TransitionTo(a_stateMachine.walkState); // FlyEnemy�� idle ���·� ��ȯ
                }


                // FlyEnemy�� ��� ���� �� 1�� �� idle ���·� ��ȯ
                if (flyEnemy != null)
                {
                    yield return new WaitForSeconds(2f); // ���� �ִϸ��̼� �ð� ��� (1��)
                    a_stateMachine.TransitionTo(a_stateMachine.idleState); // FlyEnemy�� idle ���·� ��ȯ
                }
            }
        }
    }
}
