using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
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
}
