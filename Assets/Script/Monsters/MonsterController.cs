using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class MonsterController : MonoBehaviour
{

    [Header("Config")]
    public float moveSpeed = 4f;
    public int startHealth;
    public float attackRange;
    public Behaviour startBehaviour;
    

    [Header("Controllers")]
    public GameObject agroRangeObject;

    [Header("Effects")]
    public GameObject deathEffect;

    [Header("Current Config")]
    public int currentHealth;
    public Behaviour currentBehaviour;


    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private GameObject target;
    private AgroController agroRangeController;
    

    private bool flippedDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = startHealth;
        currentBehaviour = startBehaviour;
        agroRangeController = agroRangeObject.GetComponent<AgroController>();
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
        Debug.Log("Damage taken");
        if (damage <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AttackTarget ()
    {
        if (!target)
        {
            target = AquireTarget();
        }
        if (target)
        {
            GetComponent<AIDestinationSetter>().target = target.transform;
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
        if (moveVelocity.x > 0 && !flippedDirection)
        {
            transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            flippedDirection = true;
        }
        else if (moveVelocity.x < 0 && flippedDirection)
        {
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            flippedDirection = false;
        }
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
