using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicMove : MonoBehaviour
{
    Animator animator;
    Transform player;

    public bool move;
    public bool rotate;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Move", move);
        animator.SetBool("Rotating", rotate);

        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
    }
}
