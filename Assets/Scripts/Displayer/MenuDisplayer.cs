using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MenuDisplayer : MonoBehaviour
{
    // �޴� â
    [SerializeField] private GameObject menuPanel;

    // ��
    [SerializeField]private GameObject resolutionTab;
    [SerializeField]private GameObject soundTab;

    // ���� �����̴�
    [SerializeField]private Slider volumeSlider;

    //  �ػ� ��Ӵٿ�
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    // �ػ� ���
    private Resolution[] resolutions;

    // ��ư �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI ButtonText;

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

        // ��ư �ؽ�Ʈ �ʱ�ȭ
        UpdateButtonText();
    }

    private void Update()
    {   // Esc ��ư �� �޴� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }

    // �޴���ư 
    public void OpenMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        Time.timeScale = menuPanel.activeSelf ? 0 : 1;
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

    // Ǯ��ũ�� �¿���
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        UpdateButtonText();
        
    }
    // Ǯ��ũ�� �¿��� �ؽ�Ʈ ������Ʈ
    private void UpdateButtonText()
    {
        if(Screen.fullScreen)
        {
            ButtonText.text = "On";
        }
        else
        {
            ButtonText.text = "Off";
        }
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
