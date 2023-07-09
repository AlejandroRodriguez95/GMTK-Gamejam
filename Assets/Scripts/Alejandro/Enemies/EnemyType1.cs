using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : EnemyBase
{
    Vector3 direction;
    public int currentWaypointIndex;
    Transform lastWaypoint;
    bool mustUpdateDirection;




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
        transform.position = Vector3.Lerp(transform.position, currentWaypoint.position, Mathf.SmoothStep(0, 1, Time.deltaTime*10));
        if(currentWaypointIndex <= internalWaypoints.Count)
            MoveToWayPoint();
    }


    private void MoveToWayPoint()
    {
        if (mustUpdateDirection)
        {
            direction = currentWaypoint.localPosition - transform.localPosition;
        }


        Vector3 movement = direction * moveSpeed * Time.deltaTime;

        //transform.Translate(movement);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform != currentWaypoint)
            return;

        if (currentWaypointIndex >= internalWaypoints.Count)
        {
            currentWaypointIndex++;

            if (!alreadyLoadedLastSegment)
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
            if (!alreadyLoadedSecondSegment)
            {
                alreadyLoadedSecondSegment = true;

                if (ArmInContactWithFloor.LeftArmIsInContactWithFloor)
                {
                    internalWaypoints = Waypoints.LeftForeArmWaypoints;
                    mustUpdateDirection = true;
                    //transform.parent = armTransform;
                }
                else
                {
                    internalWaypoints = Waypoints.SecondSegmentLeft;
                    transform.parent = null;
                }
            }
            else
            {
                internalWaypoints = Waypoints.LastSegmentLeft;
                mustUpdateDirection = false;
                alreadyLoadedLastSegment = true;
            }
        }
        else
        {
            if (!alreadyLoadedSecondSegment)
            {
                alreadyLoadedSecondSegment = true;

                if (ArmInContactWithFloor.RightArmIsInContactWithFloor)
                {
                    internalWaypoints = Waypoints.RightArmWaypoints;
                    mustUpdateDirection = true;
                    transform.parent = armTransform;
                }
                else
                {
                    internalWaypoints = Waypoints.SecondSegmentRight;
                    transform.parent = null;
                }
            }
            else
            {
                internalWaypoints = Waypoints.LastSegmentRight;
                alreadyLoadedLastSegment = true;
            }
        }

        currentWaypointIndex = 0;

        currentWaypoint = internalWaypoints[currentWaypointIndex];
        currentWaypointIndex++;

        direction = currentWaypoint.position - transform.position;
        direction.Normalize();
    }
}
