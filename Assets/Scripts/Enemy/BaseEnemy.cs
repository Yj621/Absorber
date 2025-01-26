using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("���� ����")]
    public bool isDie = false; // ���� ����ߴ��� ����
    protected bool isAttacked = false; // ���� ���ݹ޴� ������ ����
    protected Rigidbody2D rb; // Rigidbody2D ����
    [SerializeField] protected int hp = 20; // �� ü��
    public int damage = 10; // �� ���ݷ�
    [SerializeField] protected GameObject itemPrefab; // ����� ������ ������
    public PlayerController playerController; // �÷��̾� ��Ʈ�ѷ� ����
    public EnemyStateMachine stateMachine; // ���� �ӽ� ����

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // ���� �ӽ� �ʱ�ȭ
        stateMachine = new EnemyStateMachine(this);
        // ���� �ӽ��� �ʱ� ���¸� Idle�� ����
        stateMachine.Initalize(stateMachine.idleState);

    }

    protected virtual void Update()
    {
        if (isDie) return;
        PerformMovement(); // �̵� ����
    }

    protected abstract void PerformMovement(); // �̵� ����(�̵���/���������� ���� ����)

    public void EnemyTakeDamage(int damage)
    {
        if (isDie) return;

        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
        else
        {
            stateMachine.TransitionTo(stateMachine.hitState);
            Debug.Log($"{gameObject.name} ü��: {hp}");
        }
    }

    //���� ������ ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie || isAttacked) return; // �̹� ���� ���� �浹 ����
        if (collision.gameObject.CompareTag("Attack"))
        {
            Attack attack = collision.GetComponent<Attack>();
            if (attack != null)
            {
                EnemyTakeDamage(attack.damage);

                //���� �ð����� �߰� ���� ����
                isAttacked = true;
                Invoke("ResetAttackState", 1f); // 0.5�� �� �ٽ� ���� ����
            }
        }
    }

    //���� �ð����� �߰� ���� ���� �޼���
    private void ResetAttackState()
    {
        isAttacked = false; // ���� ���� ���·� ����
    }
    public void Attack(int damage)
    {
        //��ų ������(TakeDamage�� ������ ������� ü���� ��� �޼���� ������ Attack�� �޾����� ���� �� ��� ��)
        playerController.TakeDamage(damage + 10);
        Debug.Log("��ų ������");
    }

    protected virtual void Die()
    {
        isDie = true; // ��� ���� ����
        Debug.Log($"{gameObject.name} ���");

        // ���� �ð� �� ������ ����
        Invoke(nameof(SpawnItem), 2f);

        // �� ��ü ����
        Destroy(gameObject);
    }


    protected virtual void SpawnItem()
    {
        if (itemPrefab != null)
        {
            // �� ��ġ���� �ణ ���� ������ ����
            GameObject item = Instantiate(itemPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);

            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // ���� �ö󰡴� ���� ��
                rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);

                // �߷��� �����Ͽ� �ٴ����� ������ ���� ����
                rb.gravityScale = 1f; // �߷��� �ٿ� �ڿ������� ������ ����
            }
        }
    }

}
