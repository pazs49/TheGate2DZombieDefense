using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spine;
using Spine.Unity;
using UnityEngine;

public class NormalZombie : Zombie, IZombie
{
  public bool canMove;

  protected override void Awake()
  {
    base.Awake();
  }

  protected override void Start()
  {
    base.Start();
    state = ZombieState.Moving;
  }
  private void FixedUpdate()
  {
    State(state);
  }

  protected override void OnTriggerEnter2D(Collider2D other)
  {
    base.OnTriggerEnter2D(other);
    if (other.gameObject.CompareTag("Gate"))
    {
      state = ZombieState.Attacking;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Gate"))
    {
      state = ZombieState.Moving;
    }
  }

  //Spine Events
  public void WalkStart()
  {
    canMove = true;
  }

  public void WalkEnd()
  {
    canMove = false;
  }

  public override void InflictDamage()
  {
    base.InflictDamage();
    GameManager.Instance.UpdatePlayingUI();
  }

  public override void DeathEnd()
  {
    base.DeathEnd();
  }
  //-------------------------------

  public void State(ZombieState state)
  {
    switch (state)
    {
      case ZombieState.Moving:
        Move(movementSpeed);
        break;
      case ZombieState.Attacking:
        Attack();
        break;
      case ZombieState.Death:
        Death();
        break;
    }
  }

  public void Attack()
  {
    anim.SetBool("isAttacking", true);
    anim.SetBool("isWalking", false);
  }

  public void Move(float movementSpeed)
  {
    anim.SetBool("isAttacking", false);
    anim.SetBool("isWalking", true);
    if (canMove)
    {
      rb2d.velocity = new Vector2(movementSpeed, rb2d.velocity.y);
    }
    else
    {
      rb2d.velocity = new Vector2(0, rb2d.velocity.y);
    }
  }

  public void Special()
  {

  }

  public void Death()
  {
    if (!anim.GetBool("isDead"))
    {
      anim.SetBool("isDead", true);
      DisableColliders();
      Debug.Log("Dead!");
    }
  }
}
