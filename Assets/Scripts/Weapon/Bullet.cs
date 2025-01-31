using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ObjectPool objectPool; // ObjectPool ����

    public void Initialize(ObjectPool pool)
    {
        objectPool = pool; // ObjectPool�� �ʱ�ȭ
    }

    private void OnTriggerEnter2D(Collider2D other) { 
        // �� �Ǵ� ��ֹ��� �浹 ó��
        if (other.gameObject.CompareTag("Enemy")) // �� �±� Ȯ��
        {
           objectPool.ReturnObject(gameObject);
        }
        else if (other.gameObject.CompareTag("ground")) // ���̳� �ٸ� ��ü�� �浹
        {
           objectPool.ReturnObject(gameObject);
        }
    }
}
