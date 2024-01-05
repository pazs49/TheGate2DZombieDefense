using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
  public GameObject barricades;

  bool isBaseHasHealth;

  private void Start()
  {
    GameManager.Instance.OnStateChange += ResetBarricadeToActive;
  }

  private void OnDestroy()
  {
    GameManager.Instance.OnStateChange -= ResetBarricadeToActive;
  }

  private void Update()
  {
    if (GameManager.Instance.GetCurrentState() == GameState.Playing)
    {
      if (PlayerLoadout.Instance.gateAndBaseData != null &&
            PlayerLoadout.Instance.gateAndBaseData.gateHealth <= 0)
      {
        SetBarricadeActive(false);
      }

      if (PlayerLoadout.Instance.gateAndBaseData != null &&
        PlayerLoadout.Instance.gateAndBaseData.baseHealth <= 0)
      {
        if (!isBaseHasHealth)
        {
          GameOverManager.Instance.GameOver();
          isBaseHasHealth = true;
        }

      }
    }
  }

  void SetBarricadeActive(bool isActive)
  {
    barricades.SetActive(isActive);
  }

  void ResetBarricadeToActive(GameState state)
  {
    if (state == GameState.Preparation)
    {
      SetBarricadeActive(true);
      isBaseHasHealth = false;
    }
  }
}
