using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float autoFireRate;

    public Vector3 velocity;
    public float gravityModifier;

    public CharacterController controller;
    public Transform myCameraHead;

    public float mouseSensitivity;
    private float cameraVerticalRotation = 0;

    private string HORIZONTAL_KEY = "Horizontal";
    private string HORIZONTAL_RAW_KEY = "Mouse X";
    private string VERTICAL_KEY = "Vertical";
    private string VERTICAL_RAW_KEY = "Mouse Y";

    public GameObject bullet;
    public Transform firePoint;

    public GameObject muzzleFlash;
    public GameObject bulletHole;
    public GameObject waterLeak;

    void Start()
    {
        Debug.Log("Game Started");
    }

   
    void Update()
    {
        PlayerMovement();
        CameraMovement();
        Shoot();
        Invoke(nameof(AutoFire), autoFireRate);
    }

    private void AutoFire()
    {
        RaycastHit hit;
        if(Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, 200f))
        {
            if(Vector3.Distance(myCameraHead.position, hit.point) >= 2f)
            {
                firePoint.LookAt(hit.point);
                if (hit.collider.tag == "Enemies")
                {
                    if (hit.collider.tag == "Shootable")
                        Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));

                    if (hit.collider.tag == "WaterLeaker")
                        Instantiate(waterLeak, hit.point, Quaternion.LookRotation(hit.normal));

                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    Instantiate(muzzleFlash, firePoint.position, firePoint.rotation, firePoint);
                }
            }
            else
            {
                firePoint.LookAt(myCameraHead.position + (myCameraHead.forward * 50f));
            }
        }
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
                    if(hit.collider.tag == "Shootable")
                        Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));

                    if (hit.collider.tag == "WaterLeaker")
                        Instantiate(waterLeak, hit.point, Quaternion.LookRotation(hit.normal));
                }

                //if(hit.collider.tag == "Enemies")
                //    Destroy(hit.collider.gameObject);
            }
            else
            {
                firePoint.LookAt(myCameraHead.position + (myCameraHead.forward * 50f));
            }
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            Instantiate(muzzleFlash, firePoint.position, firePoint.rotation, firePoint);
        }
    }

    private void CameraMovement()
    {
        float mouseX = Input.GetAxisRaw(HORIZONTAL_RAW_KEY) * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxisRaw(VERTICAL_RAW_KEY) * Time.deltaTime * mouseSensitivity;

        cameraVerticalRotation = cameraVerticalRotation - mouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -80f, 40f);

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

        velocity.y += Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) * gravityModifier;
        controller.Move(velocity);

        if (controller.isGrounded)
        {
            velocity.y = Physics.gravity.y * Time.deltaTime;
        }
    }
}
