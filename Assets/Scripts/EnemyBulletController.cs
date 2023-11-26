using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    Rigidbody rigidbody;
    public float upForce, forwardForce;
    private float lifeSpan = 3f;
    
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
        lifeSpan -= Time.deltaTime;
        if( lifeSpan < 0 )
        {
            Destroy(gameObject);
        }
    }

}
