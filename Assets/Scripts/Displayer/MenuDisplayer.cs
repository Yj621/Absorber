using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MenuDisplayer : MonoBehaviour
{
    // �޴� â
    [SerializeField] private GameObject menuPanel;

    // ��
    [SerializeField] private GameObject saveTab;
    [SerializeField] private GameObject resolutionTab;
    [SerializeField] private GameObject soundTab;


    // ���̺� ���� ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI[] slotTexts;

    //  �ػ� ��Ӵٿ�
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    // �ػ� ���
    private Resolution[] resolutions;

    // OnOff ��ư �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI ButtonText;

    // ���� �����̴�
    [SerializeField] private Slider volumeSlider;

    void Start()
    {
        UpdateSaveSlots();

        // �ػ� ���� �ʱ�ȭ
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();


        // �ػ� �ɼ� �߰�
        foreach (Resolution res in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(res.width + " x " + res.height));
        }
        // ��Ӵٿ� �ʱ� ���ð� ����
        resolutionDropdown.value = 0; // ù��° �ɼ� ����
        resolutionDropdown.RefreshShownValue(); // UI ����

        // ��Ӵٿ� ���� �̺�Ʈ ������ �߰�
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // ��ư �ؽ�Ʈ �ʱ�ȭ
        UpdateButtonText();

        // ���� �����̴� �ʱ�ȭ
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
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
    
    // ���� ���� UI ����
    public void UpdateSaveSlots()
    {
        for(int i = 0; i < slotTexts.Length; i++)
        {
            int slot = i + 1;
            if (GameManager.IsSlotEmpty(slot))
            {
                slotTexts[i].text = "Slot" + slot + " : Empty";
            }
            else
            {
                var data = GameManager.LoadGame(slot);
                slotTexts[i].text = "Slot " + slot + ": " + data.playerLife;
            }
        }
    }
    // ���� Ŭ��
    public void OnslotClicked(int slot)
    {
        if (GameManager.IsSlotEmpty(slot))
        {
            GameManager.SaveGame(slot); // �� �����͸� ����
            Debug.Log("Game Saved in Slot " + slot);
        }
        else
        {
            var data = GameManager.LoadGame(slot); // ������ ������� ���ӻ��¸� ������Ʈ 
            Debug.Log("Game Loaded from Slot " + slot + ": Level " + data.playerPosition);
        }

        UpdateSaveSlots();
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

    // ��ü ���� ����
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    // ���̺� �޴� ����
    public void OpenSaveMenu()
    {
        saveTab.SetActive(true);
        resolutionTab.SetActive(false);
        soundTab.SetActive(false);
    }
    // �ػ� �޴� ����
    public void OpenResolutionTab()
    {
        saveTab.SetActive(false);
        resolutionTab.SetActive(true);
        soundTab.SetActive(false);
    }
    // ���� �޴� ����
    public void OpenSoundTab()
    {
        saveTab.SetActive(false);
        resolutionTab.SetActive(false);
        soundTab.SetActive(true);
    }
    
}
