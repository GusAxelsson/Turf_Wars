using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float acceleration = 1f;
    public float maxSpeed = 100f;
    Vector2 momentum;
    Vector2 movementDirection;
    Vector2 move;
    public InputAction playerControls;
    public Animator animator;

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }



    // Update is called once per frame
    void Update()
    {
        movementDirection = playerControls.ReadValue<Vector2>();

        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);
    }

    void FixedUpdate()
    {
        momentum += movementDirection * acceleration;
        if (movementDirection.magnitude < 0.5)
        {
            momentum.x = 0;
            momentum.y = 0;
        }
        if (momentum.magnitude > maxSpeed)
        {
            momentum = momentum.normalized * maxSpeed;
        }
        
        if (Mathf.Abs(movementDirection.x) > Mathf.Abs(movementDirection.y)) {
            move.x = momentum.x;
            move.y = 0;
            momentum.y = 0;
        }
        else
        {
            move.x = 0;
            move.y = momentum.y;
            momentum.x = 0;
        }
        //movement
        // rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
        Debug.Log(move);
    }
}
