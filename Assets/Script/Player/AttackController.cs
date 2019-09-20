using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AttackController : MonoBehaviour
{

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
        if (timeBtwAttack > 0)
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void Attack(GameObject weapon)
    {
        if (timeBtwAttack <= 0)
        {
            WeaponController weaponController = weapon.GetComponent<WeaponController>();

            if (weaponController.weaponType == WeaponController.WeaponType.Ranged)
            {
                RangedWeaponController rangedWeaponController = weapon.GetComponent<RangedWeaponController>();
                rangedWeaponController.FireProjectile();
            } else
            {
                animator.SetTrigger("Attack");
                
            }
            timeBtwAttack = startTimeBtwAttack;
            isAttacking = true;
        }
    }
}
