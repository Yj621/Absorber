using TMPro;
using UnityEngine;

public class SkillDisplayer : MonoBehaviour
{
    // ������ �ھ� txt
    public TextMeshProUGUI energyCoreText;

    // ��ų ����â
    public GameObject skillPanel;

    private static SkillDisplayer instance;
    public static SkillDisplayer Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // ��ųâ ����
    public void OpenSkillMenu()
    {
        skillPanel.SetActive(true);
        Time.timeScale = skillPanel.activeSelf ? 0 : 1;
    }
    // ��ųâ ����
    public void CloseSkillMenu()
    {
        skillPanel.SetActive(false);
    }
}
