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


    // This section is recommended by documentation
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    ////////////////
    


    // Update is called once per frame
    void Update()
    {
        // read direction from playerControls
        movementDirection = playerControls.ReadValue<Vector2>();
        // Send directional info to the animator
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);
    }

    void FixedUpdate()
    {
        momentum += movementDirection * acceleration;
        // If we are almost standing still. kill momentum
        if (movementDirection.magnitude < 0.5)
        {
            momentum.x = 0;
            momentum.y = 0;
        }
        // if we are going faster that "maxSpeed" cap speed
        if (momentum.magnitude > maxSpeed)
        {
            momentum = momentum.normalized * maxSpeed;
        }
        // make sure that we are always only moving in the (nwse) directions
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
        // Apply movement
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
        Debug.Log(move);
    }
}
