using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Transform mainCamera;

    void Start()
    {
        mainCamera = FindObjectOfType<CameraMove>().transform;
    }


    void Update()
    {
        transform.LookAt(transform.position + mainCamera.forward);
    }
}
