using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int amountOfHealing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerHealthSystem>().IsPickupFirstAidBox())
            {
                other.GetComponent<PlayerHealthSystem>().HealPlayer(amountOfHealing);
                AudioManager.instance.PlayerSFX(1);
                Destroy(gameObject);
            }
        }
    }
}
