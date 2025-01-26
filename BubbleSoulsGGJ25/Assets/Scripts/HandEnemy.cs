using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEnemy : MonoBehaviour
{
    public float dropSpeed = 2.0f;
    public float waitTime = .5f;

    private void Awake()
    {
        var rbh = GetComponentInChildren<RigidbodyHand>();
        rbh.dropSpeed = dropSpeed;
        rbh.waitTime = waitTime;
    }

}
