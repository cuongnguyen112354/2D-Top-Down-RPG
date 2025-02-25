using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool FinalScene { get => finalScene; set => finalScene = value; }
    public int EnemyCount { get; set; }
    public SceneData previousSceneData;

    private bool finalScene = false;

    private void Start()
    {
        GetComponent<SaveLoadSystem>().LoadGame();
    }

    public void EntranceScene()
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        EnemyCount = enemies.Length;
        FindObjectOfType<EnemyExisting>().UpdateEnemyCount();

        UIFade.Instance.FadeToClear();
    }

    public void ExitScene(string sceneToLoad)
    {
        DisableObjects();

        SaveSceneData(
            PlayerHealth.Instance.CurrentHealth,
            PlayerStamina.Instance.CurrentStamina, 
            EconomyManager.Instance.CurrentGold, 
            sceneToLoad
        );

        UIFade.Instance.FadeToBlack();
    }

    public void LoadPlayerData()
    {
        PlayerStamina.Instance.CurrentStamina = previousSceneData.stamina;
        PlayerHealth.Instance.CurrentHealth = previousSceneData.health;
        EconomyManager.Instance.CurrentGold = previousSceneData.goldCoin;
    }

    public void SaveSceneData(int health, int stamina, int goldCoin, string sceneName)
    {
        previousSceneData = new SceneData(health, stamina, goldCoin, sceneName);
    }

    public void DisableObjects()
    {
        Projectile[] projectiles = FindObjectsOfType<Projectile>();
        GrapeProjectile[] grapeProjectile = FindObjectsOfType<GrapeProjectile>();
        
        foreach (Projectile projectile in projectiles)
            projectile.enabled = false;

        foreach (GrapeProjectile grape in grapeProjectile)
            grape.StopAllCoroutines();

        GameObject.Find("Ghost")?.GetComponent<Shooter>().StopAttack();
    }

    public void InActiveGameObjects()
    {
        GameObject.Find("Gameplay Info UI")?.gameObject.SetActive(false);
    }

    public void DetectVictory()
    {
        if (EnemyCount > 0) return;

        if (!FinalScene)
        {
            OpenTheDoor();
            return;
        }
        
        SaveSceneData(5, 3, EconomyManager.Instance.CurrentGold, "Scene 1");
        BackToMainMenu();
    }

    private void OpenTheDoor()
    {
        AreaExit areaExit = FindObjectOfType<AreaExit>();

        areaExit.transform.Find("Minimap Icon Close").gameObject.SetActive(false);
        areaExit.transform.Find("Minimap Icon Open").gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        finalScene = false;
        DisableObjects();
        FindObjectOfType<PauseMenuController>().GetComponent<Canvas>().sortingOrder = -2;
        UIFade.Instance.FadeToBlack();
        StartCoroutine(LoadMenuScene());
    }

    private IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(1f);
        InActiveGameObjects();
        Destroy(PlayerController.Instance.gameObject);
        SceneManager.LoadScene("Main Menu");
    }
}
