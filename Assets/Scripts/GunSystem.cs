using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public Transform myCameraHead;

    // firing variables
    public GameObject bullet;
    public Transform firePoint;
    public GameObject muzzleFlash;
    public GameObject bulletHole;
    public GameObject waterLeak;
    void Start()
    {
        
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, 100f))
            {
                if (Vector3.Distance(myCameraHead.position, hit.point) >= 2f)
                {
                    firePoint.LookAt(hit.point);
                    if (hit.collider.tag == "Shootable")
                        Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));

                    if (hit.collider.tag == "WaterLeaker")
                        Instantiate(waterLeak, hit.point, Quaternion.LookRotation(hit.normal));
                }

                if(hit.collider.tag == "Enemies")
                    Destroy(hit.collider.gameObject);
            }
            else
            {
                firePoint.LookAt(myCameraHead.position + (myCameraHead.forward * 50f));
            }
            Instantiate(bullet, firePoint.position, firePoint.rotation, firePoint);
            Instantiate(muzzleFlash, firePoint.position, firePoint.rotation, firePoint);
        }
    }
}
