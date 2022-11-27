using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    public float walkSpeed;
    public bool isPatrol;
    private bool mustFlip;

    public Rigidbody2D rb;

    public Transform groundCheckPos;
    public LayerMask groundLayer;
    void Start()
    {
        isPatrol = true;
    }

    void Update()
    {
        if (isPatrol) {
            Patrol();
        }
    }


    void FixedUpdate()
    {
        if (isPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
        
    }
    void Patrol() 
    {
        if (mustFlip) 
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        isPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        isPatrol = true;
    }
}
