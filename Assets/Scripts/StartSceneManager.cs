using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    private MenuDisplayer mDisplayer;
    private GameObject menuPanel;
    private void Start()
    {
        mDisplayer = FindFirstObjectByType<MenuDisplayer>();
        menuPanel = mDisplayer.transform.Find("MenuPanel")?.gameObject;
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

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickSetting()
    {
        menuPanel.gameObject.SetActive(true);
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
