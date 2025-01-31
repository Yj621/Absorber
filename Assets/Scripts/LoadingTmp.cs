using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingTmp : MonoBehaviour
{
    public TMP_Text loadingText; // TMP �ؽ�Ʈ ����
    private string baseText = "�ε���"; // �⺻ �ؽ�Ʈ
    private int dotCount = 0; // ��ħǥ ����
    private float interval = 0.3f; // ���� ����

    private void Start()
    {
        StartCoroutine(AnimateText()); // �ִϸ��̼� ����
    }

    IEnumerator AnimateText()
    {
        while (true)
        {
            dotCount = (dotCount + 1) % 4; // 0, 1, 2, 3 �ݺ�
            loadingText.text = baseText + new string('.', dotCount); // ��ħǥ �߰�
            yield return new WaitForSeconds(interval); // ��� �� �ݺ�
        }
    }
}
