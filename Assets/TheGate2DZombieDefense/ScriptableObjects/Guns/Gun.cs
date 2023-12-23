using UnityEngine;

[CreateAssetMenu(fileName = "NewGunData", menuName = "Custom/GunData")]
public class Gun : ScriptableObject
{
  public string gunName;
  public GunType type;
  public float damage;
  //1:1 zombie
  public int penetrationPower;
  /// <summary>
  /// -1 means infinite ammo. Usually only for default gun AKA Pistol1
  /// </summary>
  public int magazineCapacity;
  public float reloadSpeed;
  public float fireRate;
}

public enum GunType
{
  Pistol,
  Rifle,
  Shotgun,
  Sniper
}
