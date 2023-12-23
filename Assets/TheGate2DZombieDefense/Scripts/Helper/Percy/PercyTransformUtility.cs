using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PercyGameObjectUtility
{
  public static GameObject GetTransformRootParentOfChild(Transform child)
  {
    while (child.parent != null)
    {
      child = child.parent;
    }
    return child.gameObject;
  }

  public static bool CheckRootParentTagOfChild(Transform child, string tag)
  {
    while (child.parent != null)
    {
      child = child.parent;
    }
    return child.CompareTag(tag);
  }
}
