using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuDisplayer : MonoBehaviour
{
    // �޴� â
    public GameObject menuPanel;

    // ��
    public GameObject resolutionTab;
    public GameObject soundTab;

    // ���� �����̴�
    public Slider volumeSlider;

    //  �ػ� ��Ӵٿ�
    public TMP_Dropdown resolutionDropdown;

    // �ػ� ���
    private Resolution[] resolutions;

    void Start()
    {
        // ���� �����̴� �ʱ�ȭ
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // �ػ� ���� �ʱ�ȭ
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // �ػ� �ɼ� �߰�
        foreach (Resolution res in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(res.width + " x " + res.height));
        }
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
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
    
    // ��ü ���� ����
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; 
    }

    // �ػ� ����
    public void SetResolution(int index)
    {
        Resolution selectedResolution = resolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen );
    }
    // ���� �� ����
    public void OpenSoundTab()
    {
        soundTab.SetActive(true);
        resolutionTab.SetActive(false);
    }
    // �ػ� �� ����
    public void OpenResolutionTab()
    {
        soundTab.SetActive(false );
        resolutionTab.SetActive(true);
    }
}
