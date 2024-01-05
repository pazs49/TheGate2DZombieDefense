using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class PercyGeneral
{
  public static IEnumerator DelayFunctionByXFrame(MonoBehaviour target, string funcName, int numOfFrames)
  {
    for (int i = 0; i < numOfFrames; i++)
    {
      yield return null;
    }

    MethodInfo methodInfo = target.GetType().GetMethod(funcName);

    if (methodInfo != null)
    {
      methodInfo.Invoke(target, null);
    }
    else
    {
      Debug.LogError("Method " + funcName + " not found on target object.");
    }
  }
}
