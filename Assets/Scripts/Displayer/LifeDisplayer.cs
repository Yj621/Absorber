using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplayer : MonoBehaviour
{
    // 플레이어 생명 이미지
    public List<GameObject> lifeImages;

    private static LifeDisplayer instance;
    public static LifeDisplayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<LifeDisplayer>(); // 자동으로 찾기
            }
            return instance;
        }
    }

    [SerializeField] private PlayerController playerController;
   
    
    // 생명 이미지 활성화, 비활성화
    public void SetLives(int life, int maxHp)
    {
        if (lifeImages == null || lifeImages.Count == 0) return;


        // 현재 체력과 최대 체력을 기반으로 활성화할 이미지 수 계산
        int activeImages = Mathf.CeilToInt((float)life / maxHp * lifeImages.Count);


        for (int i = 0; i < lifeImages.Count; i++)
        {
            if (i < activeImages)
            {
                lifeImages[i].SetActive(true);
            }
            else
            {
                lifeImages[i].SetActive(false);
            }
        }
    }


}
