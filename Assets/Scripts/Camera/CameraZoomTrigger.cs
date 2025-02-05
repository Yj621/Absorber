using UnityEngine;

public class CameraZoomTrigger : MonoBehaviour
{
    public float zoomedOutFOV = 80f; // �ܾƿ��� �� FOV
    public float normalFOV = 60f; // �⺻ FOV
    public float zoomSpeed = 2f; // �� �ӵ�

    private bool isZoomed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������
        {
            isZoomed = true;
            StartCoroutine(ChangeFOV(zoomedOutFOV)); // �ܾƿ�
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ������
        {
            isZoomed = false;
            StartCoroutine(ChangeFOV(normalFOV)); // �������
        }
    }

    System.Collections.IEnumerator ChangeFOV(float targetFOV)
    {
        float startFOV = UnityEngine.Camera.main.fieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            UnityEngine.Camera.main.fieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsedTime * zoomSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        UnityEngine.Camera.main.fieldOfView = targetFOV;
    }
}
