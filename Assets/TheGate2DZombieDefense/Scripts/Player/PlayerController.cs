using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] FixedJoystick joystickVertical;
  [SerializeField] FixedJoystick joystickRotation;
  [SerializeField] GameObject aimIK;

  private void FixedUpdate()
  {
    CharacterAim();
  }

  void CharacterAim()
  {
    float aimDirection = aimIK.transform.position.y + (joystickVertical.Vertical * .05f);
    aimIK.transform.position = new Vector3(aimIK.transform.position.x, aimDirection, aimIK.transform.position.z);

    float rotationAmount = joystickRotation.Vertical * Time.deltaTime * 20f;
    aimIK.transform.Rotate(Vector3.forward, rotationAmount);
  }
}
