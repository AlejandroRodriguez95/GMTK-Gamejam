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

    float speedSeed;
    float timeFactor;


    private void Start()
    {
        timeFactor = 5;
        speedSeed = 0.2f;
        StartCoroutine(SpawnEnemyAfterWaiting(timeFactor * Random.Range(1, 1.4f), Waypoints.LeftWaypoints, leftSpawnPos.position, leftEnemyPrefab, false, leftArm, speedSeed * Random.Range(1, 1.4f), gameOverimage));
        StartCoroutine(SpawnEnemyAfterWaiting(timeFactor * Random.Range(1, 1.4f), Waypoints.RightWaypoints, rightSpawnPos.position, rightEnemyPrefab, true, rightArm, speedSeed * Random.Range(1, 1.4f), gameOverimage));
    }


    IEnumerator SpawnEnemyAfterWaiting(float seconds, List<Transform> waypoints, Vector3 pos, GameObject enemy, bool side, Transform arm, float speedSeed, SpriteRenderer gameoverimage)
    {
        yield return new WaitForSeconds(seconds);

        var tempEnemy = Instantiate(enemy, pos, Quaternion.identity).GetComponent<EnemyBase>();

        tempEnemy.GameOverImage = gameoverimage;
        tempEnemy.InternalWaypoints = waypoints;
        tempEnemy.Side = side;
        tempEnemy.ArmTransform = arm;
        tempEnemy.MoveSpeed = Mathf.Pow(speedSeed,2);
        timeFactor *= Mathf.Sqrt(0.99f);
        StartCoroutine(SpawnEnemyAfterWaiting(timeFactor, waypoints, pos, enemy, side, arm, speedSeed * Random.Range(0.4f,10f), gameoverimage));
    }

}
