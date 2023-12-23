using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public Gun gun;
  public List<GameObject> zombiesAlreadyHit;

  private void FixedUpdate()
  {
    transform.position -= transform.right * Time.deltaTime * 20;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (PercyGameObjectUtility.CheckRootParentTagOfChild(other.gameObject.transform, "Zombie")
    && !zombiesAlreadyHit.Contains(PercyGameObjectUtility.GetTransformRootParentOfChild(other.gameObject.transform)))
    {
      zombiesAlreadyHit.Add(PercyGameObjectUtility.GetTransformRootParentOfChild(other.gameObject.transform));

      gun.penetrationPower -= PercyGameObjectUtility.GetTransformRootParentOfChild(other.transform).GetComponent<Zombie>().penetrationDefense;
      gun.damage *= .8f;

      if (gun.penetrationPower <= 0)
      {
        Destroy(gameObject);
      }

    }
  }
}
