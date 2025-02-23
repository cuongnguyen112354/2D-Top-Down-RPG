using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyExisting : MonoBehaviour
{
    private TextMeshProUGUI enemyCountText;

    private void Awake()
    {
        enemyCountText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateEnemyCount()
    {
        enemyCountText.text = $"Enemies: {GameManager.Instance.EnemyCount}";
    }
}
