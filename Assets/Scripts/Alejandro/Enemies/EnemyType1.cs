using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : EnemyBase
{
    Vector3 direction;
    public int currentWaypointIndex;
    Transform lastWaypoint;
    bool mustUpdateDirection;
    Vector3 lastPosition;
    [SerializeField]
    float shakeDamageScale;
    [SerializeField]
    float chipDamage;
    Rigidbody2D rb2d;
    BoxCollider2D bc2d;
    bool alive = true;





    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
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
        if (alive) { 
            lastPosition = transform.position;
            transform.position = Vector3.Lerp(transform.position, currentWaypoint.position, Mathf.SmoothStep(0, 1, Time.deltaTime * 10));
            if (currentWaypointIndex <= internalWaypoints.Count)
                MoveToWayPoint();

            TakeDamage();
        
            if(currentHealth<=0)
            {
                StartCoroutine(Die());
                alive = false;
            }
        }
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
                    internalWaypoints = Waypoints.LeftArmWaypoints;
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

    void TakeDamage()
    {
        try {
            if (transform.parent.gameObject.CompareTag("Arm"))
            {
                Vector3 direction = (currentWaypoint.position - transform.position).normalized;
                Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime;
                Debug.Log(shakeDamageScale * Vector3.Dot(direction, velocity));
                currentHealth -= shakeDamageScale * Vector3.Dot(direction, velocity);
            } 
        }
        catch { }
    }

    IEnumerator Die()
    {
        transform.parent = null;
        rb2d.gravityScale = 0.5f;
        float angle = Random.Range(0, Mathf.PI);
        float magnitude = Random.Range(20, 40);
        Vector3 force = magnitude * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
        Debug.Log(force);
        rb2d.AddForce(force,ForceMode2D.Impulse);

        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arm") && collision.gameObject.transform.parent == null)
        {
            currentHealth -= chipDamage;
        }
    }
}
