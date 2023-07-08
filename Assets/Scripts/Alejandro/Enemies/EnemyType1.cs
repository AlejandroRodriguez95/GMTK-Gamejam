using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : EnemyBase
{
    Vector3 direction;
    public int currentWaypointIndex;
    Transform lastWaypoint;

    private void Start()
    {
        currentWaypointIndex = 0;

        if(waypoints.Count > 0)
        {
            currentWaypoint = waypoints[currentWaypointIndex];
            direction = currentWaypoint.position - transform.position;
            direction.Normalize();
            currentWaypointIndex++;
        }

    }

    private void Update()
    {
        if(currentWaypointIndex <= waypoints.Count)
            MoveToWayPoint();
    }


    private void MoveToWayPoint()
    {
        Vector3 movement = direction * moveSpeed * Time.deltaTime;

        transform.Translate(movement);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentWaypointIndex >= waypoints.Count)
        {
            currentWaypointIndex++;
            Debug.LogWarning("Game over!");

            return;
        }

        currentWaypoint = waypoints[currentWaypointIndex];
        currentWaypointIndex++;
        transform.LookAt(currentWaypoint);


        direction = currentWaypoint.position - transform.position;
        direction.Normalize();
    }
}
