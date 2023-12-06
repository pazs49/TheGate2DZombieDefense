using UnityEngine;

public class Menu : MonoBehaviour
{
  public Canvas menuCanvas;
  public Canvas titleCanvas;
  public Canvas menuButtonsCanvas;
  public Canvas preparationCanvas;
  public Canvas playerControllerCanvas;

  Canvas[] allCanvas;

  private void Awake()
  {

  }
  private void Start()
  {
    LoadAllCanvas();
    GameManager.Instance.OnStateChange += HandleMenuState;
  }
  private void OnDestroy()
  {
    GameManager.Instance.OnStateChange -= HandleMenuState;
  }
  //-----------------------------------------------
  public void UpdateState(GameState state)
  {
    GameManager.Instance.UpdateState(state);
  }

  public void HandleMenuState(GameState state)
  {
    if (state == GameState.StartMenu)
    {
      menuCanvas.enabled = true;
      EnableCanvas(allCanvas, titleCanvas);
    }
    else if (state == GameState.Menu)
    {
      EnableCanvas(allCanvas, menuButtonsCanvas);
    }
    else if (state == GameState.Preparation)
    {
      EnableCanvas(allCanvas, preparationCanvas);
    }
    else if (state == GameState.Playing)
    {
      EnableCanvas(allCanvas, playerControllerCanvas);
    }
  }

  public void TitleButton()
  {
    UpdateState(GameState.Menu);
  }
  public void PlayMainMenuButton()
  {
    UpdateState(GameState.Preparation);
  }
  public void Play()
  {
    UpdateState(GameState.Playing);
  }

  //Excludes menuCanvas
  void LoadAllCanvas()
  {
    allCanvas = new Canvas[4];

    allCanvas[0] = titleCanvas;
    allCanvas[1] = menuButtonsCanvas;
    allCanvas[2] = preparationCanvas;
    allCanvas[3] = playerControllerCanvas;
  }

  /// <summary>
  /// If no 2nd param, all canvas will get disabled
  /// </summary>
  /// <param name="allCanvas"></param>
  /// <param name="canvasesToEnable"></param>
  void EnableCanvas(Canvas[] allCanvas, params Canvas[] canvasesToEnable)
  {
    if (canvasesToEnable.Length != 0)
    {
      foreach (Canvas canvas in canvasesToEnable)
      {
        foreach (Canvas canvas2 in allCanvas)
        {
          if (canvas.name == canvas2.name)
          {
            canvas2.enabled = true;
          }
          else
          {
            canvas2.enabled = false;
          }
        }
      }
    }
    else if (canvasesToEnable.Length == 0)
    {
      foreach (Canvas canvas in allCanvas)
      {
        {
          canvas.enabled = false;
        }
      }
    }
  }
}
