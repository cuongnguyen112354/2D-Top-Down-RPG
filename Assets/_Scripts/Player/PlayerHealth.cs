using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }
    public int CurrentHealth
    { 
        get => _currentHealth; 
        set
        {
            if (value > maxHealth)
                _currentHealth = maxHealth;
            else if (value < 0)
                _currentHealth = 0;
            else
                _currentHealth = value;

            UpdateHealthSlider();
        }
    }

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    const string HEALTH_SLIDER_TEXT = "Heart Slider";
    readonly int DEADTH_HASH = Animator.StringToHash("Death");

    private Slider healthSlider;
    private int _currentHealth;
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
        _currentHealth = maxHealth;

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
        if (_currentHealth < maxHealth)
        {
            _currentHealth += 1;
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
        _currentHealth -= damageAmount;
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
        healthSlider.value = _currentHealth;
    }

    private void CheckIfPlayerDeath()
    {
        if (_currentHealth <= 0 && !isDead)
        {
            _currentHealth = 0;
            isDead = true;
            canTakeDamage = false;
            Destroy(ActiveWeapon.Instance.gameObject);
            AudioManager.Instance.GameOverSFX();
            GetComponent<Animator>().SetTrigger(DEADTH_HASH);
            StartCoroutine(DeadthLoadSceneRoutine());
        }
    }

    private IEnumerator DeadthLoadSceneRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.BackToMainMenu();
    }
}
