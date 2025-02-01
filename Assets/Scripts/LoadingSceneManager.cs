using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingSceneManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        string nextScene = SceneController.GetTargetScene(); // �̵��� �� ��������
        if (string.IsNullOrEmpty(nextScene))
        {
            nextScene = "TutorialScene"; // �⺻ �� ���� (ó�� ������ �� ���)
        }
        float RandomTime = Random.Range(1f, 2f);
        yield return new WaitForSeconds(RandomTime); // �ε� �ð� ���� (���̵� ȿ�� ����)

        SceneManager.LoadScene(nextScene); // ��� �� �ε�
    }
}
