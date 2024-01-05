using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;

  public delegate void StateChange(GameState state);
  public event StateChange OnStateChange;

  public GameState currentState;
  List<GameState> recordedStates;

  [Space(20)]
  public TextMeshProUGUI currentGoAmmoText;
  public TextMeshProUGUI currentGateHealth;
  public TextMeshProUGUI currentBaseHealth;

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

    recordedStates = new List<GameState>();
  }

  private void Start()
  {
    UpdateState(GameState.StartMenu);
  }
  //-----------------------------------------------
  void State(GameState state)
  {
    recordedStates.Add(state);
    GetPreviousState();

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
        StartCoroutine(PercyGeneral.DelayFunctionByXFrame(this, "UpdatePlayingUI", 1));
        break;
      case GameState.LevelEndWon:
        ResetVar();
        break;
      case GameState.LevelEndLost:
        ResetVar();
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

  public GameState GetPreviousState()
  {
    if (recordedStates.Count > 1)
    {
      Debug.Log("Previous state is " + recordedStates[recordedStates.Count - 2]);
      return recordedStates[recordedStates.Count - 2];
    }
    GameState firstState = GameState.StartMenu;
    return firstState;
  }
  //----------------------------------------------------------
  public void UpdatePlayingUI()
  {
    if (currentState == GameState.Playing && PlayerLoadout.Instance.currentItemEquipped != null)
    {
      currentGoAmmoText.text = "Ammo: " + PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().currentMagazineBullet
      + "/" + PlayerLoadout.Instance.GetCurrentEquippedGun().gunData.magazineCapacity
      + "(" + (PlayerLoadout.Instance.GetCurrentEquippedGunAmmo() - PlayerLoadout.Instance.currentItemEquipped.GetComponent<ItemPreparation>().currentMagazineBullet) + ")";
      currentGateHealth.text = "Gate: " + PlayerLoadout.Instance.gateAndBaseData.gateHealth;
      currentBaseHealth.text = "Base: " + PlayerLoadout.Instance.gateAndBaseData.baseHealth;
    }
  }

  public void ResetVar()
  {
    List<GameObject> items = GameObject.FindGameObjectsWithTag("Item").ToList();
    items.ForEach(x => Destroy(x));
    PlayerLoadout.Instance.guns.Clear();
    PlayerLoadout.Instance.skills.Clear();

    ZombieSpawner.Instance.isNight = false;
    ZombieSpawner.Instance.sunAnim.SetBool("isNight", false);
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
