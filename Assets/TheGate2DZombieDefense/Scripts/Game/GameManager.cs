using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;

  public delegate void StateChange(GameState state);
  public event StateChange OnStateChange;

  public GameState state;

  public void State(GameState state)
  {
    switch (state)
    {
      case GameState.StartMenu:
        break;
      case GameState.Playing:
        break;
      case GameState.LevelEndWon:
        break;
      case GameState.LevelEndLost:
        break;
      case GameState.Shop:
        break;
      case GameState.Paused:
        break;
    }
    OnStateChange(state);
  }

  private void Start()
  {
    State(GameState.StartMenu);
  }

}

public enum GameState
{
  StartMenu,
  Playing,
  LevelEndWon,
  LevelEndLost,
  Shop,
  Paused
}