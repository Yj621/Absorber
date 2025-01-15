using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // Ŭ���� or ���ӿ��� �˾�â
    [SerializeField] private GameObject popupCanvas;
    // ���� Ŭ���� ����
    private bool isCleared;
    public bool IsCleared { get { return isCleared; } }


    private PlayerController playerController;
    private Player player;
    private LifeDisplayer LifeDisplayer;
    private int life = 5;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

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
        LifeDisplayer.SetLives(life);
    }

    void Update()
    {

    }

    // ������ ������ ���� ���� �� UI�ݿ�
    public void Damage()
    {
        if (life > 0) 
        {
            life--;
            LifeDisplayer.SetLives(life);
        }
        else
        {
            GameOver();
        }
    }

    // �����
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
