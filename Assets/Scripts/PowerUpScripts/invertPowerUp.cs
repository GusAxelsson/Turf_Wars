using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invertPowerUp : MonoBehaviour
{
    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    
    {
        if (triggered == false){
            triggered = true;
            if (collision.tag == "Player1")
            {
                GameObject.FindGameObjectWithTag("Player2").GetComponent<MovementController>().powerUpCollected(2);
            }
            if (collision.tag == "Player2")
            {
                GameObject.FindGameObjectWithTag("Player1").GetComponent<MovementController>().powerUpCollected(2);
            }
            GameObject.Find("PowerupSpawner").GetComponent<PowerupSpawned>().totalPowerups -= 1;
            Destroy(gameObject);
        }
    }

}
