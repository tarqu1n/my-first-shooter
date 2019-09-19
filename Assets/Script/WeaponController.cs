using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public int damage;
    public float attackSpeed;
    public GameObject wielder;
    public GameObject hitEffect;

    private AttackController attackController;
    void Start()
    {
        attackController = wielder.GetComponent<AttackController>();

        // todo move this to attack controller and access weapon from there
        wielder.GetComponent<Animator>().SetFloat("AttackSpeedMultiplier", 1 / attackSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getDamage ()
    {
        return damage;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(attackController.isAttacking);
        Debug.Log(collider.gameObject.tag);
        if (collider.gameObject.tag == "Monster" && attackController.isAttacking)
        {
            attackController.isAttacking = false;
            collider.gameObject.GetComponent<MonsterController>().TakeDamage(getDamage());

            if (hitEffect)
            {
                Instantiate(hitEffect, collider.gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
