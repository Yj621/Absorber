using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton <GameManager>
{
    private bool isCleared;
    public bool IsCleared { get { return isCleared; } }
   
    [SerializeField] private LifeDisplayer lifeDisplayer;
    
    [SerializeField] private int life = 10; 

    private Player player;
    private PlayerController playerController;

    [SerializeField] GameObject gameOverPanel;    
    

    private void Start()
    {   
        if (lifeDisplayer != null)
        {
            player = new Player(life, 0f,0f,0f);
            lifeDisplayer.SetLives(player.PlayerHp, player.PlayerMaxHp);
            
        }

        playerController = FindObjectOfType<PlayerController>();

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    //player.TakeDamage(1);
        //}
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    player.Heal(1);
        //}

    }
    // ���� ���� �г� Ȱ��ȭ
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    // ���� �޴� �̵�
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    // ���� ����
    public void QuitGame()
    {
        Application.Quit();
    }

    // �����
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
