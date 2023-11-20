using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float autoFireRate;

    // gravity variables
    public Vector3 velocity;
    public float gravityModifier;

    // camera movement variables
    public CharacterController controller;
    public Transform myCameraHead;

    public float mouseSensitivity;
    private float cameraVerticalRotation = 0;

    // input key constants
    private const string HORIZONTAL_KEY = "Horizontal";
    private const string HORIZONTAL_RAW_KEY = "Mouse X";
    private const string VERTICAL_KEY = "Vertical";
    private const string VERTICAL_RAW_KEY = "Mouse Y";
    private const string JUMP_KEY = "Jump";


    // Animation variables
    public Animator myAnimator;
    private const string ANIM_PLAYER_SPEED = "PlayerSpeed";

    // firing variables
    public GameObject bullet;
    public Transform firePoint;

    public GameObject muzzleFlash;
    public GameObject bulletHole;
    public GameObject waterLeak;

    // jumping variables
    public float jumpHeight;
    private bool readyToJump;
    public Transform ground;
    public LayerMask groundLayer;
    public float groundDistance;

    // crouching variables
    private Vector3 crouchScale = new Vector3(1f, 0.5f, 1f);
    private Vector3 bodyScale;
    public Transform myBody;
    public float crouchSpeed;
    private bool isCrouching = false;

    // sprinting variables
    public float sprintSpeed;

    // sliding variables
    public bool isRunning = false;
    public float slideSpeed;

    void Start()
    {
        bodyScale = myBody.localScale;
        Debug.Log("Game Started");
    }

   
    void Update()
    {
        PlayerMovement();
        CameraMovement();
        Shoot();
        Jump();
        Crouch();
        //Invoke(nameof(AutoFire), autoFireRate);
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
            StartCrouching();
        if (Input.GetKeyUp(KeyCode.C))
            StopCrouching();
    }

    private void StopCrouching()
    {
        myBody.localScale = bodyScale;
        myCameraHead.position += new Vector3(0, 0.5f, 0);
        controller.height *= 2.5f;
        isCrouching = false;
    }

    private void StartCrouching()
    {
        myBody.localScale = crouchScale;
        myCameraHead.position -= new Vector3(0, 0.5f, 0);
        controller.height /= 2.5f;
        isCrouching = true;

        if(isRunning)
        {
            velocity = Vector3.ProjectOnPlane(myCameraHead.transform.forward, Vector3.up).normalized * slideSpeed * Time.deltaTime;
        }
    }   

    private void Jump()
    {
        readyToJump = Physics.OverlapSphere(ground.position, groundDistance, groundLayer).Length > 0;
        if (Input.GetButtonDown(JUMP_KEY) && readyToJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y) * Time.deltaTime;
        }
        controller.Move(velocity);
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
        if(Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            movement = movement * sprintSpeed * Time.deltaTime;
            isRunning = true;
        }
        else if (isCrouching)
        {
            movement = movement * crouchSpeed * Time.deltaTime;
        }
        else
        {
            movement = movement * speed * Time.deltaTime;
            isRunning = false;
        }

        myAnimator.SetFloat(ANIM_PLAYER_SPEED ,movement.magnitude);
        Debug.Log(movement.magnitude);
        controller.Move(movement);

        velocity.y += Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) * gravityModifier;
        controller.Move(velocity);

        if (controller.isGrounded)
        {
            velocity.y = Physics.gravity.y * Time.deltaTime;
        }
    }
}
