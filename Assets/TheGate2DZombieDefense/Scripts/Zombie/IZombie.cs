using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombie
{
  public void Move(float movementSpeed);
  public void Attack(float damage);
  public void Special();
  public void Death();
  // public void TakeDamage(float damage);
  public void State(ZombieState state);
}
