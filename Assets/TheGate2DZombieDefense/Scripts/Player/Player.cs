using System.Collections;
using System.Linq;
using Spine;
using Spine.Unity;
using UnityEngine;

public class Player : MonoBehaviour
{
  public static Player Instance;
  public GameObject bullet;
  public GameObject gunIK;

  public bool isReloading;
  //----
  public SkeletonMecanim mecanim;

  [SpineSlot]
  public string slotNameBarrelEnd;

  [SpineAttachment(slotField: "slotName")]
  public string attachmentNameBarrelEnd;
  //----
  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    GameManager.Instance.OnStateChange += VarReset;
  }

  private void OnDestroy()
  {
    GameManager.Instance.OnStateChange -= VarReset;
  }

  public void VarReset(GameState state)
  {
    if (state == GameState.Playing)
    {
      isReloading = false;
    }

    if (state == GameState.LevelEndWon
    || state == GameState.LevelEndLost)
    {
      StopAllCoroutines();
    }
  }

  //loadout should have update hud so after shooting ammo is updated graphically
  public void Shoot()
  {
    var currentEquippedGun = PlayerLoadout.Instance.GetCurrentEquippedGun();

    Vector3 gunPosition = PercySpine.GetPointAttachmentToUnityPosition(mecanim, slotNameBarrelEnd, attachmentNameBarrelEnd);
    if (PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().itemData.bulletCount >= 1
    && PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().currentMagazineBullet >= 1)
    {
      GameObject go = Instantiate(bullet, new Vector3(gunPosition.x,
      gunPosition.y, gunPosition.z),
      Quaternion.Euler(0f, 0f, PercySpine.GetPointAttachmentToUnityRotation(mecanim, slotNameBarrelEnd, attachmentNameBarrelEnd)));

      Gun gun = PercyScriptableObjectUtility.DeepCopy(PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().gunData);
      go.GetComponent<Bullet>().gun = gun;
      go.GetComponent<Bullet>().gun.damage = PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().gunData.damage;

      PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().itemData.bulletCount -= 1;
      PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().currentMagazineBullet -= 1;

      GameManager.Instance.UpdatePlayingUI();
    }
  }

  public void Reload()
  {
    var currentGun = PlayerLoadout.Instance.GetCurrentEquippedGun();
    if (!isReloading && currentGun.itemData.bulletCount > 0 &&
    currentGun.gunData.currentMagazineBullet != PlayerLoadout.Instance.currentItemEquipped
    .GetComponent<ItemPreparation>().currentMagazineBullet &&
    currentGun.itemData.bulletCount != PlayerLoadout.Instance.currentItemEquipped
          .GetComponent<ItemPreparation>().currentMagazineBullet)
    {
      isReloading = true;
      StartCoroutine(ReloadCoroutine());
    }
  }

  public IEnumerator ReloadCoroutine()
  {
    var currentGun = PlayerLoadout.Instance.GetCurrentEquippedGun();
    Debug.Log("Reloading... " + ": Wait for " + currentGun.gunData.reloadSpeed + " seconds");
    float reloadSpeed = currentGun.gunData.reloadSpeed;
    yield return new WaitForSeconds(reloadSpeed);

    if (currentGun.itemData.bulletCount >= currentGun.gunData.magazineCapacity)
    {
      PlayerLoadout.Instance.currentItemEquipped
          .GetComponent<ItemPreparation>().currentMagazineBullet = currentGun.gunData.magazineCapacity;
    }
    else
    {
      PlayerLoadout.Instance.currentItemEquipped
                .GetComponent<ItemPreparation>().currentMagazineBullet = currentGun.itemData.bulletCount;
    }

    GameManager.Instance.UpdatePlayingUI();
    Debug.Log("Gun reloaded!");
    isReloading = false;
  }
}
