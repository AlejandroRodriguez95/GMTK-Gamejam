using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] waves;
    public bool spawnWaves;
    public float waitTime = 1f;
    public float clampMin = 2f;
    public float clampMax = 20f;
    public int spawnedCount = 0;
    public static int enemiesToKill = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(spawnWaves)
        {
            SpawnWaveAfterClear();
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

    public void SpawnWaveAfterClear()
    {
        print("ifnotcleared " + enemiesToKill);
        if(spawnedCount < waves.Length && enemiesToKill <= 1)
        {
            print("ifcleared " + enemiesToKill);
            enemiesToKill = 0;
            Instantiate(waves[spawnedCount]);
            spawnedCount++;
        }
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

        if(waitTime <= 8 && (spawnedCount == 15 || spawnedCount == 23 || spawnedCount == 27) )
        {
            waitTime -= 3f;
        }
        StartCoroutine(SpawnEnemyEveryXSeconds());
        yield return null;
    }
}
