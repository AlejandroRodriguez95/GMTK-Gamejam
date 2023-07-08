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

        if(internalWaypoints.Count > 0)
        {
            currentWaypoint = internalWaypoints[currentWaypointIndex];
            direction = currentWaypoint.position - transform.position;
            direction.Normalize();
            currentWaypointIndex++;
        }

    }

    private void Update()
    {
        if(currentWaypointIndex <= internalWaypoints.Count)
            MoveToWayPoint();
    }


    private void MoveToWayPoint()
    {
        Vector3 movement = direction * moveSpeed * Time.deltaTime;

        transform.Translate(movement);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentWaypointIndex >= internalWaypoints.Count)
        {
            currentWaypointIndex++;

            if (!alreadyLoadedSecondSegment)
                LoadNextWaypointsSegment();
            else
                Debug.Log("Game over!");

            return;
        }

        currentWaypoint = internalWaypoints[currentWaypointIndex];
        currentWaypointIndex++;
        

        direction = currentWaypoint.position - transform.position;
        direction.Normalize();
    }

    private void LoadNextWaypointsSegment()
    {
        if(side == false)
        {
            
            if (ArmInContactWithFloor.LeftArmIsInContactWithFloor)
            {
                internalWaypoints = Waypoints.LeftArmWaypoints;
            }
            else
            {
                internalWaypoints = Waypoints.SecondSegmentLeft;
            }
        }
        else
        {
            
            if (ArmInContactWithFloor.RightArmIsInContactWithFloor)
            {
                internalWaypoints = Waypoints.RightArmWaypoints;
            }
            else
            {
                internalWaypoints = Waypoints.SecondSegmentRight;
            }
        }

        alreadyLoadedSecondSegment = true;
        currentWaypointIndex = 0;

        currentWaypoint = internalWaypoints[currentWaypointIndex];
        currentWaypointIndex++;

        direction = currentWaypoint.position - transform.position;
        direction.Normalize();
    }
}
