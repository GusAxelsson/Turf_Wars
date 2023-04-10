using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxSpeed = 5f;
    Vector2 movementDirection;
    Vector2 lastAnimDirection = new Vector2(0, 1);
    public bool logMovement = false;
    public InputAction playerControls;
    public Animator animator;
    public TileManager tileManager;
    public bool plant;

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

    /// <summary>
    /// Ensures that a vector does not include diagonal movement.
    /// </summary>
    private Vector2 simplifyDirectionalVector(Vector2 direction)
    {
        Vector2 newDirection = new Vector2(direction.x, direction.y);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            newDirection.x = Mathf.Sign(direction.x) * direction.magnitude;
            newDirection.y = 0;
        }
        else
        {
            newDirection.x = 0;
            newDirection.y = Mathf.Sign(direction.y) * direction.magnitude;
        }
        return newDirection;
    }

    // Update is called once per frame
    void Update()
    {
        // read direction from playerControls
        movementDirection = playerControls.ReadValue<Vector2>();
        movementDirection = simplifyDirectionalVector(movementDirection);

        // Only change sprite on actual movement.
        if (movementDirection.magnitude > 0)
        {
            lastAnimDirection = movementDirection;
        }

        if (logMovement) Debug.Log(lastAnimDirection);
        // Send directional info to the animator
        animator.SetFloat("Horizontal", lastAnimDirection.x);
        animator.SetFloat("Vertical", lastAnimDirection.y);
        animator.SetFloat("Speed", lastAnimDirection.sqrMagnitude);
    }

    void FixedUpdate()
    {

        Vector2 momentum = movementDirection * maxSpeed;
        if (logMovement)
        {
            Debug.Log(momentum);
        }
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

        // If we are going to move, we're properly triggering the mowing/planting depending on player type.
        if (momentum.magnitude > 0)
        {
            if (plant)
            {
                tileManager.Plant(rb.position);
            } else
            {
                tileManager.Mow(rb.position);
            }
        }

        // Apply movement
        rb.MovePosition(rb.position + momentum * Time.fixedDeltaTime);
    }
}
