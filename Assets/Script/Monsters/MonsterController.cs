using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
public class MonsterController : MonoBehaviour
{
    [Header("Config")]
    public string id;
    public float moveSpeed = 4f;
    public int startHealth;
    public float attackRange;
    public Behaviour startBehaviour;
    
    [Header("Controllers")]
    public GameObject agroRangeObject;
    public Animator animator;
    public AttackBehaviour attackBehaviour;

    [Header("Effects")]
    public GameObject deathEffect;

    [Header("Current Config")]
    public int currentHealth;
    public Behaviour currentBehaviour;

    private GameObject target;
    private AgroController agroRangeController;
    private AIPath aiPath;

    private bool flippedDirection;

    void Start()
    {
        currentHealth = startHealth;
        currentBehaviour = startBehaviour;
        agroRangeController = agroRangeObject.GetComponent<AgroController>();
        aiPath = GetComponent<AIPath>();
    }

    void Update()
    {
        SetDirection();
        if (currentBehaviour == Behaviour.Attack)
        {
            AttackTarget();
        }
    }

    public void TakeDamage (int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        } else if (currentBehaviour != Behaviour.Attack)
        {
            SetBehaviour(Behaviour.Attack);
        }
    }

    public void AttackTarget ()
    {
        
        if (!target)
        {
            target = AquireTarget();
        }
        if (attackBehaviour)
        {
            attackBehaviour.RecieveTarget(target);
        }
    }

    public GameObject AquireTarget ()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        GameObject closestPlayer = null;
        float distance = Mathf.Infinity;
        foreach (GameObject player in players)
        {
            Vector2 diff = player.transform.position - transform.position;
            float currDistance = diff.sqrMagnitude;
            if (currDistance < distance)
            {
                closestPlayer = player;
                distance = currDistance;
            }
        }

        return closestPlayer;
    }

    void SetDirection()
    {
        if (aiPath.desiredVelocity.x < 0 && !flippedDirection)
        {
            transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            flippedDirection = true;
        }
        else if (aiPath.desiredVelocity.x > 0 && flippedDirection)
        {
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            flippedDirection = false;
        }

        animator.SetFloat("Vertical Vector", aiPath.desiredVelocity.y);
        animator.SetFloat("Speed", aiPath.desiredVelocity.sqrMagnitude);
    }

    public void SetBehaviour(Behaviour behaviour)
    {
        if (behaviour == Behaviour.Idle)
        {
            agroRangeController.Enable();
            currentBehaviour = behaviour;
        }
        else if (behaviour == Behaviour.Attack)
        {
            currentBehaviour = behaviour;
        }
    }

    public enum Behaviour
    {
        Idle = 0,
        Attack = 1
    }
}
