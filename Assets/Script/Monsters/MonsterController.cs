using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
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

    public GameObject target;
    private AgroController agroRangeController;
    private Rigidbody2D rb;

    private bool flippedDirection;

    void Start()
    {
        currentHealth = startHealth;
        currentBehaviour = startBehaviour;
        agroRangeController = agroRangeObject.GetComponent<AgroController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (currentBehaviour == Behaviour.Attack || currentBehaviour == Behaviour.Rush)
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
        GameObject[] targets;

        if (currentBehaviour == Behaviour.Attack)
        {
            targets = GameObject.FindGameObjectsWithTag("Player");
        } else
        {
            targets = GameObject.FindGameObjectsWithTag("Shard");
        }

        GameObject closestTarget = null;
        float distance = Mathf.Infinity;
        foreach (GameObject target in targets)
        {
            Vector2 diff = target.transform.position - transform.position;
            float currDistance = diff.sqrMagnitude;
            if (currDistance < distance)
            {
                closestTarget = target;
                distance = currDistance;
            }
        }

        return closestTarget;
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
        Attack = 1, // attack player
        Rush = 2 // attack crystal
    }
}
