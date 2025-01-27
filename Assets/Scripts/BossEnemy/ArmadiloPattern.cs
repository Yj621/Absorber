using System.Collections;
using System.Collections.Generic;
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
    public EnemyStateMachine c_stateMachine;
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
            c_stateMachine = GetComponent<BaseEnemy>().stateMachine;
        }
        else if (gameObject.CompareTag("FrogBoss")) // �̸��� "Armadillo"�� ���
        {
            f_stateMachine = GetComponent<BaseEnemy>().stateMachine;
        }
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

        IceJumpEffect.SetActive(false);
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
        if (collision.gameObject.CompareTag("Player") && !isCooldown)
        {
            if (collision.IsTouching(CloseRange.GetComponent<Collider2D>()))
            {
                EnqueueAction(CloseAttack());
                rangeState = RangeState.Close;
            }
            else if (collision.IsTouching(FarRange.GetComponent<Collider2D>()))
            {
                EnqueueAction(FarAttack());
                rangeState = RangeState.Far;
            }
            else if (collision.IsTouching(AirRange.GetComponent<Collider2D>()))
            {
                EnqueueAction(AirAttack());
                rangeState = RangeState.Air;
            }
            else if (collision.IsTouching(BackRange.GetComponent<Collider2D>()))
            {
                Debug.Log("�޹���");
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
        else if (gameObject.CompareTag("CatapillarBoss")) // Ư�� �±׷� ����
        {
            IceStomp();
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
        else if (gameObject.CompareTag("CatapillarBoss")) // Ư�� �±׷� ����
        {
            IceDrop();
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
        else if (gameObject.CompareTag("CatapillarBoss")) // Ư�� �±׷� ����
        {
            IceJump();
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
        if(rangeState == RangeState.Far || rangeState == RangeState.Air)
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

    private void IceStomp()
    {
        c_stateMachine.TransitionTo(c_stateMachine.c_StompState);
    }

    private void IceDrop()
    {
        c_stateMachine.TransitionTo(c_stateMachine.c_DropState);
        StartCoroutine(IceDropEffects());
    }

    private void IceJump()
    {
        c_stateMachine.TransitionTo(c_stateMachine.c_JumpState);
        StartCoroutine(IceJumpEffects());
    }
    
    
    private void FrogJump()
    {

    }

    private void FrogTounge()
    {

    }

    private void FrogSpwan()
    {

    }
 }
