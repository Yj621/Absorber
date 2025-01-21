using UnityEngine;

public class EnemyTrace : MonoBehaviour
{
    [SerializeField] private Transform player; // �÷��̾� Transform
    private Transform enemyTransform; // �θ� Enemy Transform ����

    [SerializeField] private float moveSpeed = 2.5f; // ���� �̵� �ӵ�

    private EnemyMove enemyMove; // EnemyMove ��ũ��Ʈ ����
    private bool isFollowing = false; // ���� ����

    void Awake()
    {
        enemyMove = GetComponentInParent<EnemyMove>();
        enemyTransform = transform.parent;
    }

    void Update()
    {
        if (isFollowing && player != null)
        {
            FollowPlayer(); // �÷��̾� ����
        }
    }

    // �÷��̾ �����ϴ� ����
    void FollowPlayer()
    {
        float direction = player.position.x - enemyTransform.position.x;
        int nextMove = direction > 0 ? -1 : 1; // �÷��̾ �����ʿ� ������ -1, ���ʿ� ������ 1

        enemyTransform.Translate(Vector2.right * nextMove * moveSpeed * Time.deltaTime);

        // ���� ��ȯ
        if (nextMove == 1)
            enemyTransform.rotation = Quaternion.Euler(0, 180, 0); // ����
        else
            enemyTransform.rotation = Quaternion.Euler(0, 0, 0); // ������
    }
    // �÷��̾ ���� ������ ���Դ��� Ȯ��
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // "Player" �±׷� �÷��̾ ����
        {
            isFollowing = true; // ���� ����
            Debug.Log("������");
            enemyMove.enabled = false; // EnemyMove ��Ȱ��ȭ
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFollowing = false; // ���� ����
            Debug.Log("��������");
            enemyMove.enabled = true; // EnemyMove Ȱ��ȭ
        }
    }
}
