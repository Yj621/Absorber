using UnityEngine;

public class AbsorbOb : MonoBehaviour
{
    public float fadeSpeed = 0.5f; // ���������� �ӵ� (�������� ����)
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // �ڽ��� SpriteRenderer ��������
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Ư�� �±׸� ���� ������Ʈ�� �۵�
        if (other.CompareTag("AbsorbEffect"))
        {
            // ������Ʈ�� SpriteRenderer ��������
            Color currentColor = spriteRenderer.color;

            if (spriteRenderer != null)
            {
                // ���İ�(����)�� ���� ����
                float newAlpha = Mathf.Max(currentColor.a - (fadeSpeed * Time.deltaTime), 0);
                spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

                // ���İ��� 0�� �Ǹ� ������Ʈ ��Ȱ��ȭ
                if (newAlpha <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}

