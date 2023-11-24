using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] waves;
    public bool spawnWaves;
    public float waitTime = 2f;
    public float clampMin = 2f;
    public float clampMax = 20f;
    public int spawnedCount = 0;
    public static int enemiesToKill = 0;
    public static List<GameObject> enemiesLeft = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if(spawnWaves)
        {
            Instantiate(waves[spawnedCount]);
            spawnedCount++;
            //StartCoroutine(SpawnWaveEveryXSeconds());
        }else
        {
            StartCoroutine(SpawnEnemyEveryXSeconds());
        }
        
    }

    IEnumerator SpawnWaveEveryXSeconds()
    {
        if(spawnedCount < waves.Length)
        {
            Instantiate(waves[spawnedCount]);
            spawnedCount++;
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(SpawnWaveEveryXSeconds());
        }
        yield return null;
    }

    public IEnumerator SpawnWaveAfterClear()
    {
        print("ifnotcleared " + enemiesToKill+"/"+enemiesLeft.Count);
        if(spawnedCount < waves.Length && enemiesToKill == 0)
        {
            print("ifcleared " + enemiesToKill);
            enemiesToKill = 0;
            yield return new WaitForSeconds(waitTime);
            print("instantiated");
            Instantiate(waves[spawnedCount]);
            spawnedCount++;
        }
        yield return null;
    }

    IEnumerator SpawnEnemyEveryXSeconds()
    {
        waitTime = Mathf.Clamp(waitTime, clampMin, clampMax);
        yield return new WaitForSeconds(waitTime);
        Instantiate(enemy, new Vector3(transform.position.x, transform.position.y - Random.Range(-.5f, .4f), transform.position.z), Quaternion.identity);
        spawnedCount++;

        if(waitTime <= 8 && (spawnedCount == 15 || spawnedCount == 23 || spawnedCount == 30) )
        {
            waitTime -= 1f;
        }

        if(waitTime > 8 && (spawnedCount == 15 || spawnedCount == 23 || spawnedCount == 27) )
        {
            waitTime -= 3f;
        }
        StartCoroutine(SpawnEnemyEveryXSeconds());
        yield return null;
    }
}
