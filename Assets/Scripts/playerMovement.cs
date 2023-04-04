using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float acceleration = 1f;
    Vector2 momentum;
    Vector2 movementDirection;
    Vector2 move;
    public InputAction playerControls;

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
        //input
        //movementDirection.x = Input.GetAxisRaw("Horizontal");
        //movementDirection.y = Input.GetAxisRaw("Vertical");

        movementDirection = playerControls.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        momentum += movementDirection;
        if (movementDirection.magnitude < 0.01)
        {
            momentum.x = 0;
            momentum.y = 0;
        }
        if (momentum.magnitude > 10)
        {
            momentum = momentum.normalized * 10;
        }
        
        if (Mathf.Abs(movementDirection.x) > Mathf.Abs(movementDirection.y)) {
            move.x = momentum.x;
            move.y = 0;
        }
        else
        {
            move.x = 0;
            move.y = momentum.y;
        }
        //movement
        // rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }
}
