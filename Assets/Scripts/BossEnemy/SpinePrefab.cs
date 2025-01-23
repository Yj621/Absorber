using UnityEngine;

public class SpineProjectile : MonoBehaviour
{
    private Transform target; // �÷��̾� Ÿ��
    private Rigidbody2D rb;

    [SerializeField] private float initialSpeed = 5f; // �ʱ� �ӵ�
    [SerializeField] private float rotationSpeed = 200f; // ȸ�� �ӵ�
    [SerializeField] private float decelerationRate = 0.95f; // ���� ���� (1���� �۾ƾ� ��)
    [SerializeField] private float dashSpeed = 10f; // ���� �ӵ�
    [SerializeField] private float dashTime = 2f; // ���� �� �������� ��ȯ�Ǵ� �ð�
    private bool isDashing = false; // ���� ���� �÷���

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // ���� �ð� �� ���� ���·� ��ȯ
        Invoke(nameof(StartDash), dashTime);
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Dash(); // ���� ����
        }
        else
        {
            TrackAndCurve(); // �÷��̾� ���� �� � �̵�
        }
    }

    private void TrackAndCurve()
    {
        if (target == null) return;

        // Ÿ�� ���� ���
        Vector2 direction = (target.position - transform.position).normalized;

        // ���� ȸ��
        float rotateAmount = Vector3.Cross(direction, transform.right).z; // ȸ���� ���
        rb.angularVelocity = -rotateAmount * rotationSpeed; // ȸ�� �ӵ� ����

        // ���� �̵�
        rb.linearVelocity = transform.right * initialSpeed;

        // ���� ó��
        if (initialSpeed > dashSpeed) // ������ ���� �ӵ����� ũ��
        {
            initialSpeed *= decelerationRate;
        }
    }

    private void Dash()
    {
        // ���� �̵� (ȸ���� ���� ����)
        rb.angularVelocity = 0f; // ȸ�� ����
        rb.linearVelocity = transform.right * dashSpeed; // ���� �̵�
    }

    private void StartDash()
    {
        isDashing = true; // ���� ���·� ��ȯ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ �� � ����� �� ���� ����
        if (collision.CompareTag("Player") || collision.CompareTag("ground"))
        {
            Destroy(gameObject); // ���� ����
        }
    }
}
