using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered == false)
        {
            triggered = true;
            collision.GetComponent<MovementController>().powerUpCollected(1);
            GameObject.Find("PowerupSpawner").GetComponent<PowerupSpawned>().totalPowerups -= 1;
            Destroy(gameObject);
        }
    }
}
