using UnityEngine;

public class ItemSlots : MonoBehaviour
{
  public static ItemSlots Instance;

  public GameObject[] gunSlots;
  public GameObject[] skillSlots;

  private void Awake()
  {
    Instance = this;
  }
}
