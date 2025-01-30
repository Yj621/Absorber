using UnityEngine;

public class ButtonSound : MonoBehaviour 
{ 
    public AudioClip hoverSound; // ��ư ���� ���콺�� �ö� �� ����� �Ҹ�
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = hoverSound;
    }

    public void PlayHoverSound()
    {
        if (audioSource != null && hoverSound != null)
        {
            Debug.Log("play");
            audioSource.Play();
        }
    }
}

