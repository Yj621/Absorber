using System.Collections;
using UnityEngine;

public class ArmadiloPattern : MonoBehaviour
{
    private bool isInCloseRange = false;
    private bool isInFarRange = false;

    [SerializeField] private GameObject CloseRange;
    [SerializeField] private GameObject FarRange;

    private Animator ani;



    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("����1");

            if (collision.IsTouching(CloseRange.GetComponent<Collider2D>()) && collision.IsTouching(FarRange.GetComponent<Collider2D>()) && !isInCloseRange)
            {
                Debug.Log("����2: ����� �Ÿ�");
                isInCloseRange = true;
                StartCoroutine(CloseAttack());
            }

            // FarRange�� ���� �ȿ� �÷��̾ �ִ��� Ȯ��
            if (collision.IsTouching(FarRange.GetComponent<Collider2D>()) && !isInFarRange)
            {
                Debug.Log("����3: �� �Ÿ�");
                isInFarRange = true;
                StartCoroutine(FarAttack());
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
        }
    }

    private IEnumerator CloseAttack()
    {
        if (isInCloseRange)
        {
            Stomp();
            yield return new WaitForSeconds(5);
        }
    }

    private IEnumerator FarAttack()
    {
        int result = Random.Range(0, 10);
        if (result < 6)
        {
            Tackle();
        }
        else
        {
            SpineAttack();
        }
        yield return new WaitForSeconds(5);
    }

    private void Stomp()
    {
        ani.SetTrigger("Stomp");
    }

    private void Tackle()
    {
        ani.SetTrigger("Angry");
    }

    private void SpineAttack()
    {
        ani.SetTrigger("Spine");
    }
}
