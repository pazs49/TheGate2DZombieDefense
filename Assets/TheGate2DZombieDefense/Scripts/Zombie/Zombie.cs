using System;
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


  //Damage for player's base
  protected virtual void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Base"))
    {
      PlayerLoadout.Instance.gateAndBaseData.baseHealth -= zombieData.baseDamage;
      TakeDamage(99999, ZombieBodyPart.Head, false);
      GameManager.Instance.UpdatePlayingUI();
    }
  }

  protected virtual void Awake()
  {
    Init(zombieData);
  }

  protected virtual void Start()
  {
    ZombieSpawner.Instance.OnNightBuff += NightBuff;
  }

  private void Update()
  {
    AdjustZPosition();
  }

  private void OnDestroy()
  {
    ZombieSpawner.Instance.OnNightBuff -= NightBuff;
  }

  //---------------------------------

  public virtual void DeathEnd()
  {
    Destroy(gameObject);
  }

  //---------------------------------
  public static void ResetZombies()
  {
    List<Zombie> zombies = GameObject.FindGameObjectsWithTag("Zombie")
    .Select(x => x.GetComponent<Zombie>()).ToList();

    zombies.ForEach(x => x.TakeDamage(99999, ZombieBodyPart.Head));
  }
  //---------------------------------
  public void TakeDamage(float damage, ZombieBodyPart bodyPart, bool isRewarded = true)
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
    if (isRewarded && health <= 0)
    {
      state = ZombieState.Death;
    }
    else if (!isRewarded && health <= 0)
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

  public virtual void InflictDamage()
  {
    PlayerLoadout.Instance.gateAndBaseData.gateHealth -= attackPower;
  }

  public void NightBuff()
  {
    movementSpeed += movementSpeed * .5f;
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
