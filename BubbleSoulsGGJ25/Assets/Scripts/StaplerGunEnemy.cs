using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerGunEnemy : Enemy
{
    [SerializeField] GameObject projectilePrefab;
    GameObject playerTarget;

    public float fireRate = .5f;
    private float rotationSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Fire(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsTarget();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            print("Player in range");
            playerTarget = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            playerTarget = null;
        }
    }

    private void RotateTowardsTarget()
    {
        if(playerTarget)
        {
            Vector3 targetDirection = playerTarget.transform.position - transform.position;

            transform.rotation = Quaternion.LookRotation(Vector3.forward + 90, targetDirection);
        }
    }

    public void Fire()
    {
        if (playerTarget)
        {
            var newProjectile = Instantiate(projectilePrefab, gameObject.transform.position, new Quaternion());
            //newProjectile.transform.parent = transform; 
        }
    }
}
