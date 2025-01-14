using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // �÷��̾� ���� �̹���
    public List<GameObject> lifeImages; 
    // �÷��̾� ���� ��
    [SerializeField] private int life = 5; 
    // Ŭ���� or ���ӿ��� �˾�â
    [SerializeField] private GameObject popupCanvas;
    // ���� Ŭ���� ����
    private bool isCleared;
    public bool IsCleared { get { return isCleared; } }

    private PlayerController player;

    private static UIController instance;
    public static UIController Instance { get { return instance; } }

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

    private void Start()
    {   // ���� �̹��� Ȱ��ȭ
        SetLives(life);
    }
    void Update()
    {

    }

    // ��� �� ���� ���� �� UI�ݿ�, ����� 
    public void Die()
    {
        life--;
        SetLives(life);

        StartCoroutine(RestartCoroutine());
    }
    // 2�� �� ����� 
    private IEnumerator RestartCoroutine()
    {
        yield return new WaitForSeconds(2);
        Restart();
    }

    // ���� �̹��� Ȱ��ȭ, ��Ȱ��ȭ
    public void SetLives(int life)
    {
        for (int i = 0; i < lifeImages.Count; i++)
        {
            if (i < life)
            {
                lifeImages[i].SetActive(true);
            }
            else
            {
                lifeImages[i].SetActive(false);
            }
        }
    }
    // �����
    void Restart()
    {
        if (life > 0)
        {

        }
        else
        {
            GameOver();
        }
    }
    // ���ӿ���
    void GameOver()
    {
        isCleared = false;
        popupCanvas.SetActive(true);
    }
    // ���� Ŭ����
    public void GameClear()
    {
        isCleared = true;
        popupCanvas.SetActive(true);
    }


}
