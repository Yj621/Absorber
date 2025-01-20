using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplayer : MonoBehaviour
{
    // �÷��̾� ���� �̹���
    public List<GameObject> lifeImages;

    private static LifeDisplayer instance;
    public static LifeDisplayer Instance { get { return instance; } }

    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���� �̹��� Ȱ��ȭ, ��Ȱ��ȭ
    public void SetLives(int life)
    {
        if (lifeImages == null || lifeImages.Count == 0) return;

        // ���� ü�°� �ִ� ü���� ������� Ȱ��ȭ�� �̹��� �� ���
        int activeImages = Mathf.CeilToInt((float)life / playerController.player.PlayerMaxHp * lifeImages.Count);

        for (int i = 0; i < lifeImages.Count; i++)
        {
            if (i < activeImages)
            {
                lifeImages[i].SetActive(true);
                Debug.Log("heal");
            }
            else
            {
                lifeImages[i].SetActive(false);
                Debug.Log("damage");
            }
        }
    }


}
