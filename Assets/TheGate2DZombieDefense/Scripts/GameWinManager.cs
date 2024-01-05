using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinManager : MonoBehaviour
{
  public static GameWinManager Instance;

  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    GameManager.Instance.OnStateChange += Win;
  }

  private void OnDestroy()
  {
    GameManager.Instance.OnStateChange -= Win;
  }

  public void Win(GameState state)
  {
    if (state == GameState.LevelEndWon)
    {
      StartCoroutine(PercyGeneral.DelayFunctionByXFrame(this, "WinWrapper", 1));
    }

  }

  public void WinWrapper()
  {
    CurrentPlayerData.Instance.data.dayLevel++;
    SaveLoad.Instance.Save();
  }

  public void NextLevel()
  {
    GameManager.Instance.UpdateState(GameState.Preparation);
  }
}
