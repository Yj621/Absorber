using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public ObjectPool objectPool; // ObjectPool ����
    private float lifeTime = 5f; // �Ѿ��� ���� �ð�
    private float timer = 0f; // Ÿ�̸�

    public void Initialize(ObjectPool pool, float lifeTime)
    {
        objectPool = pool;
        this.lifeTime = lifeTime; // �Ѿ��� ���� �ð� ����
    }

    private void OnEnable()
    {
        timer = 0f; // Ÿ�̸� �ʱ�ȭ
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            // Ÿ�̸Ӱ� ���� �ð��� ������ Ǯ�� ��ȯ
            if (objectPool != null)
            {
                objectPool.ReturnObject(gameObject);
            }
        }
    }
}
