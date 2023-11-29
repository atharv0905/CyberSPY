using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public int damageAmount;
    private PlayerHealthSystem playerHealthSystem;
    private EnemyHealthSystem enemyHealthSystem;

    private void Start()
    {
        playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();
        enemyHealthSystem = FindObjectOfType<EnemyHealthSystem>();

        StartCoroutine(DestroyExplosion());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealthSystem.TakeDamage(damageAmount);
        }
        if (other.CompareTag("Enemies"))
        {
            enemyHealthSystem.TakeDamage(damageAmount);
        }
    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
