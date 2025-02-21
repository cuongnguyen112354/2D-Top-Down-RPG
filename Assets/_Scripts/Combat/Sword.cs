using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimaPrefab;
    [SerializeField] private WeaponInfo weaponInfo;

    private Animator animator;

    private Transform weaponCollider;
    private Transform slashAnimaSpawnPoint;
    private GameObject slashAnima;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimaSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }

    void Update()
    {
        MouseFollowWithOffset();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void Attack()
    {
        AudioManager.Instance.FlashAttackSFX();
        animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAnima = Instantiate(slashAnimaPrefab, slashAnimaSpawnPoint.position, Quaternion.identity);
        slashAnima.transform.parent = this.transform.parent;
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimaEvent()
    {
        slashAnima.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnima.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimaEvent()
    {
        slashAnima.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnima.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector2 mousePos = Input.mousePosition;
        float playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position).x;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * (Mathf.Rad2Deg / 2.3f);

        if (mousePos.x < playerScreenPoint)
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        else 
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
