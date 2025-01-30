using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraZoomTrigger : MonoBehaviour
{
    public float zoomOutFOV = 30f; // Ʈ���� �ȿ����� �ܾƿ� FOV
    public float zoomSpeed = 2f; // FOV ���� �ӵ�
    public float originalFOV; // ī�޶��� ���� FOV
    private bool isInTrigger = false; // Ʈ���� �ȿ� �ִ��� Ȯ��

    
    void Update()
    {
        // Ʈ���� �ȿ� ���� ���� �ƴ� �� FOV�� ����
        if (isInTrigger)
        {
            UnityEngine.Camera.main.fieldOfView = Mathf.Lerp(zoomOutFOV, originalFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            UnityEngine.Camera.main.fieldOfView = Mathf.Lerp(originalFOV, zoomOutFOV, Time.deltaTime * zoomSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ Ʈ���� �ȿ� ������ ��
        if (other.CompareTag("Player"))
        {
            Debug.Log("DDDD");
            isInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �÷��̾ Ʈ���Ÿ� ������ ��
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }
}