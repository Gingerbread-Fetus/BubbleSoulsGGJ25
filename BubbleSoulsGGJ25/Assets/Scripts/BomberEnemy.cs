using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : Enemy
{
    [SerializeField] float dropActivationDistance = 15f;
    [SerializeField] float moveSpeed = 1f;

    public Transform startPoint;
    public Transform endPoint;
    public Transform controlPoint;

    private Rigidbody2D rb;
    private Player player;
    private bool isActive = false;
    private float t = 0f; //parameter for interpolation

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null
            && Mathf.Abs(transform.position.x - player.transform.position.x) <= dropActivationDistance
            && !isActive
            )
        {
            isActive = true;
            rb.gravityScale = 1f;
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            t += Time.fixedDeltaTime * moveSpeed;

            if (t  > 1f)
            {
                //Drop off map
                return;
            }

            Vector2 targetPosition = CalculateQuadraticBezierPoint(t, startPoint.position, controlPoint.position, endPoint.position);

            Vector2 movement = (targetPosition - rb.position) / Time.fixedDeltaTime;
            rb.velocity = movement;
        }
    }

    private Vector2 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // Quadratic Bezier formula: (1-t)^2 * P0 + 2 * (1-t) * t * P1 + t^2 * P2
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector2 point = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return point;
    }

    // Visualize the curve in the Scene view
    private void OnDrawGizmos()
    {
        if (startPoint == null || endPoint == null || controlPoint == null)
            return;

        Gizmos.color = Color.yellow;
        Vector2 previousPoint = startPoint.position;

        int segments = 20; // Number of segments for the curve
        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector2 currentPoint = CalculateQuadraticBezierPoint(t, startPoint.position, controlPoint.position, endPoint.position);
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }

        // Draw points for start, control, and end for clarity
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPoint.position, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(controlPoint.position, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPoint.position, 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage();
        }
    }
}
