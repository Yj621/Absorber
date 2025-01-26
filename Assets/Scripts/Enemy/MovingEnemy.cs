using UnityEngine;

public class MovingEnemy : BaseEnemy
{
    [Header("�̵� ���� ����")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float traceSpeed = 7f;
    [SerializeField] private float detectionRange = 5f;
    private bool isFollowing = false; // �÷��̾� ���� ����
    [SerializeField] private Transform groundCheck, wallCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius = 2f;
    [SerializeField] private Collider2D traceCollider; // �÷��̾� ���� ���� ����

    [SerializeField] private bool movingRight = true;
    [SerializeField] private Transform player;
    private Vector3 originalScale; // �ʱ� ���� ������ ����

    private void Start()
    {
        originalScale = transform.localScale; // �ʱ� ���� ������ ����
    }
    protected override void PerformMovement()
    {
        if (isFollowing)
        {
            FollowPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        // �������� �Ǵ� �� ���� �� ���� ��ȯ
        if (!IsGroundAhead() || IsWallAhead())
        {
            movingRight = !movingRight;
        }

        // �̵� ó��
        Move(movingRight ? moveSpeed : -moveSpeed);
    }

    private void FollowPlayer()
    {
        if (player == null) return; // �÷��̾ ������ ����

        // �÷��̾���� x�� �Ÿ� ���
        float directionToPlayer = player.position.x - transform.position.x;

        // �������� �Ǵ� �� ���� �� ���� ��ȯ
        if (!IsGroundAhead() || IsWallAhead())
        {
            movingRight = !movingRight;
        }

        // �÷��̾� ��ġ�� ���� ���� ����
        movingRight = directionToPlayer < 0;

        // �̵� ó��
        Move(movingRight ? traceSpeed : -traceSpeed);
    }

    private void Move(float speed)
    {
        if (!isDie)
        {
            // �̵� ó��
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

/*            // ���� ��ȯ�� �� ���� ����
            if (stateMachine.CurrentState != stateMachine.walkState)
            {
            }*/
            stateMachine.TransitionTo(stateMachine.walkState);

            Debug.Log("speed : " + speed);
            // ��������Ʈ ���� ����
            AdjustSpriteDirection(speed);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("playerController : " + playerController);
            //�÷��̾�� ������ ������
            playerController.TakeDamage(damage);
            Debug.Log("�Ϲ� ������");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // �÷��̾� ���� �� ���� ����
        if (collision.CompareTag("Player"))
        {
            isFollowing = true;
        }
    }

    private bool IsGroundAhead() =>
        Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

    private bool IsWallAhead() =>
        Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);


    private void AdjustSpriteDirection(float moveDirection)
    {
        // �̵� ���⿡ ���� ��������Ʈ ������
        if ((moveDirection < 0 && transform.localScale.x > 0) ||
            (moveDirection > 0 && transform.localScale.x < 0))
        {
            transform.localScale = new Vector3(transform.localScale.x, originalScale.y, originalScale.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // �������� ���� �ݰ� �ð�ȭ
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }

        // �� ���� �ݰ� �ð�ȭ
        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
        }

    }
}
