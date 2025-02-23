using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    const string HEALTH_SLIDER_TEXT = "Heart Slider";
    const string TOWN_TEXT = "Main Menu";
    readonly int DEADTH_HASH = Animator.StringToHash("Death");

    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockBack;
    private Flash flash;

    protected override void Awake()
    {
        base.Awake();
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;

        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
            TakeDamage(1, enemy.transform);
    }

    public void HealPlayer()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHealthSlider();
        }
    }
    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage) return;

        AudioManager.Instance.PlayerHurtSFX();
        ScreenShakeManager.Instance.ShakeScreen();
        knockBack.GetKnockBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !isDead)
        {
            currentHealth = 0;
            isDead = true;
            canTakeDamage = false;
            AudioManager.Instance.GameOverSFX();
            Destroy(ActiveWeapon.Instance.gameObject);
            GameManager.Instance.DisableObjects();
            GetComponent<Animator>().SetTrigger(DEADTH_HASH);
            StartCoroutine(DeadthLoadSceneRoutine());
        }
    }

    private IEnumerator DeadthLoadSceneRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        UIFade.Instance.FadeToBlack();
        GameManager.Instance.InActiveGameObjects();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        SceneManager.LoadScene(TOWN_TEXT);
    }
}
