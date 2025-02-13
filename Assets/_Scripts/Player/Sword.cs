using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimaPrefab;
    [SerializeField] private Transform slashAnimaSpawnPoint;
    [SerializeField] private Transform attackCollider;
    [SerializeField] private float swordAttackCD = .5f;

    private PlayerControls playerControls;
    private Animator animator;
    private PlayerController playerController;
    
    private ActiveWeapon activeWeapon;
    private GameObject slashAnima;
    private bool attackButtonDown, isAttacking = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            attackCollider.gameObject.SetActive(true);
            slashAnima = Instantiate(slashAnimaPrefab, slashAnimaSpawnPoint.position, Quaternion.identity);
            slashAnima.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDRoutine());
        }
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        isAttacking = false;
    }

    public void DoneAttack()
    {
        attackCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimaEvent()
    {
        slashAnima.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnima.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimaEvent()
    {
        slashAnima.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnima.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector2 mousePos = Input.mousePosition;
        float playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position).x;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * (Mathf.Rad2Deg / 2.3f);

        if (mousePos.x < playerScreenPoint)
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
        else 
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
