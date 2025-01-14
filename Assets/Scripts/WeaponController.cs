using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;


public class WeaponController : MonoBehaviour
{
    //���Ƶ��̴� ���� (�ݶ��̴��� ����)
    public GameObject AbsorbRange;

    //���Ƶ��̰� �ִ� ���¸� ����
    private bool isAbsorbing = false;

    //3���� ���Ҹ� ���Ƶ��̰� �ִ� ���� ����
    private bool isRockActive = false;
    private bool isGrassActive = false;
    private bool isWaterActive = false;

    public GameObject RockEffect;
    public GameObject WaterEffect;
    public GameObject GrassEffect;

    //������ ���簪
    public float RockGauge = 0f;
    public float GrassGauge = 0f;
    public float WaterGauge = 0f;

    //������ �ִ밪
    public float MaxGauge = 100f;
    //�������� ���� �ӵ�
    public float FillSpeed = 1f;

    //���� ���� ������
    public Transform GunPivot;
    public Transform Gun;

    //���� ���
    public int WeaponMode;

    //�� ���� ����
    public GameObject bulletPrefab;
    public GameObject bombPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireCooldown = 0.5f;
    public float BombFireCooldown = 2f;
    private bool canShoot = true;
    public Vector3 mouseWorldPosition;


    //�ſ� �ʿ��� ������
    public GameObject hookPrefab; // �� ������
    public float hookSpeed = 20f; // �� �߻� �ӵ�
    public LayerMask attachableLayer; // ���� ���� �� �ִ� ���̾�
    public DistanceJoint2D joint; // ĳ���Ϳ� ���� DistanceJoint2D

    private GameObject hookInstance; // ������ �� �ν��Ͻ�
    private Rigidbody2D hookRb; // ���� Rigidbody2D
    private bool isHookAttached = false; // ���� �����Ǿ����� Ȯ��
    public LineRenderer lineRenderer;

    public static WeaponController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

        private void Start()
    {
        //���Ƶ��̴� ������ ����
        AbsorbRange.SetActive(false);
        RockEffect.SetActive(false);
        WaterEffect.SetActive(false);
        GrassEffect.SetActive(false);

      
    }

    private void Update()
    {

        //���콺�� ���� ��ġ�� Ž���ϴ� ���� ���� (����ī�޶� Ȱ��)
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        mouseWorldPosition = UnityEngine.Camera.main.ScreenToWorldPoint
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

        //���� �Ÿ� ������Ʈ
        if (isHookAttached)
        {
            UpdateJoint(); 
        }
        //���� ������ ������Ʈ
        UpdateLineRenderer();
    }

    //�������콺 ��������
    public void AbsorbClick()
    {
        isAbsorbing = true;
        AbsorbRange.SetActive(true);
    }

    //�������콺 ������
    public void AbsorbClickUp()
    {
        isAbsorbing = false;
        AbsorbRange.SetActive(false);

        RockEffect.SetActive(false);
        GrassEffect.SetActive(false);
        WaterEffect.SetActive(false);
    }

    public void OnAbsorbEffectTriggerStay(Collider2D other)
    {
        Debug.Log($"OnTriggerStay2D ȣ���: {other.name}");

        if (!isAbsorbing)
        {
            Debug.Log("isAbsorbing�� false���� ���ϵ�");
            return;
        }
        // �±׿� ���� ȿ�� Ȱ��ȭ
        switch (other.tag)
        {
            case "Rock":
                Debug.Log("�ν�");
                ActivateEffect(RockEffect);
                isRockActive = true;
                FillGauge();
                break;
            case "Grass":
                Debug.Log("�ν�");
                ActivateEffect(GrassEffect);
                isGrassActive = true;
                FillGauge();
                break;
            case "Water":
                Debug.Log("�ν�");
                ActivateEffect(WaterEffect);
                isWaterActive = true;
                FillGauge();
                break;
        }
    }

    public void OnAbsorbEffectTriggerExit(Collider2D other)
    {
       

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
        RockEffect.SetActive(false);
        GrassEffect.SetActive(false);
        WaterEffect.SetActive(false);
        isRockActive = false;
        isGrassActive = false;
        isWaterActive = false;
    }

