using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Zombie : MonoBehaviour
{
  public ZombieScriptableObject zombieData;
  public ZombieState state;
  public float health;
  public float attackPower;
  public float movementSpeed;
  public float attackSpeed;
  public int penetrationDefense;

  public Collider2D[] allCols;

  [HideInInspector] public Rigidbody2D rb2d;
  [HideInInspector] public Animator anim;

  private void Awake()
  {
    Init(zombieData);
  }
  private void Update()
  {
    AdjustZPosition();
  }
  //---------------------------------
  public void TakeDamage(float damage, ZombieBodyPart bodyPart)
  {
    switch (bodyPart)
    {
      case ZombieBodyPart.Head:
        damage *= 1.5f;
        health -= damage;
        break;
      case ZombieBodyPart.Body:
        damage *= 1f;
        health -= damage;
        break;
      case ZombieBodyPart.Arms:
        damage *= .1f;
        health -= damage;
        break;
      case ZombieBodyPart.Legs:
        damage *= .7f;
        health -= damage;
        break;

    }
    Debug.Log(gameObject.name + " took " + damage + " damage!");
    if (health <= 0)
    {
      state = ZombieState.Death;
    }
  }
  public void Init(ZombieScriptableObject data)
  {
    health = data.health;
    attackPower = data.attackPower;
    attackSpeed = data.attackSpeed;
    movementSpeed = data.movementSpeed;
    penetrationDefense = data.penetrationDefense;

    rb2d = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();

    allCols = GetComponentsInChildren<Collider2D>();
  }

  void AdjustZPosition()
  {
    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * .1f);
  }

  protected void DisableColliders()
  {
    var disabledColls = allCols.Select(col =>
    {
      col.enabled = false;
      return col;
    }).ToArray();
    allCols = disabledColls;
  }
}

public enum ZombieState
{
  Moving,
  Attacking,
  Death
}

public enum ZombieBodyPart
{
  Head,
  Body,
  Arms,
  Legs
}
