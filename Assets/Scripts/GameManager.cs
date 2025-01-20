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
   
    [SerializeField]private LifeDisplayer lifeDisplayer;
    // ���� �� 
    [SerializeField]private int life = 10; 

    private Player player;

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
        if (lifeDisplayer != null)
        {
            player = new Player(life, 0f,0f,0f, lifeDisplayer);
            lifeDisplayer.SetLives(player.PlayerHp);
            
        }
        else 
        { 
            Debug.Log("������ ���÷��� Ȱ��ȭ �ȵƾ��"); 
        }
    }

    void Update()
    {
        // �׽�Ʈ��
        if (Input.GetKeyDown(KeyCode.G))
        {
<<<<<<< Updated upstream
            player.TakeDamage(1);
=======
            //player.TakeDamage(1);
            TestDamage();
>>>>>>> Stashed changes
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            player.Heal(1);
        }

    }

    void TestDamage()
    {
        if(life > 0)
        {
            life--;
            lifeDisplayer.SetLives(life);
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
