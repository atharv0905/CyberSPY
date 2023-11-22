using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform myPlayerHead;

    private float startFOV;
    private float targetFOV;
    public float FOVSpeed;

    private Camera myCamera;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        myCamera = GetComponent<Camera>();

        startFOV = myCamera.fieldOfView;
        targetFOV = startFOV;
    }

    void LateUpdate()
    {
        transform.position = myPlayerHead.position;
        transform.rotation = myPlayerHead.rotation;

        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetFOV, FOVSpeed * Time.deltaTime);
    }

    public void ZoomIn(float targetZoom)
    {
        targetFOV = targetZoom;
    }

    public void ZoomOut()
    {
        targetFOV = startFOV;
    }
}
