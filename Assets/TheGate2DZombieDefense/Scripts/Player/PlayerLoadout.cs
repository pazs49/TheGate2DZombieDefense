using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Current guns/skills in playing
public class PlayerLoadout : MonoBehaviour
{
  public static PlayerLoadout Instance;

  public List<GameObject> guns;
  public List<GameObject> skills;

  public GameObject currentItemEquipped;

  [Space(20)]
  public TextMeshProUGUI currentGoAmmoText;

  const string GUN_PATH = "guns/";

  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    GameManager.Instance.OnStateChange += Init;
    GameManager.Instance.OnStateChange += HandleLoadout;
  }

  private void OnDestroy()
  {
    GameManager.Instance.OnStateChange -= HandleLoadout;
    GameManager.Instance.OnStateChange -= Init;
  }

  public int GetCurrentEquippedGunAmmo(string gunName = "default")
  {
    int currentEquippedGunAmmo;
    if (currentItemEquipped.GetComponent<ItemPreparation>().itemData != null)
    {
      currentEquippedGunAmmo = currentItemEquipped.GetComponent<ItemPreparation>().itemData.bulletCount;
    }
    else
    {
      currentEquippedGunAmmo = CurrentPlayerData.Instance.GetGunData(gunName).bulletCount;
    }
    return currentEquippedGunAmmo;
  }

  void Init(GameState state)
  {
    if (state == GameState.Playing)
    {
      EquipGun(guns.First(), guns.First().GetComponent<ItemPreparation>().gunData.name.ToLower(), currentGoAmmoText);
    }
  }

  void HandleLoadout(GameState state)
  {
    if (state == GameState.Playing)
    {
      foreach (GameObject gun in guns)
      {
        HandleEquipGun(gun, gun.GetComponent<ItemPreparation>().gunData.name.ToLower(), currentGoAmmoText);
      }
      foreach (GameObject skill in skills)
      {

      }
    }
  }

  void HandleEquipGun(GameObject gunGo, string gunName, TextMeshProUGUI currentGoAmmoText)
  {
    Button button = gunGo.GetComponent<Button>();
    button.onClick.AddListener(() =>
    {
      EquipGun(gunGo, gunName, currentGoAmmoText);
    });
  }
  void EquipGun(GameObject gunGo, string gunName, TextMeshProUGUI currentGoAmmoText)
  {
    currentItemEquipped = gunGo;

    currentGoAmmoText.text = "Ammo: " + GetCurrentEquippedGunAmmo(gunName);

    string gunPath = GUN_PATH + gunName;
    switch (gunGo.GetComponent<ItemPreparation>().gunData.type)
    {
      case GunType.Pistol:
        PlayerEquip.Instance.Pistol(gunPath);
        break;
      case GunType.Rifle:
        PlayerEquip.Instance.Rifle(gunPath);
        break;
      case GunType.Shotgun:
        break;
      case GunType.Sniper:
        break;
    }
  }
}
