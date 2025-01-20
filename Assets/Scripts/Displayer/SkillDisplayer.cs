using UnityEngine;

public class SkillDisplayer : MonoBehaviour
{
    // ��ų ����â
    public GameObject skillPanel;

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
