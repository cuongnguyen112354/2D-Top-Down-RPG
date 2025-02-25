using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadSystem : MonoBehaviour
{
    private string path => Application.persistentDataPath + "/data.json";

    public void SaveGame()
    {
        string jsonData = JsonUtility.ToJson(GameManager.Instance.previousSceneData);
        File.WriteAllText(path, jsonData);
        Debug.Log("Game data saved.");
    }

    public void LoadGame()
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            GameManager.Instance.previousSceneData = JsonUtility.FromJson<SceneData>(jsonData);
            Debug.Log("Game data loaded.");
        }
    }
}
