using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyHand : MonoBehaviour
{
    private Vector3 startPosition;
    private bool isResetting = false;
    private Rigidbody2D rb;

    [HideInInspector] public float dropSpeed;
    [HideInInspector] public float waitTime;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * dropSpeed;
        if (isResetting && Vector2.Distance(transform.position, startPosition) <= 0.1f)
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isResetting = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.GetContact(0);
            if (Vector2.Dot(contact.normal, Vector2.up) == 1)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage();
                ResetPosition();
            } 
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            ResetPosition(); 
        }
    }

    public void SlamHand()
    {
        if (!isResetting)
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            rb.velocity = Vector3.down;
        }
    }
    public void ResetPosition()
    {
        isResetting = true;
        rb.velocity = Vector2.up;
    }
}
