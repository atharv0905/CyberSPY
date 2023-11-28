using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    private int currentHealth;
    public int maxHealth;
    private EnemyUIController uiController;
    void Start()
    {
        currentHealth = maxHealth;
        uiController = GetComponentInChildren<EnemyUIController>();
        uiController.SetMaxHealth(currentHealth);
    }


    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        uiController.SetHealth(currentHealth);
        if(currentHealth <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
