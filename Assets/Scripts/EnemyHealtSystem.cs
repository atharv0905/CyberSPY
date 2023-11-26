using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int currentHealth;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void TakeDamage()
    {
        currentHealth--;
        if(currentHealth < 0 )
        {
            Destroy(gameObject);
        }
    }
}
