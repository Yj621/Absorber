using UnityEngine;

public class AbsorbEffect : MonoBehaviour
{
    private WeaponController weaponController;

    private void Start()
    {
        // �θ��� WeaponController ��ũ��Ʈ ����
        weaponController = GetComponentInParent<WeaponController>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // �θ��� WeaponController�� �̺�Ʈ ����
        if (weaponController != null)
        {
            weaponController.OnAbsorbEffectTriggerStay(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �θ��� WeaponController�� �̺�Ʈ ����
        if (weaponController != null)
        {
            weaponController.OnAbsorbEffectTriggerExit(other);
        }
    }
}
