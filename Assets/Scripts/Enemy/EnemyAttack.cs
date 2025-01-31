using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerController playerController; // �÷��̾� ��Ʈ�ѷ� ����
    private BaseEnemy baseEnemy;
    private void Start()
    {
        baseEnemy = GetComponentInParent<BaseEnemy>();
    }

    // �÷��̾ ������ �ǰ� ��� �޼���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController = collision.gameObject.GetComponent<PlayerController>();
        playerController.TakeDamage(baseEnemy.damage);
    }
}
