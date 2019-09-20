using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Camera camera;
    public WeaponController test;
    private Rigidbody2D rb;
    private Animator animator;

    // this file handles controls and needs access to trigger events on objects that should also be attached to the player
    private PlayerController playerController;
    private AttackController attackController;

    private Vector2 moveVelocity;
    private bool flippedDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        attackController = GetComponent<AttackController>();
    }
    void Update()
    {
        HandleMoveInput();
        SetDirection();
    }

    void FixedUpdate()
    {
        HandleMove();
        HandleAction();
    }

    void HandleAction()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (playerController.equipped.GetComponent<WeaponController>())
            {
                attackController.Attack(playerController.equipped);
            }
        }
    }

    void HandleMove ()
    {
        // move camera with player
        float camDistance = Camera.main.transform.position.z;
        float camAngle = 30f;
        float offset = (float)Math.Tan(camAngle * (Math.PI / 180)) * Math.Abs(camDistance); // adj * angle (toa)
        Vector2 newCameraPos = rb.position + moveVelocity * Time.fixedDeltaTime;
        camera.transform.position = new Vector3(newCameraPos.x, newCameraPos.y - offset, camera.transform.position.z);

        // move player
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    void HandleMoveInput()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
        animator.SetBool("Running", moveInput.sqrMagnitude >= 0.1);
        
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
