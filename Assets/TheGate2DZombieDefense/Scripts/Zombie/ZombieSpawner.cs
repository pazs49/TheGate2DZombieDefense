using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ZombieSpawner : MonoBehaviour
{
  public static ZombieSpawner Instance;

  public GameObject spawnBoundUpper, spawnBoundLower;
  public Animator sunAnim;

  Day currentDay;

  //in seconds
  float spawnTimer;
  int zombiesCount;
  int initialZombiesCount;

  bool isActive;
  bool canCallCheckZombiesAlive = true;

  public bool isNight;
  public delegate void NightBuffHandler();
  public event NightBuffHandler OnNightBuff;

  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    GameManager.Instance.OnStateChange += Init;
  }

  private void Update()
  {
    if (isActive)
    {
      Spawn();
    }
    if (GameManager.Instance.GetCurrentState() == GameState.Playing
    && canCallCheckZombiesAlive)
    {
      StartCoroutine(CheckZombiesAlive());
    }
  }

  private void OnDestroy()
  {
    GameManager.Instance.OnStateChange -= Init;
  }

  private void Init(GameState state)
  {
    if (state == GameState.Playing)
    {
      currentDay = DayManager.Instance.GetCurrentDay();
      zombiesCount = currentDay.zombiesCount;
      initialZombiesCount = currentDay.zombiesCount;
      isActive = true;
    }
  }

  private void Spawn()
  {
    spawnTimer += Time.deltaTime;
    if (spawnTimer >= currentDay.spawnSpeed)
    {
      Instantiate(currentDay.zombies[Random.Range(0, currentDay.zombies.Length)],
      new Vector3(spawnBoundUpper.transform.position.x, Random.Range(
        spawnBoundUpper.transform.position.y, spawnBoundLower.transform.position.y
      ), 0), Quaternion.identity);

      zombiesCount -= 1;

      spawnTimer = 0;
    }

    //Day and night
    if (zombiesCount <= initialZombiesCount / 2)
    {
      if (!isNight)
      {
        isNight = true;
        sunAnim.SetBool("isNight", true);
        OnNightBuff?.Invoke();
        Debug.Log("Night!");
      }
    }

    if (zombiesCount <= 0)
    {
      Debug.Log("Done!");
      isActive = false;
    }
  }


  //try true false for fun
  private IEnumerator CheckZombiesAlive()
  {
    canCallCheckZombiesAlive = false;

    yield return new WaitForSeconds(3f);

    GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
    if (zombies.Length > 0 && zombiesCount > 0)
    {
      Debug.Log("Still alive");
    }
    else if (zombies.Length <= 0 && zombiesCount <= 0
    && PlayerLoadout.Instance.gateAndBaseData.baseHealth > 0)
    {
      Debug.Log("All zombies dead!");
      GameManager.Instance.UpdateState(GameState.LevelEndWon);
    }

    canCallCheckZombiesAlive = true;
  }
}
