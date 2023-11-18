using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform myPlayerHead;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        transform.position = myPlayerHead.position;
        transform.rotation = myPlayerHead.rotation;
    }
}
