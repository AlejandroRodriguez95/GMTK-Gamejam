using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject leftEnemyPrefab;
    [SerializeField] Transform leftSpawnPos;

    [SerializeField] Transform rightArm;
    [SerializeField] Transform leftArm;

    [SerializeField] GameObject rightEnemyPrefab;
    [SerializeField] Transform rightSpawnPos;

    private void Start()
    {
        StartCoroutine(SpawnLeftEnemyAfterWaiting(2, Waypoints.LeftWaypoints, leftSpawnPos.position, leftEnemyPrefab, false, leftArm));
        StartCoroutine(SpawnLeftEnemyAfterWaiting(2, Waypoints.RightWaypoints, rightSpawnPos.position, rightEnemyPrefab, true, rightArm));
    }


    IEnumerator SpawnLeftEnemyAfterWaiting(float seconds, List<Transform> waypoints, Vector3 pos, GameObject enemy, bool side, Transform arm)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);

            var tempEnemy = Instantiate(enemy, pos, Quaternion.identity).GetComponent<EnemyBase>();

            tempEnemy.InternalWaypoints = waypoints;
            tempEnemy.Side = side;
            tempEnemy.ArmTransform = arm;
        }
    }
}
