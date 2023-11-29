using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public Transform myCameraHead;
    private UICanvasController canvas;
    public Animator animator;
    private string currentGunName;
    private float fireRange;
    public bool isRocketLauncher;

    // firing variables
    public GameObject bullet;
    public Transform firePoint;
    public GameObject muzzleFlash;
    public GameObject rocketTrail;
    public GameObject bulletHole;
    public GameObject waterLeak;
    public GameObject blood;

    public bool canAutoFire;
    private bool shooting;
    private bool readyToShoot = true;

    public float timeBetweenShots;

    private int bulletsAvailable;
    public int totalBullets;
    public int magazineSize;
    public int addAmmoOnPickup;
    public float reloadTime;
    public int damageAmount;
    public float maxFireRange;
    private bool isReloading = false;

    // aiming variables
    public Transform aimPosition;
    private Vector3 gunStartPosition;
    private float aimTransitionSpeed = 2f;
    public float zoomAmount;

    string gunAnimationName;

    void Start()
    {
        totalBullets -= magazineSize;
        bulletsAvailable = magazineSize;
        gunStartPosition = transform.localPosition;
        currentGunName = gameObject.name;
        canvas = FindObjectOfType<UICanvasController>();
        fireRange = FindObjectOfType<WeaponSwitchSystem>().GetMaxFireRange();
    }

    void Update()
    {
        Shoot();
        GunManager();
        UpdateAmmoText();
        AnimationManager();
    }

    private void AnimationManager()
    {
        switch (currentGunName)
        {
            case "Pistol":
                gunAnimationName = "Pistol Reload";
                break;
            case "Rifle":
                gunAnimationName = "Rifle Reload";
                break;
            case "Sniper":
                gunAnimationName = "Sniper Reload";
                break;
            case "Rocket Laucher":
                gunAnimationName = "Rocket Launcher Reload";
                break;
        }
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

            if (Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, fireRange))
            {
                if (Vector3.Distance(myCameraHead.position, hit.point) >= 2f)
                {
                    firePoint.LookAt(hit.point);

                    if (!isRocketLauncher)
                    {
                        if (hit.collider.tag == "Shootable")
                            Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));

                        if (hit.collider.tag == "WaterLeaker")
                            Instantiate(waterLeak, hit.point, Quaternion.LookRotation(hit.normal));
                    }
                }

                if (hit.collider.tag == "Enemies" && !isRocketLauncher)
                {
                    Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
                    hit.collider.GetComponent<EnemyHealthSystem>().TakeDamage(damageAmount);
                }
            }
            else
            {
                firePoint.LookAt(myCameraHead.position + (myCameraHead.forward * 50f));
            }

            bulletsAvailable--;

            if (!isRocketLauncher)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation, firePoint);
                Instantiate(muzzleFlash, firePoint.position, firePoint.rotation, firePoint);
            }
            else
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                Instantiate(rocketTrail, firePoint.position, firePoint.rotation);
            }

            StartCoroutine(ResetShot());

        }
    }

    private void Reload()
    {
        animator.SetTrigger(gunAnimationName);
        isReloading = true;
        StartCoroutine(ReloadTime());

    }

    public void AddAmmo()
    {
        totalBullets += addAmmoOnPickup;
    }

    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        readyToShoot = true;
    }

    IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(reloadTime);

        int bulletsToAdd = magazineSize - bulletsAvailable;
        if (totalBullets > bulletsToAdd)
        {
            totalBullets -= bulletsToAdd;
            bulletsAvailable = magazineSize;
        }
        else
        {
            bulletsAvailable += totalBullets;
            totalBullets = 0;
        }

        isReloading = false;
    }

    private void UpdateAmmoText()
    {
        canvas.ammoText.SetText(bulletsAvailable + "/" + magazineSize);
        canvas.totalBulletsText.SetText(totalBullets.ToString());
    }
}
