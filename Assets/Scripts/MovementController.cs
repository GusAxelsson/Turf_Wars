using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public GameObject prefabRangeAnimation;
    public GameObject prefabSwapAnimation;
    public GameObject prefabSpeedAnimation;
    public GameObject prefabStunAnimation;

    public Rigidbody2D rb;
    public float maxSpeed = 5f;
    Vector2 movementDirection;
    public Vector2 directionFacing = new Vector2(0, 1);
    public bool logMovement = false;

    public InputAction playerControlUP;
    public InputAction playerControlLEFT;
    public InputAction playerControlDOWN;
    public InputAction playerControlRIGHT;

    public Animator animator;
    public TileManager tileManager;
    public bool plant;

    // Powerup variables and timers 
    // speedPowerup 1
    public bool speedPower;
    public float speedPowerTime = 6.0f;
    public float currentSpeedTimer = 6.0f;
    public AudioSource speedBoostSound;
    // Invert Powerup 2 
    public bool invertPower;
    public float invertPowerTime = 6.0f;
    public float currentInvertTimer = 6.0f;
    public AudioSource invertSound;
    // Range Powerup 4
    public bool rangePower;
    public float defaultRange = 0.7f;
    public float currentRange = 0.7f;
    public float rangePowerTime = 4.0f;
    public float currentRangeTimer = 4.0f;
    public AudioSource rangeSound;
    // Stun Powerup(debuff) 3
    public bool stunPower;
    public float stunPowerTime = 6.0f;
    public float currentStunTimer = 6.0f;
    public AudioSource stunSound;

    // Create a list of parts.
    List<int> keysPressed =  new List<int>();


    // This section is recommended by documentation //
    private void OnEnable()
    {
        playerControlUP.Enable();
        playerControlDOWN.Enable();
        playerControlLEFT.Enable();
        playerControlRIGHT.Enable();
    }
    private void OnDisable()
    {
        playerControlUP.Disable();
        playerControlDOWN.Disable();
        playerControlLEFT.Disable();
        playerControlRIGHT.Disable();
    }
    //////////////////////////////////////////////////


    void updateControlState(InputAction action, int index)
    {
        if (action.ReadValue<float>() > 0)
        {
            if (!keysPressed.Contains(index))
            {
                keysPressed.Insert(0, index);
            }
        }
        else
        {
            if (keysPressed.Contains(index))
            {
                keysPressed.Remove(index);
            }
        }
    }

    void updateControlStates()
    {
        updateControlState(playerControlUP, 0);
        updateControlState(playerControlLEFT, 1);
        updateControlState(playerControlRIGHT, 2);
        updateControlState(playerControlDOWN, 3);

    
        if (keysPressed.Count > 0)
        {
            switch (keysPressed[0])
            {
                case 0:
                    movementDirection = new Vector2(0, 1);
                    break;
                case 1:
                    movementDirection = new Vector2(-1, 0);
                    break;
                case 2:
                    movementDirection = new Vector2(1, 0);
                    break;
                case 3:
                    movementDirection = new Vector2(0, -1);
                    break;
            }
        } else
        {
            movementDirection = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // read direction from playerControls
        updateControlStates();

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
        if (momentum.magnitude > 0 && !stunPower)
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
            removeAnimations("SpeedAnim");
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
            removeAnimations("SwapAnim");
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
            removeAnimations("StunAnim");
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
            removeAnimations("RangeAnim");
        }


        // Finally apply movement
        rb.MovePosition(rb.position + momentum * Time.fixedDeltaTime);
    }

    private void removeAnimations(string tag)
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(tag))
            {
                Destroy(child.gameObject);
            }
        }
    }

    private bool hasAnimation(string tag)
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    public void powerUpCollected(int powerUp)
    {
        if (powerUp == 1)
        {
            speedBoostSound.Play();
            speedPower = true;
            currentSpeedTimer = speedPowerTime;
            maxSpeed = 12;

            if (!hasAnimation("SpeedAnim"))
            {
                GameObject anim = Instantiate(prefabSpeedAnimation, this.gameObject.transform.position + new Vector3(0, 0.2F, 0), Quaternion.identity);
                anim.transform.parent = this.gameObject.transform;
            }
        }
        if (powerUp == 2)
        {
            invertSound.Play();
            invertPower = true;
            currentInvertTimer = invertPowerTime;

            if (!hasAnimation("SwapAnim"))
            {
                GameObject anim = Instantiate(prefabSwapAnimation, this.gameObject.transform.position + new Vector3(0, 0.2F, 0), Quaternion.identity);
                anim.transform.parent = this.gameObject.transform;
            }
        }
        if (powerUp == 3)
        {
            stunSound.Play();
            stunPower = true;
            currentStunTimer = stunPowerTime;

            if (!hasAnimation("StunAnim"))
            {
                GameObject anim = Instantiate(prefabStunAnimation, this.gameObject.transform.position, Quaternion.identity);
                anim.transform.parent = this.gameObject.transform;
            }

        }
        if (powerUp == 4)
        {
            rangeSound.Play();
            rangePower = true;
            currentRangeTimer = rangePowerTime;

            if (!hasAnimation("RangeAnim"))
            {
                GameObject anim = Instantiate(prefabRangeAnimation, this.gameObject.transform.position, Quaternion.identity);
                anim.transform.parent = this.gameObject.transform;
            }
        }
    }

    public Vector2 GetDirection()
    {
        return this.directionFacing;
    }
}