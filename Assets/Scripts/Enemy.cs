using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public Vector2 pos;
    public PlayerController playerController;
    public float speed = 1;
    public string enemyType;
    private bool inHitArea = false;
    public bool shielded;
    public SpriteRenderer circle;
    public GameObject arrow;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    private EventTrigger eventTrigger;
    // Start is called before the first frame update
    void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        EnemySpawner.enemiesToKill++;
        EnemySpawner.enemiesLeft.Add(gameObject);
        print("Kill:"+EnemySpawner.enemiesToKill + " Left:"+EnemySpawner.enemiesLeft.Count);
        if(enemyType == "l_shield" || enemyType == "r_shield")
        {audioSource = GetComponent<AudioSource>();}
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyType)
        {
            case "l_ground": case "l_sky": case "l_shield":
                rb.position += Vector2.right * Time.deltaTime * speed;
            break;

            case "r_ground": case "r_sky": case "r_shield":
                rb.position += Vector2.left * Time.deltaTime * speed;
            break;

            case "mr_ground": case "mr_shield":
                rb.position += new Vector2(-.5f,.5f) * Time.deltaTime * speed;
            break;

            case "ml_ground": case "ml_shield":
                rb.position += new Vector2(.5f,.5f) * Time.deltaTime * speed;
            break;
        }
    }

    public void GetAttacked()
    {
        switch(enemyType)
        {
            case "l_ground": case "l_sky": case "l_shield": case "ml_ground": case"ml_shield":
                if(!playerController.L_currentlyAttacking && !playerController.L_preparingAttack && inHitArea)
                {
                    playerController.L_enemyPos = transform.position;
                    StartCoroutine(playerController.RaiseArmThenSmash(enemyType));
                }
            break;

            case "r_ground": case "r_sky": case "r_shield": case "mr_ground":case "mr_shield":
                if(!playerController.R_currentlyAttacking && !playerController.R_preparingAttack && inHitArea)
                {
                    playerController.R_enemyPos = transform.position;
                    StartCoroutine(playerController.RaiseArmThenSmash(enemyType));
                }
            break;
        }
        //eventTrigger.enabled = false;
    }


    public IEnumerator Attack_Main()
    {
        yield return new WaitForSeconds(1f);
        if(col.enabled)
        {PlayerController.GetDamaged();}
        yield return null;
        yield return new WaitForSeconds(2f);
        StartCoroutine(Attack_Main());
    }
    public IEnumerator ShootArrow()
    {
       yield return new WaitForSeconds(3f);
       if(col.enabled)
       Instantiate(arrow, transform.position, Quaternion.identity);
       yield return null;
       StartCoroutine(ShootArrow());
    }
    IEnumerator DisableThenDestroy()
    {
        EnemySpawner.enemiesToKill--;
        EnemySpawner.enemiesLeft.Remove(gameObject);
        col.enabled = false;
        spriteRenderer.enabled = false;
        circle.enabled = false;

        if(playerController.L_currentlyAttacking)
        {playerController.L_currentlyAttacking = false;}
        else if(playerController.R_currentlyAttacking)
        {playerController.R_currentlyAttacking = false;}
        
        print("-Kill:"+EnemySpawner.enemiesToKill + " Left:"+EnemySpawner.enemiesLeft.Count);
        StartCoroutine(playerController.enemySpawner.SpawnWaveAfterClear());
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        yield return null;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(playerController.L_currentlyAttacking && col.name == "L_forearmBone")
        {
            if(!shielded)
            {
                StartCoroutine(DisableThenDestroy());
            }
            else if(shielded)
            {
                audioSource.Play();
                circle.color = new Color(1f, 0, 0, 0.3490196f);
                shielded = false;
            }
        }else if(playerController.R_currentlyAttacking && col.name == "R_forearmBone")
        {
            if(!shielded)
            {
                StartCoroutine(DisableThenDestroy());
            }
            else if(shielded)
            {
                audioSource.Play();
                circle.color = new Color(1f, 0, 0, 0.3490196f);
                shielded = false;
            }
        }else if(col.name == "Stomach")
        {
            speed = 0;
            StartCoroutine(Attack_Main());
        }

        if(col.name == "Hit Area" && (enemyType == "l_sky" || enemyType == "r_sky"))
        {
            StartCoroutine(ShootArrow());
        }

        if(col.name == "Hit Area")
        {
            circle.enabled = true;
            inHitArea = true;
        }
    }

    void OnTriggerExit2D()
    {
        speed = 1;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(/*col.name == "L_forearmBone" || col.name == "R_forearmBone" || */col.name == "Stomach")
        {
            speed = 0;
        }
    }
}
