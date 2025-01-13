using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BombExplode());
    }

    private IEnumerator BombExplode()
    {
        yield return new WaitForSeconds(2f);

        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);
        // ��ź ������Ʈ ����
        Destroy(explosion);
    }

}
