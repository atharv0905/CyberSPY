using System;
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

    public bool canAutoFire;
    private bool shooting;
    private bool readyToShoot = true;

    public float timeBetweenShots;

    public int bulletsAvailable;
    public int totalBullets;
    public int magazineSize;
    public float reloadTime;
    public bool isReloading = false;

    void Start()
    {
        totalBullets -= magazineSize;
        bulletsAvailable = magazineSize;
    }

    void Update()
    {
        Shoot();
        GunManager();
    }

    private void GunManager()
    {
        if (Input.GetKeyDown(KeyCode.R) && bulletsAvailable < magazineSize && !isReloading)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        if(canAutoFire)
            shooting = Input.GetMouseButton(0);
        else
            shooting = Input.GetMouseButtonDown(0);

        if (shooting && readyToShoot && bulletsAvailable > 0 && !isReloading)
        {
            readyToShoot = false;

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

            bulletsAvailable--;

            Instantiate(bullet, firePoint.position, firePoint.rotation, firePoint);
            Instantiate(muzzleFlash, firePoint.position, firePoint.rotation, firePoint);

            StartCoroutine(ResetShot());

        }
    }

    private void Reload()
    {
        
        int bulletsToAdd = magazineSize - bulletsAvailable;

        if(totalBullets > bulletsToAdd)
        {
            totalBullets -= bulletsToAdd;
            bulletsAvailable = magazineSize;
        }
        else
        {
            bulletsAvailable += totalBullets;
            totalBullets = 0;
        }

        isReloading = true;
        StartCoroutine(ReloadTime());

    }

    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        readyToShoot = true;
    }

    IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }
}
