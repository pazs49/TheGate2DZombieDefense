using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDayData", menuName = "Custom/DayData")]
public class Day : ScriptableObject
{
  public GameObject[] zombies;
  public int zombiesCount;
  public float spawnSpeed;
}
