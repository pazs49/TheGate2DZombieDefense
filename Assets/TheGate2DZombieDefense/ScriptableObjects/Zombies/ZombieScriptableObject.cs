using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewZombieData", menuName = "Custom/ZombieData")]
public class ZombieScriptableObject : ScriptableObject
{
  public float health;
  public float attackPower;
  public float movementSpeed;
  public float attackSpeed;
  public int penetrationDefense;
}
