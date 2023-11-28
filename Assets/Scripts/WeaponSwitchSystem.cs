using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSwitchSystem : MonoBehaviour
{
    private GunSystem activeGun;
    public List<GunSystem> allGuns = new List<GunSystem>();
    public List<GunSystem> unlockableGuns = new List<GunSystem>();
    public int currentGunNumber;
    private float fireRange;
    void Start()
    {
        foreach(GunSystem gun in allGuns)
        {
            gun.gameObject.SetActive(false);
        }

        activeGun = allGuns[currentGunNumber];
        activeGun.gameObject.SetActive(true);
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
        GetMaxFireRange();
    }
    
    public float GetMaxFireRange()
    {
        fireRange = activeGun.gameObject.GetComponent<GunSystem>().maxFireRange;
        return fireRange;
    }

    public void AddGun(string gunName)
    {
        bool unlocked = false;
        if(unlockableGuns.Count > 0)
        {
            for(int i =0; i < unlockableGuns.Count; i++)
            {
                if (unlockableGuns[i].name == gunName)
                {
                    allGuns.Add(unlockableGuns[i]);
                    unlockableGuns.RemoveAt(i);

                    i = unlockableGuns.Count;
                    unlocked = true;
                }
            }
        }

        if (unlocked)
        {
            currentGunNumber = allGuns.Count - 2;
            SwitchGun();
        }
    }
}
