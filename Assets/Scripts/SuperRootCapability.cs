using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRootCapability : MonoBehaviour
{
    public GameObject superRootPrefab;
    public TileManager tileManager;

    private MovementController movementController;
    private SpriteRenderer renderer;

    void Start()
    {
        this.movementController = this.gameObject.GetComponent<MovementController>();
        this.renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            Vector3 spawnPosition = this.transform.position;
            spawnPosition.y -= 0.1F;
            GameObject animation = Instantiate(superRootPrefab, spawnPosition, Quaternion.identity);

            // Plant grass in the area of the roots
            Vector2 pos = new Vector2(spawnPosition.x, spawnPosition.y);
            this.tileManager.Plant(pos, 2.5f);

        }
    }
}
