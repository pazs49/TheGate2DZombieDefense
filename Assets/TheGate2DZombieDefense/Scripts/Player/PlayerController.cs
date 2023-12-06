using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] FixedJoystick joystick;
  [SerializeField] GameObject aimIK;

  private void FixedUpdate()
  {
    CharacterAim();
  }

  void CharacterAim()
  {
    float aimDirection = aimIK.transform.position.y + (joystick.Vertical * .05f);
    aimIK.transform.position = new Vector3(aimIK.transform.position.x, aimDirection, aimIK.transform.position.z);
  }
}
