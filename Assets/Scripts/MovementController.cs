using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxSpeed = 5f;
    Vector2 movementDirection;
    public Vector2 directionFacing = new Vector2(0, 1);
    public bool logMovement = false;
    public InputAction playerControls;
    public Animator animator;
    public TileManager tileManager;
    public bool plant;

    // Powerup variables and timers 
    // speedPowerup 1
    public bool speedPower;
    public float speedPowerTime = 6.0f;
    public float currentSpeedTimer = 6.0f;
    // Invert Powerup 2 
    public bool invertPower;
    public float invertPowerTime = 6.0f;
    public float currentInvertTimer = 6.0f;
    // Range Powerup 4
    public bool rangePower;
    public float defaultRange = 0.7f;
    public float currentRange = 0.7f;
    public float rangePowerTime = 4.0f;
    public float currentRangeTimer = 4.0f;
    // Stun Powerup(debuff) 3
    public bool stunPower;
    public float stunPowerTime = 6.0f;
    public float currentStunTimer = 6.0f;



    // This section is recommended by documentation //
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    //////////////////////////////////////////////////

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
            directionFacing = movementDirection;
        }

        if (logMovement) Debug.Log(directionFacing);
        // Send directional info to the animator
        animator.SetFloat("Horizontal", directionFacing.x);
        animator.SetFloat("Vertical", directionFacing.y);
        animator.SetFloat("Speed", directionFacing.sqrMagnitude);
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
                tileManager.Plant(rb.position, currentRange);
            }
            else
            {
                tileManager.Mow(rb.position, currentRange);
            }
        }



        //////// Powerups logic  //////////

        //Speedpower//
        // if speedpower is still true count down timer
        if (speedPower)
        {
            currentSpeedTimer -= Time.deltaTime;
            // test = GameObject.Find("PlantBoosterInfo");
            // GameObject.Find("PowerupSpawner").SetActive(false);
        }
        // if current speedtimer is zero or smaller. set speedpower false and revert speed to normal
        if (currentSpeedTimer <= 0)
        {
            speedPower = false;
            maxSpeed = 5;
            // test.SetActive(true);
        }

        //InvertPower//
        if (invertPower)
        {
            currentInvertTimer -= Time.deltaTime;
            momentum = -momentum;
        }
        if (currentInvertTimer <= 0)
        {
            invertPower = false;
        }

        // StunPower//
        if (stunPower)
        {
            currentStunTimer -= Time.deltaTime;
            momentum.x = 0;
            momentum.y = 0;
            // Here a stun animation should probably be called
        }
        if (currentStunTimer <= 0)
        {
            stunPower = false;
        }

        // RangePower //
        if (rangePower)
        {
            currentRangeTimer -= Time.deltaTime;
            currentRange = 1.4f;
        }
        // if current speedtimer is zero or smaller. set speedpower false and revert speed to normal
        if (currentRangeTimer <= 0)
        {
            rangePower = false;
            currentRange = defaultRange;
        }


        // Finally apply movement
        rb.MovePosition(rb.position + momentum * Time.fixedDeltaTime);
    }


    public void powerUpCollected(int powerUp)
    {
        if (powerUp == 1)
        {
            speedPower = true;
            currentSpeedTimer = speedPowerTime;
            maxSpeed = 12;
        }
        if (powerUp == 2)
        {
            invertPower = true;
            currentInvertTimer = invertPowerTime;
        }
        if (powerUp == 3)
        {
            stunPower = true;
            currentStunTimer = stunPowerTime;
        }
        if (powerUp == 4)
        {
            rangePower = true;
            currentRangeTimer = rangePowerTime;
        }
    }

    public Vector2 GetDirection()
    {
        return this.directionFacing;
    }
}
