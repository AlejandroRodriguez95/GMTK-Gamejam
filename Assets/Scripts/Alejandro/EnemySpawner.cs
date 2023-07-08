using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject leftEnemyPrefab;
    [SerializeField] Transform leftSpawnPos;

    [SerializeField] GameObject rightEnemyPrefab;
    [SerializeField] Transform rightSpawnPos;

    private void Start()
    {
        StartCoroutine(SpawnLeftEnemyAfterWaiting(2, Waypoints.leftWaypoints, leftSpawnPos.position, leftEnemyPrefab));
        StartCoroutine(SpawnLeftEnemyAfterWaiting(2, Waypoints.rightWaypoints, rightSpawnPos.position, rightEnemyPrefab));
    }


    IEnumerator SpawnLeftEnemyAfterWaiting(float seconds, List<Transform> waypoints, Vector3 pos, GameObject enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);

            var tempEnemy = Instantiate(enemy, pos, Quaternion.identity);
            tempEnemy.GetComponent<EnemyBase>().Waypoints = waypoints;
        }
    }
}
