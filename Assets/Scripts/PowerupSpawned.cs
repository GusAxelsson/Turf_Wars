using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawned : MonoBehaviour
{
    public GameObject speedPower;
    public GameObject invertPower;

    // map bouderies
    private float xStart = -8.5f;
    private float yStart = 2.5f;
    private float xEnd = 8.5f;
    private float yEnd = -4.5f;

    // float timers
    private float lastPowerUpTime = 0.0f;
    private float timeUntilPowerup = 0;

    // amount of powerups on map
    public int totalPowerups = 0;
    public int maxAllowedPowerups = 6;

    private void powerToSpawn()
    {
        int powerToSpawn = Random.Range(1, 3);
        if (powerToSpawn == 1)
        {
            Instantiate(speedPower, new Vector3(Random.Range(xStart, xEnd), Random.Range(yEnd, yStart), 0), Quaternion.identity);
        }
        else
        {
            Instantiate(invertPower, new Vector3(Random.Range(xStart, xEnd), Random.Range(yEnd, yStart), 0), Quaternion.identity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPowerUpTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // If its time to spawn a powerup again
        if(Time.time - lastPowerUpTime >= timeUntilPowerup & totalPowerups <= maxAllowedPowerups)
        {
            // keep track of powerups on the map
            lastPowerUpTime = Time.time;
            timeUntilPowerup = Random.Range(3.0f, 8.0f);
            totalPowerups += 1;

            // spawn powerup
            powerToSpawn();
        }
    }
}
