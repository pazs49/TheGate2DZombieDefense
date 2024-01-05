using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
  public static GameOverManager Instance;

  private void Awake()
  {
    Instance = this;
  }

  public void GameOver()
  {
    Zombie.ResetZombies();
    GameManager.Instance.UpdateState(GameState.LevelEndLost);
  }
}
