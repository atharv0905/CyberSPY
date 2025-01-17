using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    UICanvasController healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar = FindObjectOfType<UICanvasController>();
        healthBar.SetMaxHealth(maxHealth);
    }


    void Update()
    {

    }

    public void TakeDamage(int amountOfDamage)
    {
        currentHealth -= amountOfDamage;
        AudioManager.instance.PlayerSFX(4);
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            AudioManager.instance.PlayerSFX(3);
            AudioManager.instance.StopBackgroundMusic();
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerRespawn();
        }
    }

    public void HealPlayer(int healing)
    {
        currentHealth += healing;
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthBar.SetHealth(currentHealth);
    }

    public bool IsPickupFirstAidBox()
    {
        if(currentHealth == maxHealth)
            return false;
        return true;
    }

}
