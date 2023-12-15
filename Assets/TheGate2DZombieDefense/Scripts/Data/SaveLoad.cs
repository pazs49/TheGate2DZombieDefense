using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
  public static SaveLoad Instance;

  PlayerData playerData;

  string savePath;

  private void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
    }

    savePath = Application.persistentDataPath + "/playerData.json";
  }

  public void Save()
  {
    List<string> gunInventory = CurrentPlayerData.Instance.data.gunInventory;
    List<string> skillInventory = CurrentPlayerData.Instance.data.skillInventory;
    int gold = CurrentPlayerData.Instance.data.gold;
    int dayLevel = CurrentPlayerData.Instance.data.dayLevel;

    playerData = new PlayerData(gunInventory, skillInventory, gold, dayLevel);

    string json = JsonUtility.ToJson(playerData);
    File.WriteAllText(savePath, json);
  }

  public PlayerData Load()
  {
    if (File.Exists(savePath))
    {
      Debug.Log("Save file loaded!");
      string json = File.ReadAllText(savePath);
      playerData = JsonUtility.FromJson<PlayerData>(json);
    }
    else
    {
      Debug.Log("New save file created!");
      playerData = new PlayerData();
    }

    return playerData;
  }

}
