using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 pos;
    public PlayerController playerController;
    public int speed = 1;
    public string enemyType;
    // Start is called before the first frame update
    void Start()
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
        }
    }

    public void GetAttacked()
    {
        if(!playerController.currentlyAttacking)
        {
            playerController.enemyPos = transform.position;
            StartCoroutine(playerController.RaiseArmThenSmash());
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if(playerController.currentlyAttacking && (col.name == "L_forearmBone" || col.name == "R_forearmBone") )
        {
            Destroy(gameObject);
            playerController.currentlyAttacking = false;
        }
        else if(col.name == "L_forearmBone")
        {
            speed = 0;
            StartCoroutine(Attack_L());
        }
        else if(col.name == "R_forearmBone")
        {
            speed = 0;
            StartCoroutine(Attack_R());
        }
        else if(col.name == "Stomach")
        {
            speed = 0;
            StartCoroutine(Attack_Main());
        }
    }

    void OnTriggerExit2D()
    {
        speed = 1;
        StopAllCoroutines();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.name == "L_forearmBone" || col.name == "R_forearmBone" || col.name == "Stomach")
        {
            speed = 0;
        }
    }
}
