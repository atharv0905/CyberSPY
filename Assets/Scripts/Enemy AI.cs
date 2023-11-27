using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    private Transform player;
    private Animator animator;
    public Transform firePosition;

    // gaurding
    private Vector3 destinationPoint;
    private bool isDestinationSet;
    public float destinationRange;

    // chasing
    public float chaseRange;
    private bool isPlayerInChaseRange;

    // attacking
    public float attackRange;
    private bool isPlayerInAttackRange;
    public GameObject EnemeyBullet;
    public float grenadeThrowDelay;
    private bool isEnemyReadyToAttack = true;
    

    // melee attack
    public bool isEnemyMeleeAttacker;
    public int meleeDamageAmount;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        isPlayerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);
        isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!isPlayerInChaseRange && !isPlayerInAttackRange)
            Gaurding();
        if (isPlayerInChaseRange && !isPlayerInAttackRange)
            Chasing();
        if (isPlayerInChaseRange && isPlayerInAttackRange && isEnemyReadyToAttack)
            Attacking();
    }

    private void Gaurding()
    {
        if (!isDestinationSet)
            SearchForDestination();
        else
            agent.SetDestination(destinationPoint);

        Vector3 distanceToDestination = transform.position - destinationPoint;

        if(distanceToDestination.magnitude < 1f)
        {
            isDestinationSet = false;
        }
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (isEnemyReadyToAttack && !isEnemyMeleeAttacker)
        {
            animator.SetTrigger("Attacking");
            firePosition.LookAt(player);
            Instantiate(EnemeyBullet, firePosition.position, transform.rotation);
            isEnemyReadyToAttack = false;
            StartCoroutine(ThrowGrenadeDelay());
        }
        else if(isEnemyReadyToAttack && isEnemyMeleeAttacker)
        {
            animator.SetTrigger("Attacking");
        }
    }

    public void MeleeDamage()
    {
        if (isEnemyReadyToAttack)
        {
            player.GetComponent<PlayerHealthSystem>().TakeDamage(meleeDamageAmount);
        }
    }

    private void SearchForDestination()
    {
        float randomPositionZ = Random.Range(-destinationRange, destinationRange);
        float randomPositionX = Random.Range(-destinationRange, destinationRange);

        destinationPoint = new Vector3(transform.position.x + randomPositionX, transform.position.y, transform.position.z + randomPositionZ);

        if (Physics.Raycast(destinationPoint, -transform.up, 2f, whatIsGround))
        {
            isDestinationSet = true;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    IEnumerator ThrowGrenadeDelay()
    {
        yield return new WaitForSeconds(grenadeThrowDelay);
        isEnemyReadyToAttack = true;
    }
}
