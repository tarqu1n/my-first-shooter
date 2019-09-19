using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{

    public float moveSpeed = 4f;
    public int startHealth;
    public int health;
    public float attackRange;
    public Behaviour startBehaviour;
    public Behaviour currentBehaviour;

    public GameObject deathEffect;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private GameObject target;

    private bool flippedDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = startHealth;
        currentBehaviour = startBehaviour;
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
        if (currentBehaviour == Behaviour.Attack)
        {
            AttackTarget();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity);
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
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
            target = aquireTarget();
        }

        Collider2D targetCollider = target.GetComponent<Collider2D>();
        Vector2 closestPoint = targetCollider.ClosestPoint(transform.position);
        if (target && Vector2.Distance(transform.position, closestPoint) > attackRange)
        {
            // move towards target
            moveVelocity = Vector2.MoveTowards(transform.position, closestPoint, moveSpeed * Time.deltaTime) - new Vector2(transform.position.x, transform.position.y);
        } else
        {
            moveVelocity = new Vector2();
        }
    }

    public GameObject aquireTarget ()
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
        Debug.Log(moveVelocity.x.ToString());
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

    public enum Behaviour
    {
        Attack = 1
    }
}
