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
            int enemyNum = GameManager.Instance.EnemyCount;

            if (enemyNum > 0) return;

            GameManager.Instance.DisableObjects();
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);

            GameManager.Instance.SaveSceneData(
                PlayerHealth.Instance.CurrentHealth,
                PlayerStamina.Instance.CurrentStamina, 
                EconomyManager.Instance.CurrentGold, 
                sceneToLoad);

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
}
