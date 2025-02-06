using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class SaveLoad
{
    private static string savePath = Application.persistentDataPath + "/playerData.json";

    public static void Save(PlayerData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, jsonData);
        Debug.Log("Data saved at: " + savePath);
    }

    public static PlayerData Load()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }

        Debug.LogWarning("Save file not found!");
        return null;
    }
}

