using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public Transform myCameraHead;
    private UICanvasController canvas;

    // firing variables
    public GameObject bullet;
    public Transform firePoint;
    public GameObject muzzleFlash;
    public GameObject bulletHole;
    public GameObject waterLeak;
    public GameObject blood;

    public bool canAutoFire;
    private bool shooting;
    private bool readyToShoot = true;

    public float timeBetweenShots;

    public int bulletsAvailable;
    public int totalBullets;
    public int magazineSize;
    public float reloadTime;
    public int damageAmount;
    public bool isReloading = false;

    // aiming variables
    public Transform aimPosition;
    private Vector3 gunStartPosition;
    private float aimTransitionSpeed = 2f;
    public float zoomAmount;

    void Start()
    {
        totalBullets -= magazineSize;
        bulletsAvailable = magazineSize;
        gunStartPosition = transform.localPosition;
        canvas = FindObjectOfType<UICanvasController>();
    }

    void Update()
    {
        Shoot();
        GunManager();
        UpdateAmmoText();
    }

    private void GunManager()
    {
        if (Input.GetKeyDown(KeyCode.R) && bulletsAvailable < magazineSize && !isReloading)
        {
            Reload();
        }

        if (Input.GetMouseButton(1))
        {
            transform.position = Vector3.MoveTowards(transform.position, aimPosition.position, aimTransitionSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, gunStartPosition, aimTransitionSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<CameraMove>().ZoomIn(zoomAmount);
        }
        if (Input.GetMouseButtonUp(1))
        {
            FindObjectOfType<CameraMove>().ZoomOut();
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

                    if (hit.collider.tag == "Enemies")
                        Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
                }

                if (hit.collider.tag == "Enemies")
                    hit.collider.GetComponent<EnemyHealthSystem>().TakeDamage(damageAmount);
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

    private void UpdateAmmoText()
    {
        canvas.ammoText.SetText(bulletsAvailable + "/" + magazineSize);
        canvas.totalBulletsText.SetText(totalBullets.ToString());
    }
}
