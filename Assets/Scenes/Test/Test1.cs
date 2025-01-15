using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // ĳ������ ��������Ʈ ������

    [SerializeField] private float moveSpeed = 10f; // ĳ���� �̵� �ӵ�
    [SerializeField] private float jumpSpeed = 10f; // ĳ���� ���� �ӵ�
    [SerializeField] private float dashSpeed = 20f; // ĳ���� �뽬 �ӵ�
    [SerializeField] private float dashCooldown = 1f; // �뽬 ��Ÿ��
    private float dashDuration = 0.2f; // �뽬 ���� �ð�
    private float dashCooldownTimer; // �뽬 ��Ÿ���� ����ϱ� ���� Ÿ�̸�


    private Rigidbody2D rb; // ĳ������ Rigidbody2D

    private Vector2 moveDirection; // �Էµ� �̵� ����

    private bool isJump; // ���� ������ ���θ� ��Ÿ���� �÷���
    private bool isDashing; // �뽬 ������ ���θ� ��Ÿ���� �÷���

    private bool isSelecting; // Space�ٰ� ���� �������� ����
    private bool isFirstSelect = true; // ù ��° �������� ����
    public int currentSelection1 = 1; // ù ��° ���õ� �� (1, 2, 3)
    public int currentSelection2 = 1; // �� ��° ���õ� �� (1, 2, 3)

    private void Awake()
    {
        // ������Ʈ �ʱ�ȭ
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // ���� �ӽ��� �ʱ� ���¸� Idle�� ����
    }

    private void FixedUpdate()
    {
        // �뽬 ���� �ƴ� ��쿡�� �̵� ó��
        if (!isDashing)
        {
            ApplyMovement();
        }
    }

    private void Update()
    {
        // Space �Է� ó��
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isSelecting = true;
            Debug.Log(isFirstSelect ? "ù ��° ���� ����" : "�� ��° ���� ����");
        }
        else if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            isSelecting = false;

            if (isFirstSelect)
            {
                Debug.Log($"ù ��° ���� Ȯ��: {currentSelection1}");
                isFirstSelect = false; // ���� �������� ��ȯ
            }
            else
            {
                Debug.Log($"�� ��° ���� Ȯ��: {currentSelection2}");
                Combination(); // �� ��° ���� Ȯ�� �� ���� ����
                isFirstSelect = true; // ù ��° �������� �ʱ�ȭ
            }
        }

        // ���콺 �� �Է� ó�� (Space�ٸ� ������ �ִ� ����)
        if (isSelecting)
        {
            float scrollValue = Mouse.current.scroll.ReadValue().y;
            if (scrollValue > 0)
            {
                if (isFirstSelect)
                {
                    currentSelection1 = currentSelection1 >= 3 ? 1 : currentSelection1 + 1;
                    Debug.Log($"���� ù ��° ���� ����: {currentSelection1}");
                }
                else
                {
                    currentSelection2 = currentSelection2 >= 3 ? 1 : currentSelection2 + 1;
                    Debug.Log($"���� �� ��° ���� ����: {currentSelection2}");
                }
            }
            else if (scrollValue < 0)
            {
                if (isFirstSelect)
                {
                    currentSelection1 = currentSelection1 <= 1 ? 3 : currentSelection1 - 1;
                    Debug.Log($"���� ù ��° ���� ����: {currentSelection1}");
                }
                else
                {
                    currentSelection2 = currentSelection2 <= 1 ? 3 : currentSelection2 - 1;
                    Debug.Log($"���� �� ��° ���� ����: {currentSelection2}");
                }
            }
        }

        // ĳ���� ��������Ʈ ���� ����
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Mathf.Abs(UnityEngine.Camera.main.transform.position.z))
        );

        if ((mouseWorldPosition.x > transform.position.x && spriteRenderer.flipX) ||
            (mouseWorldPosition.x < transform.position.x && !spriteRenderer.flipX))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        // �뽬 ��Ÿ�� Ÿ�̸� ������Ʈ
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    //select1���� �������� �޼ҵ�
    public int GetSelect1()
    {
        return currentSelection1;
    }

    //select2���� �������� �޼ҵ�
    public int GetSelect2()
    {
        return currentSelection2;
    }


    private void Combination()
    {
        Dictionary<(int, int), string> combinations = new Dictionary<(int, int), string>
        {
            { (1, 1), "water"}, // ��+��
            { (2, 2), "treeVine" }, // Ǯ+Ǯ
            { (3, 3), "rockBomb" }, // ����+����
            { (1, 2), "potion" }, // ��+Ǯ
            { (2, 1), "potion" }, // Ǯ+��
            { (2, 3), "platform" }, // Ǯ+����
            { (3, 2), "platform" }, // ����+Ǯ
            { (1, 3), "bullet" }, // ��+����
            { (3, 1), "bullet" }  // ����+��
        };

        var selectedCombination = (currentSelection1, currentSelection2);

        if (combinations.TryGetValue(selectedCombination, out string result))
        {
            Debug.Log($"����: {result}");
            WeaponController.Instance.WeaponMode = result;
        }
        else
        {
            Debug.Log("��ȿ���� ���� �����Դϴ�.");
        }
    }

    // �̵� �Է� ó��
    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }

    // ���� �Է� ó��
    public void OnJump(InputValue value)
    {
        if (value.isPressed && !isJump)
        {
            isJump = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveSpeed);
        }
    }

    // �뽬 �Է� ó��
    public void OnSprint(InputValue value)
    {
        if (value.isPressed && !isDashing && dashCooldownTimer <= 0)
        {
            StartCoroutine(Dash()); // �뽬 �ڷ�ƾ ����
        }
    }

    // �̵� ���� ó��
    private void ApplyMovement()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocity.y); // �̵� �ӵ� ����
    }   

    // �뽬 ���� ó��
    private IEnumerator Dash()
    {
        isDashing = true; // �뽬 �÷��� ����
        Vector2 dashForce = new Vector2(moveDirection.x * dashSpeed, 0); // �뽬 ���� ����
        rb.AddForce(dashForce, ForceMode2D.Impulse); // �뽬 �� ����
        yield return new WaitForSeconds(dashDuration); // �뽬 ���� �ð� ���
        isDashing = false; // �뽬 �÷��� ����
        dashCooldownTimer = dashCooldown; // �뽬 ��Ÿ�� ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isJump = false;
        }
    }
}