    private void FillGauge()
    {
        if (isRockActive)
        {
            RockGauge += FillSpeed * Time.deltaTime;
            RockGauge = Mathf.Clamp(RockGauge, 0, MaxGauge);
            Debug.Log("�� ������: " + RockGauge);
        }

        if (isGrassActive)
        {
            GrassGauge += FillSpeed * Time.deltaTime;
            GrassGauge = Mathf.Clamp(GrassGauge, 0, MaxGauge);
            Debug.Log("Ǯ ������: " + GrassGauge);
        }

        if (isWaterActive)
        {
            WaterGauge += FillSpeed * Time.deltaTime;
            WaterGauge = Mathf.Clamp(WaterGauge, 0, MaxGauge);
            Debug.Log("�� ������: " + WaterGauge);
        }
    }

    //���� ���� (�޸��콺 ������ �� �ߵ�)
    public void WeaponSelect()
    {
        switch (WeaponMode)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                StartCoroutine(RockBullet());
                break;
            case 4:
                StartCoroutine(RockBomb());
                break;
            case 5:
                RopeActive();
                break;
            case 6:
                break;
        }
    }

    private IEnumerator RockBullet()
    {
        Debug.Log("�߻�");
        if (canShoot && RockGauge > 0 && WaterGauge > 0)
        {
            Debug.Log("�߻�2");
            WaterGauge -= 1f;
            RockGauge -= 1f;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // �Ѿ��� Rigidbody2D�� �ӵ� �߰�
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0; // �߷� ���� ����
                rb.linearVelocity = firePoint.right * bulletSpeed; // �߻� ����� �ӵ� ����
            }
            StartCoroutine(ShootCooldown());
            yield return new WaitForSeconds(2f);
            Destroy(bullet);
        }
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false; // �߻� �Ұ��� ���·� ��ȯ
        yield return new WaitForSeconds(fireCooldown); // ��ٿ� �ð� ��ٸ�
        canShoot = true; // �߻� ���� ���·� ��ȯ
    }

    private IEnumerator RockBomb()
    {
        Debug.Log("�߻�");
        if (canShoot && RockGauge > 0)
        {
            Debug.Log("�߻�2");
            RockGauge -= 5f;
            GameObject Bomb = Instantiate(bombPrefab, firePoint.position, firePoint.rotation);

            // �Ѿ��� Rigidbody2D�� �ӵ� �߰�
            Rigidbody2D rb = Bomb.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.right * bulletSpeed; // �߻� ����� �ӵ� ����
            }
            StartCoroutine(BombCooldown());
            yield return new WaitForSeconds(2f);
            Destroy(Bomb);
        }
    }

    IEnumerator BombCooldown()
    {
        canShoot = false; // �߻� �Ұ��� ���·� ��ȯ
        yield return new WaitForSeconds(BombFireCooldown); // ��ٿ� �ð� ��ٸ�
        canShoot = true; // �߻� ���� ���·� ��ȯ
    }

    private void WaterSpray()
    {

    }

    //���� ��� ���
    private void RopeActive()
    {
        Vector2 direction = (mouseWorldPosition - firePoint.position).normalized;

        hookInstance = Instantiate(hookPrefab, firePoint.position, Quaternion.identity);
        hookRb = hookInstance.GetComponent<Rigidbody2D>();
        hookRb.linearVelocity = direction * hookSpeed;

        // Raycast�� �浹 ���� ����
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, Mathf.Infinity, attachableLayer);
        if (hit.collider != null)
        {
            AttachHook(hit.point);
        }

        lineRenderer.enabled = true;
    }

    //���� �پ��� �� ���
    private void AttachHook(Vector2 attachPoint)
    {
        isHookAttached = true;

        // �� ����
        hookRb.linearVelocity = Vector2.zero;
        hookRb.position = attachPoint;

        // DistanceJoint2D ����
        joint.enabled = true;
        joint.connectedAnchor = attachPoint;
        joint.distance = Vector2.Distance(transform.position, attachPoint);
    }

    //���� ������ �� ���
    private void DetachHook()
    {
        isHookAttached = false;

        // �� �� ���� ����
        if (hookInstance != null)
        {
            Destroy(hookInstance);
        }
        joint.enabled = false;

        lineRenderer.enabled = false;
    }

    private void UpdateJoint()
    {
        // ĳ���Ϳ� �� �� �Ÿ� ����
        joint.distance = Vector2.Distance(transform.position, joint.connectedAnchor);
    }

    private void UpdateLineRenderer()
    {
        if (lineRenderer.enabled && hookInstance != null)
        {
            // LineRenderer�� �������� ���� ����
            lineRenderer.SetPosition(0, firePoint.position); // ������: ĳ������ �ѱ�
            lineRenderer.SetPosition(1, hookInstance.transform.position); // ����: �� ��ġ
        }
    }

    private void RockPlatform()
    {

    }
    private void HealPotion()
    {

    }
}
