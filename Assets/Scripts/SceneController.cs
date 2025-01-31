using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // �� �迭
    public enum Scenes
    {
        //TutorialScene,
        Forest,
        BossForest,
        DesertNormalScene,
        DesertBossScene,
        SnowNormalScene,
        SnowBossScene
    }

    // �������� ���� ���� 
    private static int currentStage = 1;

    private static string targetScene;

    // ������ �� ��ȯ
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }
    // ���� �� �ε� 
    public void LoadNextScene()
    {
        if (currentStage < System.Enum.GetValues(typeof(Scenes)).Length)
        {
            Scenes nextScene = (Scenes)currentStage;
            targetScene = nextScene.ToString(); // ��ǥ �� ����
            SceneManager.LoadScene("LoadingScene"); // �ε� �� ���� ����
            currentStage++;
        }
        else
        {
            Debug.Log("��� �������� �Ϸ�");
        }
    }

    public static void LoadSpecificScene(Scenes scene)
    {
        targetScene = scene.ToString();
        SceneManager.LoadScene("LoadingScene"); // �ε� ���� ���� �ε�
    }

    // �ε� ������ ȣ���� �Լ�
    public static string GetTargetScene()
    {
        return targetScene;
    }
}
