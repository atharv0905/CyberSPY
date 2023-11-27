using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }


    void Update()
    {
        
    }

    public void TakeDamage(int amountOfDamage)
    {
        currentHealth -= amountOfDamage;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
