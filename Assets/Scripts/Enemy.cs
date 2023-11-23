using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        if(enemyType == "l_shield" || enemyType == "r_shield")
        {audioSource = GetComponent<AudioSource>();}
        playerController = GameObject.FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        EnemySpawner.enemiesToKill++;
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
    }

    public IEnumerator Attack_L()
    {
        yield return new WaitForSeconds(3f);
        playerController.l_health -= 1;
        yield return null;
        StartCoroutine(Attack_L());
    }

    public IEnumerator Attack_R()
    {
        yield return new WaitForSeconds(3f);
        playerController.r_health -= 1;
        yield return null;
        StartCoroutine(Attack_R());
    }

    public IEnumerator Attack_Main()
    {
        yield return new WaitForSeconds(3f);
        PlayerController.health -= 1;
        yield return null;
        StartCoroutine(Attack_Main());
    }
    public IEnumerator ShootArrow()
    {
       yield return new WaitForSeconds(3f);
       Instantiate(arrow, transform.position, Quaternion.identity);
       yield return null;
       StartCoroutine(ShootArrow());
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(playerController.L_currentlyAttacking && col.name == "L_forearmBone")
        {
            if(!shielded)
            {
                Destroy(gameObject);
                EnemySpawner.enemiesToKill--;
            }
            else if(shielded)
            {
                audioSource.Play();
                circle.color = new Color(1f, 0, 0, 0.3490196f);
                shielded = false;
            }
            playerController.L_currentlyAttacking = false;
        }else if(playerController.R_currentlyAttacking && col.name == "R_forearmBone")
        {
            if(!shielded)
            {
                Destroy(gameObject);
                EnemySpawner.enemiesToKill--;
            }
            else if(shielded)
            {
                audioSource.Play();
                circle.color = new Color(1f, 0, 0, 0.3490196f);
                shielded = false;
            }
            playerController.R_currentlyAttacking = false;
        }else if(col.name == "Stomach")
        {
            speed = 0;
            StartCoroutine(Attack_Main());
        }

        if(col.name == "Hit Area" && (enemyType == "l_sky" || enemyType == "r_sky") )
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
