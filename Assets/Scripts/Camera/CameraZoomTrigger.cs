using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraZoomTrigger : MonoBehaviour
{
    private Coroutine zoomCoroutine;

    public float zoomOutSize = 8f; // �ܾƿ��� ũ��
    public float zoomInSize = 5f; // ���� ũ��
    public float zoomSpeed = 2f; // �� �ӵ�

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������ ������ ��
        {
            if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
            zoomCoroutine = StartCoroutine(ZoomCamera(zoomOutSize));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������ ����� ��
        {
            if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
            zoomCoroutine = StartCoroutine(ZoomCamera(zoomInSize));
        }
    }

    private IEnumerator ZoomCamera(float targetSize)
    {
        while (Mathf.Abs(UnityEngine.Camera.main.orthographicSize - targetSize) > 0.01f)
        {
            UnityEngine.Camera.main.orthographicSize = Mathf.Lerp(UnityEngine.Camera.main.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        UnityEngine.Camera.main.orthographicSize = targetSize;
    }
}