using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class ZombieBulletCollider : MonoBehaviour
{
  public ZombieBodyPart bodyPart;
  Zombie zombie;

  private void Awake()
  {
    zombie = PercyGameObjectUtility.GetTransformRootParentOfChild(transform).GetComponent<Zombie>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Bullet"))
    {
      zombie.TakeDamage(other.GetComponent<Bullet>().gun.damage, bodyPart);
    }
  }
}
