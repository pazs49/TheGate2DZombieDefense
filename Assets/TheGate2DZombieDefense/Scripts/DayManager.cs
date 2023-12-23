using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayManager : MonoBehaviour
{
  public static DayManager Instance;

  public List<Day> days;

  private void Awake()
  {
    Instance = this;
  }

  public Day GetCurrentDay()
  {
    Day day = days.First(x => x.name.StartsWith("Day" + CurrentPlayerData.Instance.data.dayLevel));
    return day;
  }
}
