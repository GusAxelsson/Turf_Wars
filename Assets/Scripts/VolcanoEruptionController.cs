using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoEruptionController : MonoBehaviour
{
    public GameObject impendingDoomPrefab;
    public GameObject targetObject;
    public float spawnRadius = 2.0f;
    public float spawnRate = 0.3f;
    public float timer = 20.0f;
    private float spawnTimer = 0.0f;

    void Update()
    {
        timer -= Time.deltaTime;
        spawnTimer -= Time.deltaTime;

        if (timer > 0)
        {
            if (spawnTimer <= 0)
            {
                spawnTimer = spawnRate;
                Vector2 spawnPoint = Random.insideUnitCircle * spawnRadius;
                Vector3 spawnPosition = new Vector3(
                    targetObject.transform.position.x + spawnPoint.x,
                    targetObject.transform.position.y + spawnPoint.y,
                    targetObject.transform.position.z
                );
                Instantiate(impendingDoomPrefab, spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            Destroy(this.gameObject);
            GameObject.FindGameObjectWithTag("VolcanoEvent").GetComponent<VolcanoEvent>().terminateRumble();
        }
    }
}
