using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invertPowerUp : MonoBehaviour
{
   
    public GameObject powerUpScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player1")
        {
            GameObject.FindGameObjectWithTag("Player2").GetComponent<playerMovement>().powerUpCollected(2);
        }
        if (collision.tag == "Player2")
        {
            GameObject.FindGameObjectWithTag("Player1").GetComponent<playerMovement>().powerUpCollected(2);
        }
        GameObject.Find("PowerupSpawner").GetComponent<PowerupSpawned>().totalPowerups -= 1;
        Destroy(gameObject);
    }

}
