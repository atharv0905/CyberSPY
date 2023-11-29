using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Rigidbody myRigidBody;
    public float bulletLife;
    public ParticleSystem explosionEffect;
    public bool isRocket;
    void Start()
    {
        
    }

    void Update()
    {
        BulletFly();
        bulletLife -= Time.deltaTime;
        if (bulletLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void BulletFly()
    {
        myRigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Enemies")
        //    Destroy(other.gameObject);
        if (isRocket)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
