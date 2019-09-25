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
        if (currentTarget && attackTimer <= 0 && Vector2.Distance(transform.position, Physics2D.ClosestPoint(transform.position, currentTarget.GetComponent<Rigidbody2D>())) < basicAttackRange)
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

        // make weapon face camera
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
        currentTarget = target;
        movementController.target = target;
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
