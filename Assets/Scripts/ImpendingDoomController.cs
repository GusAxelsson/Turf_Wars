using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpendingDoomController : MonoBehaviour
{
    public GameObject player1; //lawn
    public GameObject player2; //plant

    public GameObject meteor;
    public GameObject shadow; 
    public GameObject volcanoEvent;
    public float speed = 1.0F;

    private float totalDistance;

    void Start()
    {
        totalDistance = (meteor.transform.position - transform.position).magnitude;
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        volcanoEvent = GameObject.FindGameObjectWithTag("VolcanoEvent");
    }

    void Update()
    {
        float distance = (meteor.transform.position - transform.position).magnitude;
        float opacity = 1.0F - distance / totalDistance;

        shadow.GetComponent<SpriteRenderer>().color = new Vector4(1.0F, 1.0F, 1.0F, opacity);

        float step = speed * Time.deltaTime;
        meteor.transform.position = Vector3.MoveTowards(meteor.transform.position, transform.position, step);

        if (meteor.transform.position == transform.position)
        {
            AudioSource debrisSound = volcanoEvent.GetComponent<AudioSource>();
            debrisSound.Play();
            hitPlayer();
            Destroy(this.gameObject);
        }
    }

    void hitPlayer()
    {
        if ((player1.transform.position - transform.position).magnitude < 1.5F)
        {
            player1.GetComponent<MovementController>().powerUpCollected(3);
        }


        if ((player2.transform.position - transform.position).magnitude < 1.5F)
        {
            player2.GetComponent<MovementController>().powerUpCollected(3);
        }
    }
}
