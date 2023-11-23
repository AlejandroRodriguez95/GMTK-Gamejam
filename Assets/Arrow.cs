using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 1f;
    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce((new Vector3(0, -2) - transform.position).normalized * speed);
       
    }

    // Update is called once per frame
    void Update()
    {
        var dir = rb.velocity;
        if (dir != Vector2.zero)
        transform.rotation = new Quaternion(0, 0, Quaternion.LookRotation(dir).x, Quaternion.LookRotation(dir).w);
    }
}
