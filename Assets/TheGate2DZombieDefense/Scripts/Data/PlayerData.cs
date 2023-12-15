using UnityEngine;
using System.Collections.Generic;

public class PlayerData
{
  public List<string> gunInventory;
  public List<string> skillInventory;
  public int gold;
  public int dayLevel;

  public PlayerData(List<string> gunInventory, List<string> skillInventory, int gold, int dayLevel)
  {
    this.gunInventory = gunInventory;
    this.skillInventory = skillInventory;
    this.gold = gold;
    this.dayLevel = dayLevel;
  }
  public PlayerData()
  {
    ItemData pistol1 = new ItemData();
    pistol1.name = "Pistol1";
    pistol1.bulletCount = -1;
    string pistol1InJSONString = JsonUtility.ToJson(pistol1);

    ItemData rifle1 = new ItemData();
    rifle1.name = "Rifle1";
    rifle1.bulletCount = 50;
    string rifle1InJSONString = JsonUtility.ToJson(rifle1);

    ItemData heal = new ItemData();
    heal.name = "Heal";
    string healInJSONString = JsonUtility.ToJson(heal);

    this.gunInventory = new List<string>
    {
      pistol1InJSONString,
      rifle1InJSONString
    };

    this.skillInventory = new List<string>()
    {
      healInJSONString
    };

    this.gold = 0;
    this.dayLevel = 1;
  }
}
