using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSwitchSystem : MonoBehaviour
{
    private GunSystem activeGun;
    public List<GunSystem> allGuns = new List<GunSystem>();
    public int currentGunNumber;
    public string currentGunName;
    private float fireRange;
    void Start()
    {
        foreach(GunSystem gun in allGuns)
        {
            gun.gameObject.SetActive(false);
        }

        activeGun = allGuns[currentGunNumber];
        activeGun.gameObject.SetActive(true);
        currentGunName = activeGun.gameObject.name;
        fireRange = activeGun.gameObject.GetComponent<GunSystem>().maxFireRange;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            SwitchGun();
    }

    private void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);
            currentGunNumber++;

        if(currentGunNumber >= allGuns.Count)
            currentGunNumber = 0;

        activeGun = allGuns[currentGunNumber];
        activeGun.gameObject.SetActive(true);
        GetCurrentGunName();
        GetMaxFireRange();
    }
    
    public string GetCurrentGunName()
    {
        currentGunName = activeGun.gameObject.name;
        return currentGunName;
    }

    public float GetMaxFireRange()
    {
        fireRange = activeGun.gameObject.GetComponent<GunSystem>().maxFireRange;
        return fireRange;
    }
}
