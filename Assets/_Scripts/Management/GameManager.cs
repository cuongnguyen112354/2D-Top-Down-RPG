using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool FinalScene { get => finalScene; set => finalScene = value; }
    public int EnemyCount { get; set; }

    private PlayerControls playerControls;
    private bool finalScene = false;

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
        playerControls.Gameplay.Quit.performed += _ => QuitGame();
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

    public void InActiveGameObjects()
    {
        GameObject.Find("Gameplay UI")?.gameObject.SetActive(false);
    }

    public void DetectVictory()
    {
        if (EnemyCount > 0 || !FinalScene)
            return;

        DisableObjects();
        InActiveGameObjects();
        UIFade.Instance.FadeToBlack();
        StartCoroutine(LoadMenuScene());
    }

    private IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(1f);
        Destroy(PlayerController.Instance.gameObject);
        SceneManager.LoadScene("Menu Scene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
