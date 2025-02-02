using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialouge : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speaker; // "Protagonist" �Ǵ� "NPC"
        public string text;    // ��ȭ ����
    }

    public List<DialogueLine> dialogueLines; // ��ȭ ������

    public Image protagonistImage; // ���ΰ� �̹���
    public Image npcImage;
    public TMP_Text leftTextBox;  // ���ΰ� ��ȭ TMP �ڽ�
    public TMP_Text rightTextBox; // ���� ��ȭ TMP �ڽ�

    private int currentLineIndex = 0; // ���� ��ȭ �ε���

    void Start()
    {
        ShowDialogueLine(); // ù ��ȭ ǥ��
        DisableSingletons();
    }

    private void OnDestroy()
    {
        EnableSingletons(); // �� ���� �� �̱��� �ٽ� Ȱ��ȭ
    }

    public void OnClickNext()
    {
        currentLineIndex++; // ���� ��ȭ�� �̵�
        if (currentLineIndex < dialogueLines.Count)
        {
            ShowDialogueLine();
            AudioManager.Instance.ButtonSound();
        }
        else
        {
            EndDialogue(); // ��ȭ ���� ó��
        }
    }

    private void ShowDialogueLine()
    {
        var line = dialogueLines[currentLineIndex]; // ���� ��ȭ �� ��������

        // ��ȭ �ڽ� �ʱ�ȭ
        leftTextBox.text = "";
        rightTextBox.text = "";

        // ȭ�ڿ� ���� �ؽ�Ʈ ���
        if (line.speaker == "a")
        {
            protagonistImage.color = Color.white; // ���ΰ� �̹��� ���
            npcImage.color = new Color(0.5f, 0.5f, 0.5f); // NPC �̹��� ��Ӱ�
            leftTextBox.text = line.text; // ���ΰ� ��ȭ ���� �ڽ��� ���
        }
        else if (line.speaker == "b")
        {
            protagonistImage.color = new Color(0.5f, 0.5f, 0.5f); // ���ΰ� �̹��� ��Ӱ�
            npcImage.color = Color.white; // NPC �̹��� ���
            rightTextBox.text = line.text; // ���� ��ȭ ������ �ڽ��� ���
        }
    }

    private void EndDialogue()
    {
        // ��ȭ ���� ó�� (��: �ؽ�Ʈ �ڽ� �ʱ�ȭ)
        leftTextBox.text = "";
        rightTextBox.text = "";
        Debug.Log("��ȭ�� �������ϴ�.");
        SceneController.LoadNextScene();
    }

    private void DisableSingletons()
    {
        if (PlayerController.instance != null) PlayerController.instance.gameObject.SetActive(false);
        if (UIManager.Instance != null) UIManager.Instance.gameObject.SetActive(false);
    }

    private void EnableSingletons()
    {
        if (PlayerController.instance != null) PlayerController.instance.gameObject.SetActive(true);
        if (UIManager.Instance != null) UIManager.Instance.gameObject.SetActive(true);
    }
}
