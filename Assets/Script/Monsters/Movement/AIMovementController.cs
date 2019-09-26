using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Random = UnityEngine.Random;

public class AIMovementController : MonoBehaviour
{
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float pathRecalcTime = 0.5f;

    public GameObject target;
    public Vector3 targetPosition = Vector3.zero;
    public Animator animator;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private bool flippedDirection;
    private bool stopped = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        seeker.RegisterModifier(new AlternativePath());
        rb = GetComponent<Rigidbody2D>();
    }

    private void UpdatePath()
    {
        if (target && seeker.IsDone())
        {
            Vector2 closestPoint = Physics2D.ClosestPoint(rb.position, target.GetComponent<Rigidbody2D>());
            seeker.StartPath(rb.position, closestPoint, OnPathingComplete);
        } else if (targetPosition != Vector3.zero)
        {
            seeker.StartPath(rb.position, targetPosition, OnPathingComplete);
        }
    }

    private void OnPathingComplete(Path p)
    {
        if (!p.error)
        {
            // needed to remove // if (this == null) return; in modifier apply function to actually run
            seeker.RunModifiers(Seeker.ModifierPass.PreProcess, p);
            path = p;
            currentWaypoint = 1;
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        if (!target || (target.GetInstanceID() != newTarget.GetInstanceID()))
        {
            target = newTarget;
            targetPosition = Vector3.zero;
            if (path != null)
            {
                seeker.CancelCurrentPathRequest();
            }
            UpdatePath();
        }
    }

    public void SetTarget(Vector3 newTargetPosition)
    {
        if (targetPosition != newTargetPosition)
        {
            target = null;
            targetPosition = newTargetPosition;

            if (path != null)
            {
                seeker.CancelCurrentPathRequest();
            }
            UpdatePath();
        }
    }

    public void StopMovement()
    {
        stopped = true;
    }

    public void StartMovement()
    {
        if (target || targetPosition != Vector3.zero)
        {
            stopped = false;
            UpdatePath();
        }
    }

    public void MoveRandomDistance(float distance)
    {
        float currX = rb.position.x;
        float currY = rb.position.y;

        float RandX = Random.Range(currX - distance, currX + distance);
        float RandY = Random.Range(currY - distance, currY + distance);

        SetTarget(new Vector3(RandX, RandY));
    }

    void FixedUpdate()
    {
        // update path on timer
        pathRecalcTime -= Time.deltaTime;
        if (pathRecalcTime <= 0f && !stopped && (target || targetPosition != Vector3.zero))
        {
            UpdatePath();
            pathRecalcTime = 0.5f;
        }

        if (path == null || stopped)
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
        SetDirection(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void SetDirection(Vector2 force)
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

        animator.SetFloat("Vertical Force", force.y);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);

        if (target)
        {
            animator.SetFloat("Target Vertical Distance", (target.transform.position - transform.position).y);
        } else if (targetPosition != Vector3.zero)
        {
            animator.SetFloat("Target Vertical Distance", (targetPosition - transform.position).y);
        }
    }
}
