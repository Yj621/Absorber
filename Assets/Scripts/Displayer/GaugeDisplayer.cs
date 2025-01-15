using UnityEngine;
using UnityEngine.UI;

public class GaugeDisplayer : MonoBehaviour
{
    // ������ �̹���
    public Image rockGaugeImage; 
    public Image grassGaugeImage;
    public Image waterGaugeImage;

    private void Update()
    {
        // ������ �������� �̹���
        rockGaugeImage.fillAmount = WeaponController.Instance.RockGauge / WeaponController.Instance.MaxGauge;
        grassGaugeImage.fillAmount = WeaponController.Instance.GrassGauge / WeaponController.Instance.MaxGauge;
        waterGaugeImage.fillAmount = WeaponController.Instance.WaterGauge / WeaponController.Instance.MaxGauge;
    }
}
