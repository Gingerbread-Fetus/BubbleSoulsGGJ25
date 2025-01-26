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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage();
        }
    }
}
