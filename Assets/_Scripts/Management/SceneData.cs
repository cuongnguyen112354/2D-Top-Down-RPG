using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public int health;
    public int stamina;
    public int goldCoin;
    public string sceneName;

    public SceneData(int health, int stamina, int goldCoin, string sceneName)
    {
        this.health = health;
        this.stamina = stamina;
        this.goldCoin = goldCoin;
        this.sceneName = sceneName;
    }
}