using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public CharacterController controller;
    [SerializeField] public float mouseSensitivity;
    private float cameraVerticalRotation = 0;
    [SerializeField] public Transform camera;

    private string HORIZONTAL_KEY = "Horizontal";
    private string HORIZONTAL_RAW_KEY = "Mouse X";
    private string VERTICAL_KEY = "Vertical";
    private string VERTICAL_RAW_KEY = "Mouse Y";


    void Start()
    {
        Debug.Log("Game Started");
    }

   
    void Update()
    {
        PlayerMovement();
        float mouseX = Input.GetAxisRaw(HORIZONTAL_RAW_KEY) * Time.deltaTime* mouseSensitivity;
        float mouseY = Input.GetAxisRaw(VERTICAL_RAW_KEY) * Time.deltaTime * mouseSensitivity;

        cameraVerticalRotation = cameraVerticalRotation - mouseY;
        transform.Rotate(Vector3.up * mouseX);
        camera.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);
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
