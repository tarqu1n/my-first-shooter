using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FinkleAttackBehaviour : AttackBehaviour
{
    public float baseAttackSpeed;
    public int basicAttackDamage;
    public float basicAttackRange = 10;

    public GameObject projectile;
    public GameObject projectileSpawn;

    private GameObject currentTarget;
    private MonsterController monsterController;
    private float attackTimer;
    private AIMovementController movementController;
    void Start()
    {
        monsterController = GetComponent<MonsterController>();
        movementController = GetComponent<AIMovementController>();
        attackTimer = baseAttackSpeed;
    }

    void FixedUpdate()
    {
        if (currentTarget)
        {
            bool targetInRange = Vector2.Distance(transform.position, Physics2D.ClosestPoint(transform.position, currentTarget.GetComponent<Rigidbody2D>())) < basicAttackRange;
            if (attackTimer <= 0 && targetInRange)
            {
                BasicAttack();
                
                // chance to move randomly
                if (Random.Range(0f, 1f) < 0.5)
                {
                    movementController.MoveRandomDistance(3);
                    movementController.StartMovement();
                }
            } else if (!targetInRange)
            {
                movementController.SetTarget(currentTarget);
                movementController.StartMovement();
            }
        }

        attackTimer -= Time.deltaTime;
    }
    public override void BasicAttack()
    {
        movementController.StopMovement();

        monsterController.animator.ResetTrigger("Attack");
        monsterController.animator.SetTrigger("Attack");
        attackTimer = baseAttackSpeed;

        // fire projectile
        Vector3 mouseInWorldSpace = currentTarget.transform.position;
        Vector3 difference = mouseInWorldSpace - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, transform.rotation.y, rotZ - 90f);

        GameObject instance = Instantiate(projectile, projectileSpawn.transform.position, rotation);
        Projectile instanceProjectile = instance.GetComponent<Projectile>();
        instanceProjectile.damage = 1;
        instanceProjectile.ownerType = Projectile.ProjectileOwnerType.Monster;

    }

    public override void RecieveTarget (GameObject target)
    {
        if (!currentTarget || currentTarget.GetInstanceID() != target.GetInstanceID())
        {
            currentTarget = target;
            movementController.SetTarget(target);
        }
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
