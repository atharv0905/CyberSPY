using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string gunToPickUpName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<WeaponSwitchSystem>().AddGun(gunToPickUpName);
            Destroy(gameObject);
        }
    }
}
