using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CurrentPlayerData : MonoBehaviour
{
  public static CurrentPlayerData Instance;

  public PlayerData data;

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
  }

  private void Start()
  {
    InitializeValues();
  }

  public ItemData GetGunData(string name)
  {
    int gunCount = CurrentPlayerData.Instance.data.gunInventory.Count;

    ItemData gunLookingFor = null;

    for (int i = 0; i < gunCount; i++)
    {
      ItemData gun = JsonUtility.FromJson<ItemData>(CurrentPlayerData.Instance.data.gunInventory[i]);
      if (gun.name.ToLower() == name.ToLower())
      {
        gunLookingFor = gun;
      }
    }

    return gunLookingFor;
  }

  void InitializeValues()
  {
    PlayerData playerData = SaveLoad.Instance.Load();

    data = playerData;
    Debug.Log("Data loaded: " + data);
  }
}
