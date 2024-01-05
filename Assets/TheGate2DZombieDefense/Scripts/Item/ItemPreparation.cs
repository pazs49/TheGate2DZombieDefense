using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreparation : MonoBehaviour
{
  public ItemType itemType;
  public ItemData itemData;

  public Gun gunData;
  public int currentMagazineBullet;

  public Skill skillData;

  bool picked;

  private void Start()
  {
    ShowItemName();

    AddOnClick();

    GameManager.Instance.OnStateChange += HandleRemoveButtonClickAfterPreparation;
    GameManager.Instance.OnStateChange += HandleLoadItemData;

    GameManager.Instance.OnStateChange += HandleMoveBackItemDataToSaveAsJSON;
  }

  private void OnDestroy()
  {
    GameManager.Instance.OnStateChange -= HandleRemoveButtonClickAfterPreparation;
    GameManager.Instance.OnStateChange -= HandleLoadItemData;

    GameManager.Instance.OnStateChange -= HandleMoveBackItemDataToSaveAsJSON;
  }

  void HandleButtonClick()
  {
    if (!picked)
    {
      picked = true;
      if (itemType == ItemType.Gun)
      {
        for (int i = 0; i < ItemSlots.Instance.gunSlots.Length; i++)
        {
          if (ItemSlots.Instance.gunSlots[i].transform.childCount == 0)
          {
            PlayerLoadout.Instance.guns.Add(gameObject);
            transform.SetParent(ItemSlots.Instance.gunSlots[i].transform);

            break;
          }
        }
      }
      else if (itemType == ItemType.Skill)
      {
        for (int i = 0; i < ItemSlots.Instance.skillSlots.Length; i++)
        {
          if (ItemSlots.Instance.skillSlots[i].transform.childCount == 0)
          {
            PlayerLoadout.Instance.skills.Add(gameObject);
            transform.SetParent(ItemSlots.Instance.skillSlots[i].transform);
            break;
          }
        }
      }
    }
    else
    {
      if (itemType == ItemType.Gun) PlayerLoadout.Instance.guns.Remove(gameObject);
      if (itemType == ItemType.Skill) PlayerLoadout.Instance.skills.Remove(gameObject);

      transform.SetParent(PreparationManager.Instance.inventoryItemContent.transform);

      picked = false;
    }
  }

  void AddOnClick()
  {
    Button button = GetComponent<Button>();
    button.onClick.AddListener(HandleButtonClick);
  }

  void HandleRemoveButtonClickAfterPreparation(GameState state)
  {
    if (state == GameState.Playing)
    {
      Button button = GetComponent<Button>();
      button.onClick.RemoveListener(HandleButtonClick);
    }
  }

  void HandleLoadItemData(GameState state)
  {
    if (state == GameState.Playing && itemType == ItemType.Gun)
    {
      itemData = CurrentPlayerData.Instance.GetGunData(gunData.name);
      currentMagazineBullet = itemData.bulletCount >= gunData.magazineCapacity ? gunData.magazineCapacity : itemData.bulletCount;
    }
    else if (state == GameState.Playing && itemType == ItemType.Skill)
    {
      itemData = CurrentPlayerData.Instance.GetSkillData(skillData.name);
    }

  }

  void ShowItemName()
  {
    if (itemType == ItemType.Gun)
      transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gunData.gunName;
    else if (itemType == ItemType.Skill)
      transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skillData.skillName;
  }

  void HandleMoveBackItemDataToSaveAsJSON(GameState state)
  {
    //Gun data
    if (state == GameState.LevelEndWon)
    {
      List<string> currentGunsData = CurrentPlayerData.Instance.data.gunInventory;

      List<string> updatedGunsData = currentGunsData.Select(gun =>
      {
        ItemData gunData = JsonUtility.FromJson<ItemData>(gun);

        if (itemData != null && itemData.name == gunData.name)
        {
          gunData = itemData;
        }

        string newStringGunData = JsonUtility.ToJson(gunData);
        return newStringGunData;
      }).ToList();

      CurrentPlayerData.Instance.data.gunInventory = updatedGunsData;
    }
  }
}

public enum ItemType
{
  Gun,
  Skill
}
