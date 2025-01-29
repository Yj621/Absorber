using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // �� �迭
    public enum Scenes
    {
        //sTutorialScene,
        Forest,
        BossForest,
        DesertNormalScene,
        DesertBossScene,
        SnowNormalScene,
        SnowBossScene
    }

    // �������� ���� ���� 
    private static int currentStage = 0;

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
            SceneManager.LoadScene(nextScene.ToString());
            currentStage++;
        }
        else
        {
            Debug.Log("��� �������� �Ϸ�");
        }
    }
}
