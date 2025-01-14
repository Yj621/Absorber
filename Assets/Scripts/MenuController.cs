using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // �޴� â
    public GameObject menuPanel;
    // ��ų ���� â
    public GameObject skillPanel;
    // ���� �����̴�
    public Slider volumeSlider; 

    void Start()
    {
        // �����̴� �ʱ�ȭ
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }
    // �޴���ư 
    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
    // �޴� ���� 
    public void CloseMenu()
    {
        menuPanel.SetActive(false);
    }
    // ��ųâ ����
    public void OpenSkillMenu()
    {
        skillPanel.SetActive(true);
    }
    // ��ųâ ����
    public void CloseSkillMenu()
    {
        skillPanel.SetActive(false);
    }
    // ��ü ���� ����
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; 
    }
}
