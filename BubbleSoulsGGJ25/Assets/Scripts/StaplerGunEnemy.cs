using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerGunEnemy : Enemy
{
    [SerializeField] GameObject projectilePrefab;
    public float fireRate = .5f;
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
        
    }

    public void Fire()
    {
        Instantiate(projectilePrefab, gameObject.transform.position, new Quaternion());
    }
}
