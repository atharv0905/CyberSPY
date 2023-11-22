using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicMove : MonoBehaviour
{
    Animator animator;
    Transform player;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Move", true);

        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
    }
}
