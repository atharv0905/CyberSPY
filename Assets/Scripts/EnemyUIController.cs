using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    public Slider healthSlider;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }
}
