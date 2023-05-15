using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRootCapability : MonoBehaviour
{
    public GameObject superRootPrefab;
    public TileManager tileManager;

    private MovementController movementController;
    private SpriteRenderer renderer;

    public LayerMask enemyLayers;

    void Start()
    {
        this.movementController = this.gameObject.GetComponent<MovementController>();
        this.renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Vector3 spawnPosition = this.transform.position;
            GameObject animation = Instantiate(superRootPrefab, spawnPosition, Quaternion.identity);

            RootAttack(spawnPosition);
        }
    }

    void RootAttack(Vector3 playerPosition){
            
        // Plant grass in the area of the roots
        Vector2 rootArea = new Vector2(playerPosition.x, playerPosition.y);
        this.tileManager.Plant(rootArea, 2.5f);

        // Attack/Stun
        Collider2D[] mowers = Physics2D.OverlapCircleAll(playerPosition, 2f, enemyLayers);

        foreach (Collider2D mower in mowers)
        {
            mower.GetComponent<MovementController>().powerUpCollected(3);
        }
    }

   
}
