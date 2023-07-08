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
        StartCoroutine(SpawnLeftEnemyAfterWaiting(2, Waypoints.LeftWaypoints, leftSpawnPos.position, leftEnemyPrefab, false));
        StartCoroutine(SpawnLeftEnemyAfterWaiting(2, Waypoints.RightWaypoints, rightSpawnPos.position, rightEnemyPrefab, true));
    }


    IEnumerator SpawnLeftEnemyAfterWaiting(float seconds, List<Transform> waypoints, Vector3 pos, GameObject enemy, bool side)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);

            var tempEnemy = Instantiate(enemy, pos, Quaternion.identity);
            tempEnemy.GetComponent<EnemyBase>().InternalWaypoints = waypoints;
            tempEnemy.GetComponent<EnemyBase>().Side = side;
        }
    }
}
