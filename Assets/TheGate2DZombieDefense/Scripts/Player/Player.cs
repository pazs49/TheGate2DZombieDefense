using Spine;
using Spine.Unity;
using UnityEngine;

public class Player : MonoBehaviour
{
  public static Player Instance;
  public GameObject bullet;
  public GameObject gunIK;
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

  }

  //loadout should have update hud so after shooting ammo is updated graphically
  public void Shoot()
  {
    Vector3 gunPosition = PercySpine.GetPointAttachmentToUnityPosition(mecanim, slotNameBarrelEnd, attachmentNameBarrelEnd);
    // if (PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().itemData.bulletCount >= 1)
    // {
    GameObject go = Instantiate(bullet, new Vector3(gunPosition.x,
    gunPosition.y, gunPosition.z),
    Quaternion.Euler(0f, 0f, PercySpine.GetPointAttachmentToUnityRotation(mecanim, slotNameBarrelEnd, attachmentNameBarrelEnd)));

    Gun gun = PercyScriptableObjectUtility.DeepCopy(PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().gunData);
    go.GetComponent<Bullet>().gun = gun;
    go.GetComponent<Bullet>().gun.damage = PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().gunData.damage;

    PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().itemData.bulletCount -= 1;
    GameManager.Instance.UpdatePlayingUI();
    // }

  }
}
