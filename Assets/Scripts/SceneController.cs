using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // �� �迭
    public enum Scenes
    {
        TutorialScene,
        Forest,
        BossForest,
        DessertNormalScene,
        DessertBossScene,
        SnowNormalScene,
        SnowBossScene
    }

    private static Dictionary<Scenes, Vector2> spawnPositions = new Dictionary<Scenes, Vector2>()
    {
        { Scenes.TutorialScene, new Vector2(-41.5f, 3.3f) },
        { Scenes.Forest, new Vector2(-131, 5.39f) },
        { Scenes.BossForest, new Vector2(375, -6.1f) },
        { Scenes.DessertNormalScene, new Vector2(196.2f, -3.3f) },
        { Scenes.DessertBossScene, new Vector2(131.6f, -4f) },
        { Scenes.SnowNormalScene, new Vector2(-5, 2) },
        { Scenes.SnowBossScene, new Vector2(7, -4) }
    };

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

    public static Vector2 GetSpawnPosition(Scenes scene)
    {
        if (spawnPositions.ContainsKey(scene))
        {
            return spawnPositions[scene];
        }
        return Vector2.zero; // �⺻�� (0,0)
    }

    // �ε� ������ ȣ���� �Լ�
    public static string GetTargetScene()
    {
        return targetScene;
    }
}
