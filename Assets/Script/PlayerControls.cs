using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Camera camera;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 moveVelocity;
    private bool flippedDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
        animator.SetBool("Running", moveInput.sqrMagnitude >= 0.1);
        SetDirection();
    }

    void FixedUpdate()
    {
        // move camera with player
        Vector2 cameraPos = rb.position + moveVelocity * Time.fixedDeltaTime;
        camera.transform.position = new Vector3(cameraPos.x, cameraPos.y - 5f, camera.transform.position.z);

        // move player
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
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
}
