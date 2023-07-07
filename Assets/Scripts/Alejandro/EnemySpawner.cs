using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    private void Start()
    {
        StartCoroutine(SpawnEnemyAfterWaiting(2));
    }


    IEnumerator SpawnEnemyAfterWaiting(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);

            var enemy = Instantiate(enemyPrefab);
            enemy.GetComponent<EnemyBase>().Waypoints = Waypoints.leftWaypoints;
        }
    }
}
