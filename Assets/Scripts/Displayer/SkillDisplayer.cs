using UnityEngine;

public class SkillDisplayer : MonoBehaviour
{
    // ��ų ����â
    public GameObject skillPanel;

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
}
