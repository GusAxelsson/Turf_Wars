using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoPowerup : MonoBehaviour
{
    private bool triggered = false;
    public GameObject prefabVolcanoEruption;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered == false)
        {
            triggered = true;

            GameObject player = collision.gameObject;
            
            if (collision.tag == "Player1")
            {
                player = GameObject.FindGameObjectWithTag("Player2");
            }
            if (collision.tag == "Player2")
            {
                player = GameObject.FindGameObjectWithTag("Player1");
            }

            GameObject eruption = Instantiate(prefabVolcanoEruption);

            eruption.GetComponent<VolcanoEruptionController>().targetObject = player;

            Destroy(gameObject);
        }
    }
}
