using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour
{
    public static DialougeManager instance; // �̱���
    private DialogueTrigger currentDialogue; // ���� ��ȭ Ʈ����
    public Button nextButton; // ��ȭ ���� ��ư

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �̺�Ʈ ���
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitForNextButton()); // ���� �ٲ�� ��ư �ٽ� ã��
    }

    private IEnumerator WaitForNextButton()
    {
        GameObject btnObj = null;

        // `NextButton`�� ã�� �� ���� ������ ���
        while (btnObj == null)
        {
            btnObj = GameObject.FindWithTag("NextButton");
            yield return null; // �� ������ ���
        }

        nextButton = btnObj.GetComponent<Button>();

        if (nextButton != null && currentDialogue != null)
        {
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() => currentDialogue.OnClickNext());
        }
    }

    public void SetCurrentDialogue(DialogueTrigger dialogue)
    {
        currentDialogue = dialogue;
    }

    // DialogueTrigger�� Ȱ��ȭ�� �� ��ư�� ã���� ��
    public void OnDialogueEnabled()
    {
        StartCoroutine(WaitForNextButton());
    }
}
