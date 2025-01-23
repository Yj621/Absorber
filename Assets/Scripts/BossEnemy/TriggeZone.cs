using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private ArmadiloPattern boss; // ���� ���� ��ũ��Ʈ ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("�Ա� ����: ���� Ȱ��ȭ");
            boss.ActivateMovingState(); // ������ ���¸� Moving���� ����
        }
    }
}
