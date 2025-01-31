using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    public GameObject prefab; // Ǯ���� ������
    public int initialSize = 10; // �ʱ� Ǯ ũ��

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // �ʱ� Ǯ ����
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); // ��Ȱ��ȭ
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        // ��� ������ ������Ʈ�� ������ ��ȯ
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true); // Ȱ��ȭ
            return obj;
        }

        // ������ ���� ���� (Ȯ�� ����)
        GameObject newObj = Instantiate(prefab, position, rotation);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // ��Ȱ��ȭ
        pool.Enqueue(obj); // Ǯ�� �ٽ� �߰�
    }
}
