using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoEvent : MonoBehaviour
{
    public GameObject mapPowerUp;

    private GameObject player1;
    private GameObject player2;

    public GameObject background;
    
    public AudioSource rumbleSound;
    public AudioSource debrisSound;

    private Vector3 originalBackgroundTransform;
    private float rumbleDistance = 0.05F;

    private bool activated = false;
    private bool spawned = false;
    private bool rumbling = true;

    private float spawnTimer = 5.0F;
    private float rumbleTime = 0F;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        originalBackgroundTransform = background.transform.position;
    }

    public void SpawnMapPower()
    {
        Vector3 desiredPosition = new Vector3(0F, 1.4F, 0);
        Vector3 alternativePosition = new Vector3(0F, -5.4F, 0);

        Vector3 p1 = player1.transform.position - desiredPosition;
        Vector3 p2 = player2.transform.position - desiredPosition;

        Vector3 p1A = player1.transform.position - alternativePosition;
        Vector3 p2A = player2.transform.position - alternativePosition;

        float smallestDistanceDesired = Mathf.Min(p1.sqrMagnitude, p2.sqrMagnitude);
        float smallestDistanceAlternative = Mathf.Min(p1A.sqrMagnitude, p2A.sqrMagnitude);

        Vector3 finalPositon = desiredPosition;

        if (smallestDistanceDesired <= 4 && smallestDistanceAlternative > smallestDistanceDesired)
        {
            finalPositon = alternativePosition;
        }

        Instantiate(mapPowerUp,
            finalPositon, Quaternion.identity
        );
    }


    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            return;
        }

        if (!spawned)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0.0F)
            {
                this.SpawnMapPower();
                this.spawned = true;
            }
        }

        if (rumbling)
        {
            rumbleTime += Time.deltaTime;

            if (rumbleTime > 0.05F)
            {
                rumbleTime = 0.0F;

                float offsetX = Random.RandomRange(-rumbleDistance, rumbleDistance);
                float offsetY = Random.RandomRange(-rumbleDistance, rumbleDistance);
                Vector3 rumblePosition = new Vector3(
                    originalBackgroundTransform.x + offsetX, originalBackgroundTransform.y + offsetY, originalBackgroundTransform.z
                );
                background.transform.position = rumblePosition;

            }

        }
    }

    public void terminateRumble()
    {
        this.rumbling = false;
        background.transform.position = originalBackgroundTransform;
    }

    public void activate()
    {
        this.activated = true;
        rumbleSound.Play();
    }
}
