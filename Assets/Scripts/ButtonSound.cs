using UnityEngine;

public class ButtonSound : MonoBehaviour 
{ 
    public AudioSource hoverSound; // ��ư ���� ���콺�� �ö� �� ����� �Ҹ�

    public AudioSource clickSound; // ��ư ���� ���콺�� �ö� �� ����� �Ҹ�
    private void Start()
    {

    }

    public void PlayHoverSound()
    {
        hoverSound.Play();
    }

    public void PlayClickSound()
    {
        clickSound.Play();
    }
}

