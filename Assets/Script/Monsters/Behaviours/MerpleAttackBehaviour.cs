using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MerpleAttackBehaviour : AttackBehaviour
{
    public float baseAttackSpeed;
    public int basicAttackDamage;
    public float basicAttackRange = 1;

    private GameObject currentTarget;
    private MonsterController monsterController;
    private float attackTimer;
    void Start()
    {
        monsterController = GetComponent<MonsterController>();
        attackTimer = baseAttackSpeed;
    }

    void FixedUpdate()
    {
        if (currentTarget && attackTimer <= 0 && Vector2.Distance(transform.position, currentTarget.transform.position) < basicAttackRange)
        {
            BasicAttack();
        }

        attackTimer -= Time.deltaTime;
    }
    public override void BasicAttack()
    {
        monsterController.animator.ResetTrigger("Attack");
        monsterController.animator.SetTrigger("Attack");
        attackTimer = baseAttackSpeed;
    }

    public override void RecieveTarget (GameObject target)
    {
        currentTarget = target;
        // GetComponent<AIDestinationSetter>().target = target.transform;
    }

    // This is called by AttackCollisionHandlers further down in the tree.
    public override void OnHitTarget(GameObject hitTarget)
    {
        if (hitTarget.tag == "HitboxPlayer")
        {
            // This requires the players hitbox to be a child of the object with the player controller attached
            PlayerController playerController = hitTarget.GetComponentInParent<PlayerController>();
            playerController.RecieveDamage(basicAttackDamage);
        }
        if (hitTarget.tag == "HitboxShard")
        {
            ShardController shardController = hitTarget.GetComponentInParent<ShardController>();
            shardController.TakeDamage(basicAttackDamage);
        }
    }
}
