using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public CharacterController controller;
    void Start()
    {
        
    }

   
    void Update()
    {
        controller.Move(Vector3.forward * speed);
    }
}
