using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void OnClickResentGameStart()
    {
        Debug.Log("����� ���� �ҷ�����");
    }
    public void OnClickGameStart()
    {
        SceneManager.LoadScene(1);
    }
    // �޴� �г� �ݱ�
    public void OnClickQuit()
    {
        Application.Quit();
    }
}
