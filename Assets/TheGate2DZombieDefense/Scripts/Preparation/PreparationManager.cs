using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreparationManager : MonoBehaviour
{
  public static PreparationManager Instance;

  public GameObject inventoryItem;
  public GameObject inventoryItemContent;
  public GameObject inventoryItemSlots;

  public GameObject inventoryItemContentForPlaying;

  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    GameManager.Instance.OnStateChange += LoadInventoryHandler;
    GameManager.Instance.OnStateChange += HandleMoveItemsToPlayingState;
  }

  private void OnDestroy()
  {
    GameManager.Instance.OnStateChange -= LoadInventoryHandler;
    GameManager.Instance.OnStateChange -= HandleMoveItemsToPlayingState;
  }

  void LoadInventory(List<string> guns, List<string> skills)
  {
    foreach (string gun in guns)
    {
      Debug.Log(gun + " is loaded to the inventory!");
      GameObject go = Instantiate(inventoryItem, inventoryItemContent.transform);
      ItemPreparation item = go.AddComponent<ItemPreparation>();
      item.itemType = ItemType.Gun;

      ItemData itemData = JsonUtility.FromJson<ItemData>(gun);

      foreach (Gun fe_gun in GunsAndSkills.Instance.guns)
      {
        if (fe_gun.name == itemData.name)
        {
          item.gunData = fe_gun;
        }
      }
    }

    foreach (string skill in skills)
    {
      Debug.Log(skill + " is loaded to the inventory!");
      GameObject go = Instantiate(inventoryItem, inventoryItemContent.transform);
      ItemPreparation item = go.AddComponent<ItemPreparation>();
      item.itemType = ItemType.Skill;
    }
  }

  void LoadInventoryHandler(GameState state)
  {
    if (state == GameState.Preparation)
    {
      LoadInventory(CurrentPlayerData.Instance.data.gunInventory, CurrentPlayerData.Instance.data.skillInventory);
    }
  }

  void HandleMoveItemsToPlayingState(GameState state)
  {
    if (state == GameState.Playing)
    {
      Transform inventory = inventoryItemSlots.transform;

      List<Transform> inventoryChildren = inventory.Cast<Transform>().ToList();

      var indexedInventoryChildren = inventoryChildren.Select((trans, index) => new { Transform = trans, Index = index });

      foreach (var itemSlot in indexedInventoryChildren)
      {
        if (itemSlot.Transform.childCount >= 1)
        {
          itemSlot.Transform.GetChild(0).gameObject.transform.SetParent(inventoryItemContentForPlaying.transform.GetChild(itemSlot.Index).transform);
        }
      }
    }
  }
}
