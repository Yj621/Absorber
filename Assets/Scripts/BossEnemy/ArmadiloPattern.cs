using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ArmadiloPattern : MonoBehaviour
{
    private enum BossState { Idle, Moving, Attacking }
    private enum RangeState { Out, Far, Close, Air, Back}

    private BossState currentState = BossState.Idle;
    private RangeState rangeState = RangeState.Out;

    private Queue<IEnumerator> actionQueue = new Queue<IEnumerator>();
    private bool isExecutingAction = false;
    private bool isCooldown = false;

    [SerializeField] private GameObject CloseRange;
    [SerializeField] private GameObject FarRange;
    [SerializeField] private GameObject AirRange;
    [SerializeField] private GameObject BackRange;

    [SerializeField] private GameObject[] StompEffect;
    [SerializeField] private float effectDuration = 0.33f;

    [SerializeField] private GameObject IceJumpEffect;

    [SerializeField] private GameObject FrogJumpEffect;

    [SerializeField] private BoxCollider2D FrogSpwanPoint;
    [SerializeField] private GameObject FrogPrefab;
    public int maxDrops = 10;

    [SerializeField] private GameObject spinePrefab; // ���� ������
    [SerializeField] private Transform[] spawnPoints; // ���� �߻� ����
    [SerializeField] private float fireRate = 0.5f; // ���� �߻� ����
    [SerializeField] private int totalSpines = 5;

    private Animator ani;
    private bool isTransitioning = false;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private Vector3 direction;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float cooldownTime = 3f;
    public PlayerController player;
    public EnemyStateMachine a_stateMachine;
    public EnemyStateMachine b_stateMachine;
    public EnemyStateMachine f_stateMachine;

    [SerializeField] private GameObject PlayerEnter;

    private void Awake()
    {
    }
    private void Start()
    {
        if (gameObject.CompareTag("ArmadilloBoss")) // �̸��� "Armadillo"�� ���
        {
            a_stateMachine = GetComponent<BaseEnemy>().stateMachine;
        }
        else if (gameObject.CompareTag("CatapillarBoss")) // �̸��� "Armadillo"�� ���
        {
            b_stateMachine = GetComponent<BaseEnemy>().stateMachine;
        }
        else if (gameObject.CompareTag("FrogBoss")) // �̸��� "Armadillo"�� ���
        {
            f_stateMachine = GetComponent<BaseEnemy>().stateMachine;
        }
        player = FindFirstObjectByType<PlayerController>();
    
        playerTransform = player.transform;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        currentState = BossState.Idle;
        rangeState = RangeState.Out;
        StartCoroutine(ProcessActions());

        foreach (var effect in StompEffect)
        {
            effect.SetActive(false);
        }

        if (gameObject.CompareTag("CatapillarBoss")) // �̸��� "Armadillo"�� ���
            IceJumpEffect.SetActive(false);

        if (gameObject.CompareTag("FrogBoss")) // �̸��� "Armadillo"�� ���
            FrogJumpEffect.SetActive(false);
    }

    protected virtual void Update()
    {
        if (currentState == BossState.Moving)
        {
            ani.SetTrigger("Walk");
        }
    }

    private void FixedUpdate()
    {
        if (currentState == BossState.Moving || rangeState == RangeState.Back)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        if (rangeState == RangeState.Back || rangeState == RangeState.Far || rangeState == RangeState.Air)
        {
            currentState = BossState.Moving;

            // �÷��̾ ���󰡸� ���� ��ȯ
            direction = (playerTransform.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;

            // ������ ���� ��ȯ
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-12, 12, 1); // ������ ����
            }
            else
            {
                transform.localScale = new Vector3(12, 12, 1); // ���� ����
            }
        }
    }

  
    public void ActivateMovingState()
    {
        currentState = BossState.Moving;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCooldown && !isExecutingAction)
        {
            if (collision.IsTouching(CloseRange.GetComponent<Collider2D>()))
            {
                rangeState = RangeState.Close;
                EnqueueAction(CloseAttack());
            }
            else if (collision.IsTouching(FarRange.GetComponent<Collider2D>()))
            {
                rangeState = RangeState.Far;
                EnqueueAction(FarAttack());
            }
            else if (collision.IsTouching(AirRange.GetComponent<Collider2D>()))
            {
                rangeState = RangeState.Air;
                EnqueueAction(AirAttack());
            }
            else if (collision.IsTouching(BackRange.GetComponent<Collider2D>()))
            {
                rangeState = RangeState.Back;
            }
            else
            {
                rangeState = RangeState.Out; // ���� ��
            }
        }
    }



    private IEnumerator ProcessActions()
    {
        while (true)
        {
            if (actionQueue.Count > 0 && !isExecutingAction && !isCooldown)
            {
                var action = actionQueue.Dequeue();
                yield return StartCoroutine(action);
            }
            yield return null;
        }
    }


    private void EnqueueAction(IEnumerator action)
    {
        if (!isExecutingAction && !isCooldown)
        {
            actionQueue.Enqueue(action);
        }
    }

    private IEnumerator CloseAttack()
    {
        isExecutingAction = true;
        currentState = BossState.Attacking;
        rb.linearVelocity = Vector2.zero;
        // �ٰŸ� ���� �ִϸ��̼� ����
        if (gameObject.CompareTag("ArmadilloBoss")) // �̸��� "Armadillo"�� ���
        {
            Stomp();
        }
        else if (gameObject.CompareTag("BugBoss")) // Ư�� �±׷� ����
        {
            IceAttack();
        }
        else if (gameObject.CompareTag("FrogBoss")) // Ư�� �±׷� ����
        {
            FrogTounge();
        }
        yield return new WaitForSeconds(2f); // ���� �ð�

        StartCooldown();
    }

    private IEnumerator FarAttack()
    {
        isExecutingAction = true;
        currentState = BossState.Attacking;
        rb.linearVelocity = Vector2.zero;
        // �� �Ÿ� ���� �ִϸ��̼� ����
        if (gameObject.CompareTag("ArmadilloBoss"))// �̸��� "Armadillo"�� ���
        {
            Angry();
        }
        else if (gameObject.CompareTag("BugBoss")) // Ư�� �±׷� ����
        {
            IceShoot();
        }
        else if (gameObject.CompareTag("FrogBoss")) // Ư�� �±׷� ����
        {
            FrogJump();
        }
        yield return new WaitForSeconds(2f); // ���� �ð�
        StartCooldown();
    }

    private IEnumerator AirAttack()
    {
        isExecutingAction = true;
        currentState = BossState.Attacking;
        rb.linearVelocity = Vector2.zero;
        // ���� ���� �ִϸ��̼� ����
        if (gameObject.CompareTag("ArmadilloBoss")) // �̸��� "Armadillo"�� ���
        {
            Spine();
        }
        else if (gameObject.CompareTag("BugBoss")) // Ư�� �±׷� ����
        {
            IceDrop();
        }
        else if (gameObject.CompareTag("FrogBoss")) // Ư�� �±׷� ����
        {
            FrogSpwan();
        }
        yield return new WaitForSeconds(2f); // ���� �ð�
        StartCooldown();
    }

    private void StartCooldown()
    {
        isExecutingAction = false; // ���� �ൿ ����

        // ���� �� �ٽ� �̵� ���·� ��ȯ (Idle ���·� ���� ����)
        if (rangeState == RangeState.Far || rangeState == RangeState.Air || rangeState == RangeState.Back)
        {
            currentState = BossState.Moving;
        }
        else
        {
            currentState = BossState.Idle;
        }

        StartCoroutine(Cooldown()); // ��Ÿ�� ����
    }



    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;

        // ��Ÿ���� ������ �ٽ� ���� �����ϵ��� Ʈ����
        if (!isExecutingAction && actionQueue.Count > 0)
        {
            StartCoroutine(ProcessActions());
        }
    }


    private void Stomp()
    {
        a_stateMachine.TransitionTo(a_stateMachine.a_StompState);
        StartCoroutine(PlayStompEffects());
    }

    private void Angry()
    {
        a_stateMachine.TransitionTo(a_stateMachine.a_AngryState);
    }


    private void Spine()
    {
        a_stateMachine.TransitionTo(a_stateMachine.a_SpineState);
        StartCoroutine(SpineAttackRoutine());
    }

    private IEnumerator PlayStompEffects()
    {
        yield return new WaitForSeconds(2.35f);
        foreach (var effect in StompEffect)
        {
            // ����Ʈ Ȱ��ȭ
            effect.SetActive(true);

            // ������ �ð� ���� Ȱ�� ���� ����
            yield return new WaitForSeconds(effectDuration);

            // ����Ʈ ��Ȱ��ȭ
            yield return new WaitForSeconds(effectDuration); // ȿ�� ���� �ð�
            effect.SetActive(false); // ȿ�� ��Ȱ��ȭ
            yield return new WaitForSeconds(0.2f);
        }
    }

        private IEnumerator SpineAttackRoutine()
    {
        for (int i = 0; i < totalSpines; i++)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                // ���� ����
                GameObject spine = Instantiate(spinePrefab, spawnPoint.position, spawnPoint.rotation);
                spine.transform.right = spawnPoint.right; // �߻� ���� ����
            }

            yield return new WaitForSeconds(fireRate); // �߻� ���� ���
        }

    }

    private IEnumerator IceJumpEffects()
    {
            IceJumpEffect.SetActive(true);
            yield return new WaitForSeconds(1.5f); // ȿ�� ���� �ð�
            IceJumpEffect.SetActive(false); // ȿ�� ��Ȱ��ȭ
    }

    private IEnumerator IceDropEffects()
    {
        IceJumpEffect.SetActive(true);
        yield return new WaitForSeconds(1.5f); // ȿ�� ���� �ð�
        IceJumpEffect.SetActive(false); // ȿ�� ��Ȱ��ȭ
    }

    private IEnumerator FrogJumpEffects()
    {
        yield return new WaitForSeconds(1.25f);
        FrogJumpEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f); // ȿ�� ���� �ð�
        FrogJumpEffect.SetActive(false); // ȿ�� ��Ȱ��ȭ
    }
    private IEnumerator FrogSpwanEffects()
    {
        yield return new WaitForSeconds(0.33f);

        float duration = 1f;
        float elapsedTime = 0f;
        int drops = 0;

        while (elapsedTime < duration && drops < maxDrops)
        {
            Vector3 randomPosition = GetRandomPositionInCollider();
            Instantiate(FrogPrefab, randomPosition, Quaternion.identity);

            drops++;
            elapsedTime += Time.deltaTime; // �� ������ �ð� �߰�
            yield return new WaitForSeconds(0.1f); // ���� ���� (���⼱ 0.1�ʸ��� ����)
        }
    }
    private Vector2 GetRandomPositionInCollider()
    {
        // �ݶ��̴��� �߽ɰ� ũ�� ��������
        Vector2 center = FrogSpwanPoint.bounds.center;
        Vector2 size = FrogSpwanPoint.bounds.size;

        // �ݶ��̴� ���� ������ ���� ��ġ ���
        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);

        return new Vector2(randomX, randomY);
    }

    private void IceAttack()
    {
        b_stateMachine.TransitionTo(b_stateMachine.b_AttackState);
    }

    private void IceDrop()
    {
        b_stateMachine.TransitionTo(b_stateMachine.b_DropState);
        StartCoroutine(IceDropEffects());
    }

    private void IceShoot()
    {
        if (b_stateMachine == null)
        {
            Debug.LogError("b_stateMachine�� null�Դϴ�! IceShoot ���� �Ұ�.");
            return;
        }

        if (b_stateMachine.b_ShootState == null)
        {
            Debug.LogError("b_ShootState�� null�Դϴ�! ���� ��ȯ �Ұ�.");
            return;
        }

        b_stateMachine.TransitionTo(b_stateMachine.b_ShootState);
    }



    private void FrogJump()
    {
        f_stateMachine.TransitionTo(f_stateMachine.f_JumpState);
        StartCoroutine(FrogJumpEffects());
    }

    private void FrogTounge()
    {
        f_stateMachine.TransitionTo(f_stateMachine.f_ToungeState);
    }

    private void FrogSpwan()
    {
        f_stateMachine.TransitionTo(f_stateMachine.f_SpwanState);
        StartCoroutine(FrogSpwanEffects());
    }
 }
