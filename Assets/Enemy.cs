using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 pos;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetAttacked()
    {
        playerController.enemyPos = transform.position;
        StartCoroutine(playerController.RaiseArmThenSmash());
        //StartCoroutine(playerController.RaiseArmThenSmash());
    }

    void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
