using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    public GameObject impactEffect;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Goomba goomba = hitInfo.GetComponent<Goomba>();
        Koopa koopa = hitInfo.GetComponent<Koopa>();
        if (goomba  != null) {
            goomba.Hit();
        }
        // if (koopa != null) {
        //     koopa.EnterShell();
        // }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
