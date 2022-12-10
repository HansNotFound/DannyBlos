using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameObject target;
    public float speed = 7f;
    Rigidbody2D bulletRB;
    public GameObject impactEffect;
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector3 moveDir = target.transform.position - transform.position;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y).normalized * speed;
        // impactEffect = new GameObject();
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        if (player  != null) {
            player.Hit();
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
