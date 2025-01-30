using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour
{
    public static DialougeManager instance; // �̱���
    private DialogueTrigger currentDialogue; // ���� ��ȭ Ʈ����
    public Button nextButton; // ��ȭ ���� ��ư

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SetCurrentDialogue(DialogueTrigger dialogue)
    {
        currentDialogue = dialogue;
        nextButton.onClick.RemoveAllListeners(); // ���� ���� ����
        nextButton.onClick.AddListener(() => currentDialogue.OnClickNext()); // ���ο� Ʈ���� ����
    }
}
