using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AttackController : MonoBehaviour
{

    public Transform attackPos;

    public LayerMask validTargets;
    public int damage;

    public float startTimeBtwAttack;
    public bool isAttacking;
    private float timeBtwAttack;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            isAttacking = false;
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetTrigger("Attack");
                isAttacking = true; 
                timeBtwAttack = startTimeBtwAttack;
            }
        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
}
