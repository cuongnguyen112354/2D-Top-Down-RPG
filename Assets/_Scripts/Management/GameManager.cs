using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool FinalScene { get => finalScene; set => finalScene = value; }
    public int EnemyCount { get; set; }

    public SceneData previousSceneData;

    private bool finalScene = false;

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
        if (EnemyCount > 0 || !FinalScene)
            return;

        BackToMainMenu();
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
