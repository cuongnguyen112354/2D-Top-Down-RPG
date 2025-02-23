using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get => facingLeft; }

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private Vector2 movement;
    private float startingMoveSpeed;
    private Rigidbody2D rb;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private KnockBack knockBack;

    private bool facingLeft = false;
    private bool isDashing = false;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
        ActiveInventory.Instance.EquipStartingWeapon();

        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        GameManager.Instance.EnemyCount = enemies.Length;

        UIFade.Instance.FadeToClear();
    }

    void Update()
    {
        PlayerInput();
    }

    void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        if (knockBack.GettingKnockedBack || PlayerHealth.Instance.isDead)
            return;

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        float mousePos = Input.mousePosition.x;
        float playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position).x;

        if (mousePos < playerScreenPoint)
        {
            spriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing && PlayerStamina.Instance.CurrentStamina > 0)
        {
            PlayerStamina.Instance.UseStamina();
            isDashing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .3f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
