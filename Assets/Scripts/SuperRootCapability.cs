using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRootCapability : MonoBehaviour
{
    public GameObject superRootPrefab;
    public TileManager tileManager;

    private MovementController movementController;
    private SpriteRenderer renderer;

    public float cooldown;

    public LayerMask mowerLayer;

    public bool rootCapability;

    void Start()
    {
        this.movementController = this.gameObject.GetComponent<MovementController>();
        this.renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;


        if (cooldown <= 0){
            rootCapability = true;
        } else {
            rootCapability = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && cooldown <= 0)
        {
            rootCapability = false;
            Vector3 spawnPosition = this.transform.position;
            GameObject animation = Instantiate(superRootPrefab, spawnPosition, Quaternion.identity);

            RootAttack(spawnPosition);

            cooldown = 15;
        }
    }

    void RootAttack(Vector3 playerPosition){
            
        // Plant grass in the area of the roots
        Vector2 rootArea = new Vector2(playerPosition.x, playerPosition.y);
        this.tileManager.Plant(rootArea, 2.5f);

        // Attack/Stun
        Collider2D[] mowers = Physics2D.OverlapCircleAll(playerPosition, 2f, mowerLayer);

        foreach (Collider2D mower in mowers)
        {
            mower.GetComponent<MovementController>().powerUpCollected(3);
        }
    }

   
}
