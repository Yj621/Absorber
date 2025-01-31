using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speaker; // "a" �Ǵ� "b"
        public string text;    // ��ȭ ����
    }

    public List<DialogueLine> dialogueLines; // ���� Ʈ���Ÿ��� �ٸ� ��ȭ ����
    public Image protagonistImage; // ���ΰ� �̹���
    public Image npcImage;
    public TMP_Text leftTextBox;  // ���ΰ� ��ȭ TMP �ڽ�
    public TMP_Text rightTextBox; // ���� ��ȭ TMP �ڽ�
    public GameObject dialogueCanvas; // ��ȭ UI (ĵ����)

    private int currentLineIndex = 0; // ���� ��ȭ �ε��� (�� Ʈ���Ÿ��� ���� ����)
    private bool isDialogueActive = false; // ��ȭ ���� ������ üũ (���� ����)

    private static DialogueTrigger activeDialogue = null; // ���� ���� ���� ��ȭ (�ٸ� Ʈ���� ���� ����)
    private bool hasTriggered = false;
    void Start()
    {
        dialogueCanvas.SetActive(false); // ���� �� ��ȭ UI ��Ȱ��ȭ
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDialogueActive && activeDialogue == null && !hasTriggered) // ���� ��ȭ�� ���� ���� �ƴ� ���� ����
        {
            hasTriggered = true;
            DialougeManager.instance.SetCurrentDialogue(this);
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        isDialogueActive = true;
        activeDialogue = this; // ���� ��ȭ�� Ȱ��ȭ
        currentLineIndex = 0; // �� Ʈ���ſ����� �ʱ�ȭ
        dialogueCanvas.SetActive(true); // ��ȭ UI Ȱ��ȭ
        Time.timeScale = 0; // **�ð� ���߱�**
        ShowDialogueLine(); // ù ��� ǥ��
    }

    public void OnClickNext()
    {
        if (currentLineIndex >= dialogueLines.Count - 1) // ������ ��ȭ���� ���� üũ
        {
            EndDialogue();
            return;
        }

        currentLineIndex++; // �����ϰ� ����
        Debug.Log($"���� ��ȭ �ε���: {currentLineIndex} / �� ��ȭ ����: {dialogueLines.Count}");
        ShowDialogueLine();
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
        dialogueCanvas.SetActive(false); // ��ȭ UI ��Ȱ��ȭ
        isDialogueActive = false; // ��ȭ ���� ���·� ����
        activeDialogue = null; // ���� ���� ���� ��ȭ ����
        Time.timeScale = 1; // **�ð� �ٽ� ���� �ӵ���**
        currentLineIndex = 0;
        Debug.Log("��ȭ�� �������ϴ�.");
    }
}
