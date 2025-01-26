using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FloorTrapEnemy : Enemy
{
    
    public float interval = 2.0f; // Number of seconds before trigger
    public float distance = 1.0f; // Distance the spike moves up or down
    public float speed = 1.0f; // Speed at which the spike moves (unused variable)

    public float timer;
    private bool isUp = false;

    private Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        UpAndDown();
    }

    public void UpAndDown()
    {
        timer += Time.deltaTime;

        // Check if the interval has passed, then set True or False
        if (timer >= interval)
        {
            isUp = !isUp;
            timer = 0f;
        }

        // Move the object
        if (isUp)
        {
            transform.position = new Vector2(startPosition.x, startPosition.y + distance);
        }
        else
        {
            transform.position = startPosition;
        }
    }
}
