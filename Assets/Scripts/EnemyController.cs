using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; //�� �̵� �ӵ�
    [SerializeField] private int hp = 20;
    [SerializeField] private int damage = 10;

    [SerializeField] private float groundCheckRadius = 0.1f; //�ٴ� ���� �ݰ�

    [SerializeField] private LayerMask groundLayer; //ground�±װ� �ִ� ���̾� ����


    [SerializeField] private Collider2D frontCollider;
    [SerializeField] private Collider2D frontBottomCollider;

    [SerializeField] private PlayerController playerController;

    Vector2 vx;

    private void Start()
    {
        vx = Vector2.left * moveSpeed;
        Debug.Log("�� ü�� : "+hp);
    }

    private void Update()
    {
/*        //������
        bool isTouchingWall = Physics2D.OverlapCircle(frontCollider.transform.position, groundCheckRadius, groundLayer);

        //�ٴڰ���
        bool isGrounded = Physics2D.OverlapCircle(frontBottomCollider.transform.position, groundCheckRadius, groundLayer);

        if(isTouchingWall || !isGrounded) 
        {
            vx = -vx;
            transform.localScale = new Vector2(-transform.localScale.x, 1);
        }*/
    }

    private void FixedUpdate()
    {
       // transform.Translate(vx * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           playerController.player.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            Attack attack = collision.GetComponent<Attack>();
            if(attack != null)
            {
                TakeDamage(attack.damage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"��{gameObject} ü�� : {hp}"); 
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
