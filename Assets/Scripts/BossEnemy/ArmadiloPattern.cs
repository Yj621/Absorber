using System.Collections;
using UnityEngine;

public class ArmadiloPattern : MonoBehaviour
{
    private bool isInCloseRange = false;
    private bool isInFarRange = false;
    private bool isInAirRange = false;

    [SerializeField] private GameObject CloseRange;
    [SerializeField] private GameObject FarRange;
    [SerializeField] private GameObject AirRange;

    private Animator ani;
    private bool isTransitioning = false;

    private Rigidbody2D rb;
    private bool isTackling = false; // ���� ���� ����
    private Vector2 tackleDirection; // ���� ����
    private float tackleSpeed = 40f; // ���� �ӵ�
    private float tackleDuration = 0.6f; // ���� ���� �ð�
    private float tackleTimer = 0f; // ���� Ÿ�̸�

    public PlayerController player;
    public EnemyStateMachine a_stateMachine;

    private void Awake()
    {
        a_stateMachine = GetComponent<EnemyController>().stateMachine;
    }
    private void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isTackling)
        {
            // ���� ���̸� Rigidbody2D�� �ӵ� ����
            rb.linearVelocity = tackleDirection * tackleSpeed;

            // Ÿ�̸� ������Ʈ
            tackleTimer -= Time.fixedDeltaTime;

            if (tackleTimer <= 0f)
            {
                // ���� ����
                rb.linearVelocity = Vector2.zero; // �ӵ� �ʱ�ȭ
                isTackling = false; // ���� ���� ����
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("����1");

            if (!isTransitioning)
            {
                if (collision.IsTouching(CloseRange.GetComponent<Collider2D>()))
                {
                    if (!isInCloseRange)
                    {
                        Debug.Log("����2: ����� �Ÿ�");
                        isInCloseRange = true;
                        StartCoroutine(CloseAttack());
                    }
                    isInFarRange = false;
                }

                // FarRange�� ���� �ȿ� �÷��̾ �ִ��� Ȯ��
                else if (collision.IsTouching(FarRange.GetComponent<Collider2D>()))
                {
                    if (!isInFarRange)
                    {
                        Debug.Log("����3: �� �Ÿ�");
                        isInFarRange = true;
                        StartCoroutine(FarAttack());
                    }
                }

                else if (collision.IsTouching(AirRange.GetComponent<Collider2D>()))
                {
                    if (!isInAirRange)
                    {
                        Debug.Log("����3: ���� �Ÿ�");
                        isInAirRange = true;
                        StartCoroutine(AirAttack());
                    }
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision == CloseRange.GetComponent<Collider2D>())
            {
                Debug.Log("����2");
                isInCloseRange = false;
            }

            // FarRange�� �ݶ��̴��� ��
            if (collision == FarRange.GetComponent<Collider2D>())
            {
                Debug.Log("����3");
                isInFarRange = false;
            }

            if (collision == AirRange.GetComponent<Collider2D>())
            {
                Debug.Log("����3");
                isInAirRange = false;
            }
        }
    }

    private IEnumerator CloseAttack()
    {
        if (isTransitioning == false)
        {
            isTransitioning = true;
            Stomp();
            yield return new WaitForSeconds(5);
            isInCloseRange = false;
            isTransitioning = false;
            
        }
    }

    private IEnumerator FarAttack()
    {
        if (isTransitioning == false)
        {
            isTransitioning = true;
            StartCoroutine(TackleRoutine());
            yield return new WaitForSeconds(5);
            isInFarRange = false;
            isTransitioning = false;
           
        }
    }

    private IEnumerator AirAttack()
    {
        if (isTransitioning == false)
        {
            isTransitioning = true;
            SpineAttack();
            yield return new WaitForSeconds(5);
            isInFarRange = false;
            isTransitioning = false;
           
        }
    }

    private void Stomp()
    {
        a_stateMachine.TransitionTo(a_stateMachine.a_StompState);
    }

    
    private IEnumerator TackleRoutine()
    {
        a_stateMachine.TransitionTo(a_stateMachine.a_AngryState);
        yield return new WaitForSeconds(1f); // �ִϸ��̼� ���� �� 0.4�� ���

        // ���� ����
        isTackling = true;
        tackleDirection = (player.transform.position - transform.position).normalized;
        tackleTimer = tackleDuration; // ���� ���� �ð� ����

        yield return new WaitForSeconds(tackleDuration + 1f); // ���� ���� �� ���
        isTackling = false;
        
    }


    private void SpineAttack()
    {
        a_stateMachine.TransitionTo(a_stateMachine.a_SpineState);
    }


}
