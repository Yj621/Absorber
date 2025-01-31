using UnityEngine;

public class WaterGun : MonoBehaviour
{
    private float lifeTime = 5f; // �Ѿ��� ���� �ð�
    private float timer = 0f; // Ÿ�̸�

    private void OnEnable() // �Ѿ��� Ȱ��ȭ�� �� Ÿ�̸� �ʱ�ȭ
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            gameObject.SetActive(false); // 3�� �� ��Ȱ��ȭ
        }
    }
}
