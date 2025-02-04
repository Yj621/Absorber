using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    private void Start()
    {
        DisableSingletons();
    }

    private void OnDestroy()
    {
        EnableSingletons(); // �� ���� �� �̱��� �ٽ� Ȱ��ȭ
    }
    public void OnClickResentGameStart()
    {
        SaveManager.SaveData saveData = SaveManager.Instance.LoadGame();
        SaveManager.Instance.LoadGameData(saveData);
        Debug.Log("����� ���� �ҷ�����");
    }
    public void OnClickGameStart()
    {
        SceneController.LoadNextScene();
    }
    // �޴� �г� �ݱ�
    public void OnClickQuit()
    {
        Application.Quit();
    }

    private void DisableSingletons()
    {
        if (PlayerController.instance != null) PlayerController.instance.gameObject.SetActive(false);
        if (SkillUIManager.Instance != null) SkillUIManager.Instance.gameObject.SetActive(false);
    }

    private void EnableSingletons()
    {
        if (PlayerController.instance != null) PlayerController.instance.gameObject.SetActive(true);
        if (SkillUIManager.Instance != null) SkillUIManager.Instance.gameObject.SetActive(true);
    }
}
