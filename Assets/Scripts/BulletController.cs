using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Rigidbody myRigidBody;
    void Start()
    {
        
    }

    void Update()
    {
        BulletFly();
    }

    private void BulletFly()
    {
        myRigidBody.velocity = transform.forward * speed;
    }
}
