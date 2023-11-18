using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public CharacterController controller;

    private string HORIZONTAL_KEY = "Horizontal";
    private string VERTICAL_KEY = "Vertical";
    

    void Start()
    {
        Debug.Log("Game Started");
    }

   
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float x = Input.GetAxis(HORIZONTAL_KEY);
        float z = Input.GetAxis(VERTICAL_KEY);

        Vector3 movement = x * transform.right + z * transform.forward;
        movement = movement * Time.deltaTime * speed;
        controller.Move(movement);
    }
}
