using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private PlayerControls playerControls;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {        
        UIFade.Instance.FadeToClear();
        playerControls.Gameplay.Quit.performed += _ => QuitGame();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    public void DisableObjects()
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        Projectile[] projectiles = FindObjectsOfType<Projectile>();
        GrapeProjectile[] grapeProjectile = FindObjectsOfType<GrapeProjectile>();

        foreach (EnemyAI enemy in enemies)
            enemy.enabled = false;
        
        foreach (Projectile projectile in projectiles)
            projectile.enabled = false;

        foreach (GrapeProjectile grape in grapeProjectile)
            grape.StopAllCoroutines();
    }

    public void DestroyGameManager()
    {
        Destroy(gameObject);
    }

    public void InActiveGameObjects()
    {
        GameObject.Find("Active Inventory")?.gameObject.SetActive(false);
        GameObject.Find("Gold Coin Container")?.gameObject.SetActive(false);
        GameObject.Find("Heart Container")?.gameObject.SetActive(false);
        GameObject.Find("Stamina Container")?.gameObject.SetActive(false);
    }
}
