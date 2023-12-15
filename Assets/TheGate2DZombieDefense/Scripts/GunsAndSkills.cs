using UnityEngine;

public class GunsAndSkills : MonoBehaviour
{
  public static GunsAndSkills Instance;

  public Gun[] guns;
  public Skill[] skills;

  private void Awake()
  {
    Instance = this;
  }
}