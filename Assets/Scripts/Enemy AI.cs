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

    // gaurding
    private Vector3 destinationPoint;
    private bool isDestinationSet;
    public float destinationRange;

    // chasing
    public float chaseRange;
    private bool isPlayerInChaseRange;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        isPlayerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);
        if (!isPlayerInChaseRange)
            Gaurding();
        else if (isPlayerInChaseRange)
            Chasing();
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
