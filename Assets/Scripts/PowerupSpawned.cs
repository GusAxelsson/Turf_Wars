using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawned : MonoBehaviour
{
    public GameObject[] powerList;

    public bool debugSpawnArea = false;

    // map bouderies
    private float xStart = -9.0f;
    private float yStart = 3.0f;
    private int powerGridW = 18;
    private int powerGridH = 8;
    private GameObject[,] powerGrid;


    // float timers
    private float lastPowerUpTime = 0.0f;
    private float timeUntilPowerup = 0;

    // amount of powerups on map
    public int totalPowerups = 0;
    public int maxAllowedPowerups = 8;

    private void PowerToSpawn() 
    {
        int powerIndex = Random.Range(0, powerList.Length);
        int xIndex = Random.Range(0, powerGridW);
        int yIndex = Random.Range(0, powerGridH);
        if (powerGrid[xIndex, yIndex] != null)
        {
            PowerToSpawn();
        }
        else
        {
            powerGrid[xIndex, yIndex] = Instantiate(powerList[powerIndex], new Vector3(xStart + xIndex + 0.5F, yStart - yIndex - 0.5F, 0), Quaternion.identity);
        }
     }

    // Start is called before the first frame update
    void Start()
    {
        lastPowerUpTime = Time.time;
        powerGrid = new GameObject[powerGridW, powerGridH];
    }

    // Update is called once per frame
    void Update()
    {
        // debug
        if (debugSpawnArea & totalPowerups <= 100)
        {
            PowerToSpawn();
        }
        // If its time to spawn a powerup again
        else if (Time.time - lastPowerUpTime >= timeUntilPowerup & totalPowerups <= maxAllowedPowerups)
        {
            // keep track of powerups on the map
            lastPowerUpTime = Time.time;
            timeUntilPowerup = Random.Range(1.0f, 6.0f);
            totalPowerups += 1;

            // spawn powerup
            PowerToSpawn();
        }
    }
}
