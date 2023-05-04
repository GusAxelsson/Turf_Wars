using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerCapability : MonoBehaviour
{
    public GameObject flameThrowerPrefab;
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
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
                    break;
                case 1:
                    spawnPosition.x += this.renderer.bounds.size.x / 2;
                    break;
                case 2:
                    spawnPosition.y += this.renderer.bounds.size.y / 2;
                    break;
                case 3:
                    spawnPosition.x -= this.renderer.bounds.size.x / 2;
                    break;
            }

            /* TODO
            Vector2 bounds = this.renderer.bounds.size;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector2 pos = new Vector2(spawnPosition.x + i * bounds.x + bounds.x / 2, spawnPosition.y + bounds.y - j * bounds.y + bounds.y / 2);

                    this.tileManager.Mow(pos);
                }
            }*/

            Quaternion spawnRotation = Quaternion.Euler(0, 0, angle); 
            GameObject animation = Instantiate(flameThrowerPrefab, spawnPosition, spawnRotation);
        }
    }

    private void ModifySpawnPosition(Vector3 spawnPosition, float rotation)
    {

    }
}
