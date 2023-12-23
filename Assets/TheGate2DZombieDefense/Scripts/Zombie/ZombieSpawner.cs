using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
  public GameObject spawnBoundUpper, spawnBoundLower;

  Day currentDay;

  //in seconds
  float spawnTimer;
  int zombiesCount;

  bool isActive;

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
    if (zombiesCount <= 0)
    {
      Debug.Log("Done!");
      isActive = false;
    }
  }
}
