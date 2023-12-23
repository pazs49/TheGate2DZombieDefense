using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PercyScriptableObjectUtility
{
  public static T DeepCopy<T>(T source) where T : ScriptableObject
  {
    string json = JsonUtility.ToJson(source);
    T copy = ScriptableObject.CreateInstance<T>();
    JsonUtility.FromJsonOverwrite(json, copy);
    return copy;
  }
}
