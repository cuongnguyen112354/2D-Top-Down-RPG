using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool FinalScene { get => finalScene; set => finalScene = value; }
    public int EnemyCount { get; set; }

    private bool finalScene = false;

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
        GameObject.Find("Gameplay UI")?.gameObject.SetActive(false);
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
        InActiveGameObjects();
        UIFade.Instance.FadeToBlack();
        StartCoroutine(LoadMenuScene());
    }

    private IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(1f);
        Destroy(PlayerController.Instance.gameObject);
        SceneManager.LoadScene("Main Menu");
    }
}
