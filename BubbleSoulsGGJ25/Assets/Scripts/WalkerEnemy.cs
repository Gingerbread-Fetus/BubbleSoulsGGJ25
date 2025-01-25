using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerEnemy : Enemy
{
    public int moveDirection = -1;
    public float speed = 10f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
    }
}
