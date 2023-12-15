using Spine.Unity;
using UnityEngine;

public class Player : MonoBehaviour
{
  public static Player Instance;

  private void Awake()
  {
    Instance = this;
  }

  //loadout should have update hud so after shooting ammo is updated graphically
  public void Shoot()
  {
    PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().itemData.bulletCount -= 1;
    GameManager.Instance.UpdatePlayingUI();
  }
}
