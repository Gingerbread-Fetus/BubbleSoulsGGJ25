using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerEnemy : Enemy
{
    public int moveDirection = -1;
    public float speed = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        sr.flipX = rb.velocity.normalized.x <= 0;
        print(rb.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage();
        }      
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Get the collision normal
            Vector2 normal = contact.normal;

            // Check if the actor hit the left or right side of the obstacle
            if (normal.x > 0.5f)
            {
                Debug.Log("Hit on the left side of the actor! Switching direction to right.");
                moveDirection = 1;
            }
            else if (normal.x < -0.5f)
            {
                Debug.Log("Hit on the right side of the actor! Switching direction to left.");
                moveDirection = -1;
            }
        }
    }
}
