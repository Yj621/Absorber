using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
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
}
