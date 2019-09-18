using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject whatever;
    public Camera camera;

    private Rigidbody2D rb;
    private Animator animator;
    private SortingGroup sortingGroup;

    private Vector2 moveVelocity;
    private bool flippedDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sortingGroup = GetComponent<SortingGroup>();

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
        Vector2 cameraPos = rb.position + moveVelocity * Time.fixedDeltaTime;
        camera.transform.position = new Vector3(cameraPos.x, cameraPos.y - 5f, camera.transform.position.z);
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        // set the layer based on y;
        sortingGroup.sortingOrder = (int)(transform.position.y * -100);
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
