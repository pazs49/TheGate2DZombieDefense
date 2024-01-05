using Spine.Unity;
using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
  public static PlayerEquip Instance;

  [SerializeField] SkeletonMecanim skeletonMecanim;
  [SerializeField] Animator anim;

  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    DefaultGun();
  }

  public void DefaultGun()
  {
    Pistol("guns/pistol1");
  }

  public void Pistol(string name)
  {
    skeletonMecanim.Skeleton.SetSkin(name);
    anim.SetInteger("gunType", 0);
  }

  public void Rifle(string name)
  {
    skeletonMecanim.Skeleton.SetSkin(name);
    anim.SetInteger("gunType", 1);
  }
}
