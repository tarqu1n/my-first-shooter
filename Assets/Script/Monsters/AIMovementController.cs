using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class AIMovementController : MonoBehaviour
{
    

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Animator animator;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath;
    private bool flippedDirection;

    private Seeker seeker;
    private Rigidbody2D rb;
    private MonsterController monsterController;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        monsterController = GetComponent<MonsterController>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void UpdatePath()
    {
        if (monsterController.target && seeker.IsDone())
        {
            seeker.StartPath(rb.position, monsterController.target.transform.position, OnPathingComplete);
        }
    }
    private void OnPathingComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        SetDirection();

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            // we reached the end
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void SetDirection()
    {
        if (rb.velocity.x < -0.1 && !flippedDirection)
        {
            transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            flippedDirection = true;
        }
        else if (rb.velocity.x > 0.1 && flippedDirection)
        {
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            flippedDirection = false;
        }

        animator.SetFloat("Vertical Vector", rb.velocity.y);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }
}
