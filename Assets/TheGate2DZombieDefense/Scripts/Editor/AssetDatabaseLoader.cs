using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetDatabaseLoader : EditorWindow
{
  [MenuItem("Window/Reload Assets")]
  public static void ShowWindow()
  {
    // Show existing window instance. If one doesn't exist, make one.
    EditorWindow.GetWindow(typeof(AssetDatabaseLoader), false, "AssetDatabaseLoader");
  }

  private void OnGUI()
  {
    // Add your UI elements here if needed
    GUILayout.Label("Reloader", EditorStyles.boldLabel);

    if (GUILayout.Button("Reload"))
    {
      Reload();
    }
  }

  // Comment here where you can execute your function
  private void Reload()
  {
    //For DayManager
    LoadDays();
  }

  void LoadDays()
  {
    string[] guids = AssetDatabase.FindAssets("l:Day", null);
    string[] paths = guids.Select(x => AssetDatabase.GUIDToAssetPath(x)).ToArray();
    DayManager.Instance = GameObject.FindWithTag("DayManager").GetComponent<DayManager>();

    DayManager.Instance.days.Clear();

    foreach (string str in paths)
    {
      DayManager.Instance.days.Add(AssetDatabase.LoadAssetAtPath<Day>(str));
    }
  }
}
