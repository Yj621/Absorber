using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplayer : MonoBehaviour
{
    // �÷��̾� ���� �̹���
    public List<GameObject> lifeImages;

   
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
    
}
