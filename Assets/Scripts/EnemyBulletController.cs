using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    Rigidbody rigidbody;
    public float upForce, forwardForce;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        ThrowGrenade();
    }

    private void ThrowGrenade()
    {
        rigidbody.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        rigidbody.AddForce(transform.up * upForce, ForceMode.Impulse);
    }

    void Update()
    {
        
    }

}
