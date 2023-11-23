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
    // Start is called before the first frame update
    void OnEnable()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyType)
        {
            case "l_ground": case "l_sky":
                transform.position += Vector3.right * Time.deltaTime * speed;
            break;

            case "r_ground": case "r_sky":
                transform.position += Vector3.left * Time.deltaTime * speed;
            break;

            case "mr_ground":
                transform.position += new Vector3(-.5f,.5f,0) * Time.deltaTime * speed;
            break;

            case "ml_ground":
                transform.position += new Vector3(.5f,.5f,0) * Time.deltaTime * speed;
            break;
        }
    }

    public void GetAttacked()
    {
        if(!playerController.currentlyAttacking && !playerController.preparingAttack && inHitArea)
        {
            Debug.Log(gameObject.name +" hit!");
            playerController.enemyPos = transform.position;
            StartCoroutine(playerController.RaiseArmThenSmash(enemyType));
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
        playerController.health -= 1;
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
        if(playerController.currentlyAttacking && (col.name == "L_forearmBone" || col.name == "R_forearmBone") )
        {
            if(!shielded)
            {
                Destroy(gameObject);
                playerController.currentlyAttacking = false;
            }
            else if(shielded)
            {
                circle.color = new Color(1f, 0, 0, 0.3490196f);
                shielded = false;
            }
        }
       /* else if(col.name == "L_forearmBone")
        {
            speed = 0;
            StartCoroutine(Attack_L());
        }
        else if(col.name == "R_forearmBone")
        {
            speed = 0;
            StartCoroutine(Attack_R());
        }*/
        else if(col.name == "Stomach")
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
