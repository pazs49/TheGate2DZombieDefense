using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;

  public delegate void StateChange(GameState state);
  public event StateChange OnStateChange;

  [SerializeField] GameState currentState;

  [Space(20)]
  public TextMeshProUGUI currentGoAmmoText;

  private void Awake()
  {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    Debug.unityLogger.logEnabled = true;
#else
    Debug.logger.logEnabled = false;
#endif

    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void Start()
  {
    UpdateState(GameState.StartMenu);
  }
  //-----------------------------------------------
  void State(GameState state)
  {
    currentState = state;
    switch (state)
    {
      case GameState.StartMenu:
        break;
      case GameState.Menu:
        break;
      case GameState.Preparation:
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
    OnStateChange?.Invoke(state);
  }

  public void UpdateState(GameState state)
  {
    State(state);
  }

  public GameState GetCurrentState()
  {
    return currentState;
  }
  //----------------------------------------------------------
  public void UpdatePlayingUI()
  {
    currentGoAmmoText.text = "Ammo: " + PlayerLoadout.Instance.GetCurrentEquippedGunAmmo();
  }
}

public enum GameState
{
  StartMenu,
  Menu,
  Preparation,
  Playing,
  LevelEndWon,
  LevelEndLost,
  Shop,
  Paused
}
