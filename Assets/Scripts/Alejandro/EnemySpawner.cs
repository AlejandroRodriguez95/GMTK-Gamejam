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

    [SerializeField] SpriteRenderer gameOverimage;

    int iterations;

    float speedFactor;
    float timeFactor;


    private void Start()
    {
        timeFactor = 5;
        speedFactor = 0.2f;
        StartCoroutine(SpawnEnemyAfterWaiting(timeFactor * Random.Range(1, 1.4f), Waypoints.LeftWaypoints, leftSpawnPos.position, leftEnemyPrefab, false, leftArm, speedFactor * Random.Range(1, 1.4f), gameOverimage));
        StartCoroutine(SpawnEnemyAfterWaiting(timeFactor * Random.Range(1, 1.4f), Waypoints.RightWaypoints, rightSpawnPos.position, rightEnemyPrefab, true, rightArm, speedFactor * Random.Range(1, 1.4f), gameOverimage));
    }


    IEnumerator SpawnEnemyAfterWaiting(float seconds, List<Transform> waypoints, Vector3 pos, GameObject enemy, bool side, Transform arm, float moveSpeed, SpriteRenderer gameoverimage)
    {
        yield return new WaitForSeconds(seconds);

        var tempEnemy = Instantiate(enemy, pos, Quaternion.identity).GetComponent<EnemyBase>();

        tempEnemy.GameOverImage = gameoverimage;
        tempEnemy.InternalWaypoints = waypoints;
        tempEnemy.Side = side;
        tempEnemy.ArmTransform = arm;
        tempEnemy.MoveSpeed = moveSpeed;
        speedFactor *= Mathf.Sqrt(1.5f);
        timeFactor *= Mathf.Sqrt(0.9f);
        StartCoroutine(SpawnEnemyAfterWaiting(timeFactor * Random.Range(1, 1.4f), waypoints, pos, enemy, side, arm, speedFactor * Random.Range(1, 1.4f), gameoverimage));
    }

}
