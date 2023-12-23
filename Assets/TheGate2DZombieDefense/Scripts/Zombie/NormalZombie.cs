using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spine;
using Spine.Unity;
using UnityEngine;

public class NormalZombie : Zombie, IZombie
{
  public bool canMove;

  public SkeletonMecanim mecanim;

  private void Start()
  {
    state = ZombieState.Moving;
  }
  private void FixedUpdate()
  {
    State(state);
  }

  //General collider
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Gate"))
    {
      state = ZombieState.Attacking;
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

  public void InflictDamage()
  {

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
        Attack(attackPower);
        break;
      case ZombieState.Death:
        Death();
        break;
    }
  }

  public void Attack(float damage)
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
