using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rb;
    public int nextMove;
    Animator animator;
    public LayerMask groundLayer; // ������ ������ ���̾�

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Think();

        //������ �ֱ�
        Invoke("Think", 5);
    }

    // ���� ����̱� ������ FixedUpdate ���
    void FixedUpdate()
    {
        // ������ �����̰�
        rb.linearVelocity = new Vector2(nextMove, rb.linearVelocity.y);

        // �� �Ʒ� ���� ����
        if (!IsGroundAhead())
        {
            Turn(); // �������� ���� �� ���� ��ȯ
        }
    }

    // �� �Ʒ� ���� ����
    bool IsGroundAhead()
    {
        // �� �պκп� ����ĳ��Ʈ �߻�
        Vector2 origin = new Vector2(rb.position.x + nextMove * 0.5f, rb.position.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 1f, groundLayer);

        // ����׿� ���� �ð�ȭ
        Debug.DrawRay(origin, Vector2.down * 1f, Color.green);

        // ������ �����Ǹ� true ��ȯ
        return hit.collider != null;
    }

    //�������� ���������� �Ǵ�
    void Think()
    {
        // -1 �Ǵ� 1�� �������� ����
        nextMove = Random.Range(0, 2) == 0 ? -1 : 1;

        // �ִϸ��̼� ����
        animator.SetInteger("WalkSpeed", nextMove);

        // ���� ��ȯ
        if (nextMove == 1)
            transform.rotation = Quaternion.Euler(0, 0, 0); // ������
        else
            transform.rotation = Quaternion.Euler(0, 180, 0); // ����

        // ���� Think ȣ�� ����
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        // ���� ��ȯ�� ����
        nextMove *= -1;

        if (nextMove == 1)
            transform.rotation = Quaternion.Euler(0, 0, 0); // ������
        else
            transform.rotation = Quaternion.Euler(0, 180, 0); // ����
    }
}
