using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ObjectPool objectPool; // ObjectPool ����

    public void Initialize(ObjectPool pool)
    {
        objectPool = pool; // ObjectPool�� �ʱ�ȭ
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �� �Ǵ� ��ֹ��� �浹 ó��
        if (collision.gameObject.CompareTag("Enemy")) // �� �±� Ȯ��
        {
           objectPool.ReturnObject(gameObject);
        }
        else if (collision.gameObject.CompareTag("ground")) // ���̳� �ٸ� ��ü�� �浹
        {
           objectPool.ReturnObject(gameObject);
        }
    }
}
