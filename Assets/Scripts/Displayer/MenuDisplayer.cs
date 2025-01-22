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

    public TextMeshProUGUI[] slotTexts; // ���̺� ���� ���¸� ǥ���� TextMeshPro �迭 (3��)
    public GameObject saveMenuPanel;   // ���̺� �޴� UI �г�


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

        UpdateSaveSlots();
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
    // ���� ���� ����
    public void UpdateSaveSlots()
    {
        for (int i = 0; i < slotTexts.Length; i++)
        {
            int slot = i + 1;
            if (GameManager.IsSlotEmpty(slot))
            {
                slotTexts[i].text = "Slot " + slot + ": Empty";
            }
            else
            {
                var data = GameManager.LoadGame(slot);
                slotTexts[i].text = "Slot " + slot + ": " + data.playerPosition;
            }
        }
    }

    // ���� Ŭ��
    public void OnSlotClicked(int slot)
    {
        if (GameManager.IsSlotEmpty(slot))
        {
            GameManager.SaveGame(slot); // �� �����͸� ����
            Debug.Log("Game Saved in Slot " + slot);
        }
        else
        {
            var data = GameManager.LoadGame(slot); // �����͸� �ҷ���
            Debug.Log("Game Loaded from Slot " + slot + ": Level " + data.playerLife);
            // ���⼭ �����͸� ������� ���� ���¸� ������Ʈ
        }

        UpdateSaveSlots(); // UI ����
    }

    // ���̺� �޴� ����
    public void OpenSaveMenu()
    {
        soundTab.SetActive(false);
        resolutionTab.SetActive(false);
        saveMenuPanel.SetActive(true);
        UpdateSaveSlots();
    }
    // ���� �� ����
    public void OpenSoundTab()
    {
        soundTab.SetActive(true);
        resolutionTab.SetActive(false);
        saveMenuPanel.SetActive(false);
    }
    // �ػ� �� ����
    public void OpenResolutionTab()
    {
        soundTab.SetActive(false);
        resolutionTab.SetActive(true);
        saveMenuPanel.SetActive(false);
    }
}
