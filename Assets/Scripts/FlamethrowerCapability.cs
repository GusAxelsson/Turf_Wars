using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerCapability : MonoBehaviour
{
    public GameObject flameThrowerPrefab;
    public TileManager tileManager;

    private MovementController movementController;
    private SpriteRenderer renderer;

    public float cooldown;
    private Vector3 stunArea;

    public LayerMask planterLayer;

    public bool flameCapability;

    void Start()
    {
        this.movementController = this.gameObject.GetComponent<MovementController>();
        this.renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        cooldown -= Time.deltaTime;

        if (cooldown <= 0){
            flameCapability = true;
        } else {
            flameCapability = false;
        }

        if (Input.GetKeyDown(KeyCode.RightControl) && cooldown <= 0)
        {
            flameCapability = false;
            Debug.Log(movementController.GetDirection());
            Vector2 direction = movementController.GetDirection();
           
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Round(angle / 90F) * 90F;

            Vector3 spawnPosition = this.transform.position;

            int angleEnum = (int)(angle / 90F) + 1;
            switch (angleEnum)
            {
                case 0:
                    spawnPosition.y -= this.renderer.bounds.size.y / 2;
                    stunArea = spawnPosition - new Vector3(0, 2f, 0);
                    this.tileManager.Mow(stunArea, 2f);
                    break;
                case 1:
                    spawnPosition.x += this.renderer.bounds.size.x / 2;
                    stunArea = spawnPosition + new Vector3(2f, 0, 0);
                    this.tileManager.Mow(stunArea, 2f);
                    break;
                case 2:
                    spawnPosition.y += this.renderer.bounds.size.y / 2;
                    stunArea = spawnPosition + new Vector3(0, 2f, 0);
                    this.tileManager.Mow(stunArea, 2f);
                    break;
                case 3:
                    spawnPosition.x -= this.renderer.bounds.size.x / 2;
                    stunArea = spawnPosition - new Vector3(2f, 0, 0);
                    this.tileManager.Mow(stunArea, 2f);
                    break;
            }

            Quaternion spawnRotation = Quaternion.Euler(0, 0, angle); 
            GameObject animation = Instantiate(flameThrowerPrefab, spawnPosition, spawnRotation);

            
            // Attack/Stun
            Collider2D[] planters = Physics2D.OverlapCircleAll(stunArea, 2.5f, planterLayer);
            foreach (Collider2D planter in planters)
            {
                planter.GetComponent<MovementController>().powerUpCollected(3);
            }

            cooldown = 15;
        }
    }
}
