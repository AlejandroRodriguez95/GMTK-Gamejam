using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : EnemyBase
{

    public int currentWaypointIndex;
    Transform lastWaypoint;
    bool mustUpdateDirection;
    Vector3 lastPosition;
    bool alive;
    float shakeDamageScale;
    float chipDamage;




    private void Start()
    {
        alive = true;

        currentWaypointIndex = 0;

        if(internalWaypoints.Count > 0)
        {
            currentWaypoint = internalWaypoints[currentWaypointIndex];
            currentWaypointIndex++;
        }

    }

    private void Update()
    {
        if (alive) {
            lastPosition = transform.position;
            transform.position = Vector3.Lerp(transform.position, currentWaypoint.position, Mathf.SmoothStep(0, 1, Time.deltaTime * 10));

            TakeDamage();
            if (currentHealth <= 0) {
                StartCoroutine(Die());
                alive = false;
            }

        }

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
                    transform.parent = armTransform;
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
                    internalWaypoints = Waypoints.RightForeArmWaypoints;
                    mustUpdateDirection = true;
                    
                   /* if (!alreadyLoadedThirdSegment)
                    {
                        alreadyLoadedThirdSegment = true;
                        internalWaypoints = Waypoints.RightArmWaypoints;
                        mustUpdateDirection = true;

                    }*/
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
    }

    void TakeDamage() {
        Vector2 direction = (currentWaypoint.position - transform.position).normalized;
        Vector2 velocity = (transform.position - lastPosition) / Time.deltaTime;
        try
        {
            if(transform.parent.gameObject.CompareTag("Arm"))
            currentHealth -= shakeDamageScale * Vector3.Dot(direction, velocity);
        }
        catch { }
        

    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arm") && transform.parent==null)
        {
            currentHealth -= chipDamage;
        }
    }
}
