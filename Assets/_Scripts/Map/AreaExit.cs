using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            DisableObjects();
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        while (waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
    
    private void DisableObjects()
    {
        EnemyPathFinding[] enemies = FindObjectsOfType<EnemyPathFinding>();
        Projectile[] projectiles = FindObjectsOfType<Projectile>();

        foreach (EnemyPathFinding enemy in enemies)
        {
            enemy.enabled = false;

            Shooter shooter = enemy.gameObject.GetComponent<Shooter>();
            
            if (shooter)
                shooter.StopAttack();
        }
        
        foreach (Projectile projectile in projectiles)
            projectile.enabled = false;
    }
}
