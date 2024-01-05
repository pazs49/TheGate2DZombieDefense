using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombie
{
  public void Move(float movementSpeed);
  public void Attack();
  public void Special();
  public void Death();

  public void InflictDamage();
  public void State(ZombieState state);
}
