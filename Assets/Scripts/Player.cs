using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public CharacterController controller;
    [SerializeField] public float mouseSensitivity;
    private float cameraVerticalRotation = 0;
    [SerializeField] public Transform myCameraHead;

    private string HORIZONTAL_KEY = "Horizontal";
    private string HORIZONTAL_RAW_KEY = "Mouse X";
    private string VERTICAL_KEY = "Vertical";
    private string VERTICAL_RAW_KEY = "Mouse Y";

    public GameObject bullet;
    public Transform firePoint;

    void Start()
    {
        Debug.Log("Game Started");
    }

   
    void Update()
    {
        PlayerMovement();
        CameraMovement();
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, 100f))
            {
                if(Vector3.Distance(myCameraHead.position, hit.point) >= 2f) 
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(myCameraHead.position + (myCameraHead.forward * 50f));
            }
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }

    private void CameraMovement()
    {
        float mouseX = Input.GetAxisRaw(HORIZONTAL_RAW_KEY) * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxisRaw(VERTICAL_RAW_KEY) * Time.deltaTime * mouseSensitivity;

        cameraVerticalRotation = cameraVerticalRotation - mouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -80f, 20f);

        transform.Rotate(Vector3.up * mouseX);
        myCameraHead.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);
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
