using UnityEngine;
using UnityEngine.TextCore.Text;


public class WeaponController : MonoBehaviour
{
    //���Ƶ��̴� ���� (�ݶ��̴��� ����)
    //public GameObject AbsorbRange;

    //���Ƶ��̰� �ִ� ���¸� ����
    private bool isAbsorbing = false;

    //3���� ���Ҹ� ���Ƶ��̰� �ִ� ���� ����
    private bool isRockActive = false;
    private bool isGrassActive = false;
    private bool isWaterActive = false;

    //public GameObject RockEffect;
    //public GameObject WaterEffect;
    //public GameObject GrassEffect;

    //�������� ���� �ӵ�
    public float FillSpeed = 0.1f;

    //���� ���� ������
    public Transform GunPivot;
    public Transform Gun;

    //���� ���
    public int WeaponMode;
    
    private void Start()
    {
        //���Ƶ��̴� ������ ����
        //AbsorbRange.SetActive(false);
    }

    private void Update()
    {
       
        //���콺�� ���� ��ġ�� Ž���ϴ� ���� ���� (����ī�޶� Ȱ��)
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = UnityEngine.Camera.main.ScreenToWorldPoint
        (new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, transform.position.z - GunPivot.position.z));
        

        // �ѱ��� ȸ������ ����
        Vector2 direction = new Vector2(
             mouseWorldPosition.x - GunPivot.position.x,
             mouseWorldPosition.y - GunPivot.position.y
         );
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //���� �Ӹ� ���� �ٴ� �Ʒ��� �������� ���� �����ϴ� ����
        bool shouldFlipGun = angle > 90 || angle < -90;

        //�������� ���� ����
        if (shouldFlipGun)
        {
            Gun.rotation = Quaternion.Euler(180, 0, -angle); // ���� ������
        }
        else
        {
            Gun.rotation = Quaternion.Euler(0, 0, angle); // ���� ���������� ȸ��
        }

        //������ư�� Ŭ������ �� ���Ƶ��̴� ����� ����
        //if (Input.GetMouseButton(1))
       // {
        //    Absorb();
        //}

       // if(Input.GetMouseButton(0))
       // {
       //     WeaponSelect();
       // }
    }

    private void Absorb()
    {
        isAbsorbing = true;
        //AbsorbRange.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isAbsorbing) return;

        // �±׿� ���� ȿ�� Ȱ��ȭ
        switch (other.tag)
        {
            case "Rock":
                //ActivateEffect(RockEffect);
                isRockActive = true;
                FillGauge();
                break;
            case "Tree":
               //ActivateEffect(GrassEffect);
                isGrassActive = true;
                FillGauge();
                break;
            case "Water":
               // ActivateEffect(WaterEffect);
                isWaterActive = true;
                FillGauge();
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // SuckEffect�� �浹�� Collider���� �������� �� ȣ��
        Debug.Log($"OnTriggerExit2D ȣ��: {other.name}");

        // ��� ȿ���� ��Ȱ��ȭ
        DeactivateAllEffects();
    }

    private void ActivateEffect(GameObject effect)
    {
        // �ٸ� ȿ�� ��Ȱ��ȭ
        DeactivateAllEffects();

        // ���ϴ� ȿ���� Ȱ��ȭ
        effect.SetActive(true);
    }

    private void DeactivateAllEffects()
    {
        //RockEffect.SetActive(false);
        //GrassEffect.SetActive(false);
        //WaterEffect.SetActive(false);
        isRockActive = false;
        isGrassActive = false;
        isWaterActive = false;
    }

    private void FillGauge()
    {
        
    }


    private void WeaponSelect()
    {
        switch (WeaponMode)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
        }
    }

    private void RockBullet()
    {

    }

    private void RockBomb()
    {

    }

    private void WaterSpray()
    {

    }

    private void GrassRope()
    {

    }

    private void RockPlatform()
    {

    }
    private void HealPotion()
    {

    }
}
