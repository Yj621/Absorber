using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpBlock : MonoBehaviour
{
    public float riseHeight = 2f; // ����� ����
    public float riseSpeed = 2f;  // ��� �ӵ�
    public bool returnToStart = false; // ���� ��ġ�� ���ƿ��� ����
    public float returnDelay = 2f; // ���� ��ġ�� ���ƿ��� �� ��� �ð�

    private Vector3 startPos; // ���� ��ġ ����
    private bool isMoving = false; // ���� �����̰� �ִ��� üũ

    void Start()
    {
        startPos = transform.position; // ���� ��ġ ����
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isMoving) // �÷��̾ ��Ұ�, ���� �����̰� ���� ���� ��
        {
            StartCoroutine(MoveBlock(startPos + Vector3.up * riseHeight)); // ��� �ڷ�ƾ ����
        }
    }

    IEnumerator MoveBlock(Vector3 targetPos)
    {
        isMoving = true; // �̵� ����
        float elapsedTime = 0;
        Vector3 initialPos = transform.position;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(initialPos, targetPos, elapsedTime * riseSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos; // ��Ȯ�� ��ġ ����

        if (returnToStart)
        {
            yield return new WaitForSeconds(returnDelay); // �ǵ��ư��� �� ���
            StartCoroutine(MoveBlock(startPos)); // ���� ��ġ�� �̵�
        }
        else
        {
            isMoving = false; // ������ ����
        }
    }
}
