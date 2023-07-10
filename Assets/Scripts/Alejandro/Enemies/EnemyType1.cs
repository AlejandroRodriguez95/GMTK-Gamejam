using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyType1 : EnemyBase
{

    public GameObject sfxManager;



    public int currentWaypointIndex;
    Transform lastWaypoint;
    bool mustUpdateDirection;
    Vector3 lastPosition;
    bool alive;
    [SerializeField]
    float shakeDamageScale;
    [SerializeField]
    float chipDamage;
    Rigidbody2D rb2d;
    [SerializeField] private AudioClip[] deathSounds;
    private AudioSource source;
    private bool gameOver;

    public GameObject L_forearmBone;
    public GameObject L_armBone;
    public GameObject R_forearmBone;
    public GameObject R_armBone;

    public float ChipDamage
    {
        get { return chipDamage; }
        set { chipDamage = value; }
    }


    private void Start()
    {
        L_forearmBone = GameObject.Find("L_forearmBone");
        L_armBone = GameObject.Find("L_armBone");
        R_forearmBone = GameObject.Find("R_forearmBone");
        R_armBone = GameObject.Find("R_armBone");

        alive = true;
        rb2d = GetComponent<Rigidbody2D>();
        currentWaypointIndex = 0;

        if(internalWaypoints.Count > 0)
        {
            currentWaypoint = internalWaypoints[currentWaypointIndex];
            currentWaypointIndex++;
        }

        // audio setup
        this.source = gameObject.GetComponent<AudioSource>();
        this.source.clip = this.deathSounds[Random.Range(0, deathSounds.Length)];
        this.source.volume = 0.5f;

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
            {
                if (!gameOver)
                {
                    gameOverImage.gameObject.SetActive(true);
                    gameOver = true;
                    StartCoroutine(FadeOutAfter(0));
                }
            }

            return;
        }

        currentWaypoint = internalWaypoints[currentWaypointIndex];
        currentWaypointIndex++;
    }

    private void LoadNextWaypointsSegment()
    {
        if (alreadyLoadedLastSegment)
            return;

        if(side == false)  // if left side
        {
            if (!alreadyLoadedSecondSegment) // has second segment been loaded? (forearm or body)
            {
                alreadyLoadedSecondSegment = true;

                if (ArmInContactWithFloor.LeftArmIsInContactWithFloor) // if arm is in idle pos
                {
                    internalWaypoints = Waypoints.LeftForeArmWaypoints;
                    mustUpdateDirection = true;

                    //if(Collision.)
                    //transform.parent = L_forearmBone.transform;
                }
                else // arm is not idle, then go through
                {
                    internalWaypoints = Waypoints.SecondSegmentLeft;
                    transform.parent = null;
                    alreadyLoadedThirdSegment = true;
                }
            }
            else // if already past the first segment
            {
                if (!alreadyLoadedThirdSegment)
                {
                    alreadyLoadedThirdSegment = true;
                    internalWaypoints = Waypoints.LeftArmWaypoints;
                    transform.parent = L_armBone.transform;
                }

                else
                {
                    internalWaypoints = Waypoints.LastSegmentLeft;
                    mustUpdateDirection = false;
                    alreadyLoadedLastSegment = true;
                    transform.parent = null;
                }
            }
        }
        else // if right side
        {
            if (!alreadyLoadedSecondSegment)
            {
                alreadyLoadedSecondSegment = true;

                if (ArmInContactWithFloor.RightArmIsInContactWithFloor)
                {
                    internalWaypoints = Waypoints.RightForeArmWaypoints;
                    mustUpdateDirection = true;
                    //transform.parent = L_forearmBone.transform;
                }
                else
                {
                    internalWaypoints = Waypoints.SecondSegmentRight;
                    transform.parent = null;
                    alreadyLoadedThirdSegment = true;
                }
            }
            else
            {
                if (!alreadyLoadedThirdSegment)
                {
                    alreadyLoadedThirdSegment=true;
                    internalWaypoints = Waypoints.RightArmWaypoints;
                }
                else
                {
                    internalWaypoints = Waypoints.LastSegmentRight;
                    mustUpdateDirection = false;
                    alreadyLoadedLastSegment = true;
                    transform.parent = null;
                }
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
        rb2d.gravityScale = 0.5f;
        float angle = Random.Range(-Mathf.PI/4,5*Mathf.PI/4);
        float magnitude = Random.Range(10, 20);
        Vector3 force = magnitude * new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0);
        rb2d.AddForce(force, ForceMode2D.Impulse);
        this.source.PlayOneShot(this.source.clip);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arm") && collision.gameObject.transform.parent == null)
        {
            currentHealth -= chipDamage;
            transform.parent = L_forearmBone.transform;
        }

      /*  if (ArmInContactWithFloor.LeftArmIsInContactWithFloor && ) // if arm is in idle pos
                {
                    internalWaypoints = Waypoints.LeftForeArmWaypoints;
                    mustUpdateDirection = true;

                    //if(Collision.)
                    transform.parent = L_forearmBone.transform;
                }*/
    }


    #region gameover stuff


    IEnumerator FadeOutAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds + .5f);
        StartCoroutine(FadeOut());

        yield return new WaitForSeconds(seconds + .5f);

        SceneManager.LoadScene("GameOver");
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float fadeTime = .5f;

        Color color = gameOverImage.color;

        while (elapsedTime < fadeTime)
        {
            yield return new WaitForEndOfFrame();

            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeTime;

            color.a = Mathf.Lerp(0f, 1f, normalizedTime);
            gameOverImage.color = color;
        }

        color.a = 1f;
        gameOverImage.color = color;
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float fadeTime = .5f;

        Color color = gameOverImage.color;

        while (elapsedTime < fadeTime)
        {
            yield return new WaitForEndOfFrame();

            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeTime;

            color.a = Mathf.Lerp(1f, 0f, normalizedTime);
            gameOverImage.color = color;
        }
        color.a = 0f;
        gameOverImage.color = color;
    }

    #endregion

}
