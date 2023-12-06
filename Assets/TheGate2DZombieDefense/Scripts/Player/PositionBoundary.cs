using UnityEngine;

public class PositionBoundary : MonoBehaviour
{
  public float minX = -5f; // Minimum X position
  public float maxX = 5f;  // Maximum X position
  public float minY = -5f; // Minimum Y position
  public float maxY = 5f;  // Maximum Y position

  void Update()
  {
    // Get the current local position
    Vector3 currentPosition = transform.localPosition;

    // Clamp the X and Y values to the specified boundaries
    float clampedX = Mathf.Clamp(currentPosition.x, minX, maxX);
    float clampedY = Mathf.Clamp(currentPosition.y, minY, maxY);

    // Update the local position with the clamped values
    transform.localPosition = new Vector3(clampedX, clampedY, currentPosition.z);
  }
}