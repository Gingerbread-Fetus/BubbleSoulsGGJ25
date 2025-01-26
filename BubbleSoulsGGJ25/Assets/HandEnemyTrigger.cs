using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEnemyTrigger : MonoBehaviour
{
    [SerializeField] RigidbodyHand rbHand;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            print("Player has triggered the hand!");
            rbHand.SlamHand();
        }
    }
}
