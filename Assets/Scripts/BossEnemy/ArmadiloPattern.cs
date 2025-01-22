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
    private float tackleSpeed = 30f; // ���� �ӵ�
    private float tackleDuration = 1.5f; // ���� ���� �ð�
    private float tackleTimer = 0f; // ���� Ÿ��

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
       
            isTransitioning = true;
            Stomp();
            yield return new WaitForSeconds(5);
            isInCloseRange = false;
            isTransitioning = false;
        ani.SetTrigger("Idle");

    }

    private IEnumerator FarAttack()
    {
        isTransitioning = true;
            Tackle();
            yield return new WaitForSeconds(5);
            isInFarRange = false;
        isTransitioning = false;
        ani.SetTrigger("Idle");

    }

    private IEnumerator AirAttack()
    {
        isTransitioning = true;
        SpineAttack();
        yield return new WaitForSeconds(5);
        isInFarRange = false;
        isTransitioning = false;
        ani.SetTrigger("Idle");

    }

    private void Stomp()
    {
        ani.SetTrigger("Stomp");
    }

    private void Tackle()
    {
        StartCoroutine(TackleRoutine());
        ani.SetTrigger("Idle");

    }

    private IEnumerator TackleRoutine()
    {
        ani.SetTrigger("Angry");
        yield return new WaitForSeconds(1f); // �ִϸ��̼� ���� �� 0.4�� ���

        // ���� ����
        isTackling = true;
        tackleDirection = Vector2.left; // ���� ���� ���� (��: ���������� ����)
        tackleTimer = tackleDuration; // ���� ���� �ð� ����

        yield return new WaitForSeconds(tackleDuration + 0.4f); // ���� ���� �� ���
        isTackling = false;
        
    }


    private void SpineAttack()
    {
        ani.SetTrigger("Spine");
    }


}
