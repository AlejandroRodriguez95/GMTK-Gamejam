using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 1f;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    public AudioClip[] hitClip;
    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce((new Vector3(0, -2) - transform.position).normalized * speed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = rb.velocity;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        /*var dir = rb.velocity;
        if (dir != Vector2.zero)
        transform.rotation = new Quaternion(0, 0, Quaternion.LookRotation(dir).x, Quaternion.LookRotation(dir).w);*/
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Chest")
        {
            PlayerController.health--;
            StartCoroutine(DisableThenDestroy());
        }
    }

    private IEnumerator DisableThenDestroy()
    {
        //hit
        spriteRenderer.enabled = false;
        audioSource.clip = hitClip[Random.Range(0,hitClip.Length+1)];
        audioSource.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        yield return null;
    }
}
