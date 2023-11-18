using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public CharacterController controller;

    private string HORIZONTAL_KEY = "Horizontal";
    private string VERTICAL_KEY = "Vertical";
    private float DELTA_TIME = Time.deltaTime;

    void Start()
    {
        
    }

   
    void Update()
    {
        float x = Input.GetAxis(HORIZONTAL_KEY);
        float z = Input.GetAxis(VERTICAL_KEY);

        Vector3 movement = x * transform.right + z * transform.forward;
        controller.Move(movement * DELTA_TIME * speed);
    }
}
