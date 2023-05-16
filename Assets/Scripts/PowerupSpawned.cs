using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawned : MonoBehaviour
{
    public GameObject[] powerList;
    public GameObject tileHandler;

    public bool debugSpawnArea = false;

    // map bouderies
    private float xStart = -14.0f;
    private float yStart = 3.0f;
    private int powerGridW = 28;
    private int powerGridH = 10;
    private GameObject[,] powerGrid;
    private GameObject player1;
    private GameObject player2;


    // float timers
    private float lastPowerUpTime = 0.0f;
    private float timeUntilPowerup = 0;

    // amount of powerups on map
    public int totalPowerups = 0;
    public int maxAllowedPowerups = 8;

    private void PowerToSpawn() 
    {
            int powerIndex = Random.Range(0, powerList.Length);
            for (int i = 0; i < 10; i++){
                int xIndex = Random.Range(0, powerGridW);
                int yIndex = Random.Range(0, powerGridH);
                if(powerGrid[xIndex,yIndex] == null && tileHandler.GetComponent<TileManager>().TileIsAccessible(xIndex,yIndex) && !playersTooClose(xIndex, yIndex)){
                    powerGrid[xIndex, yIndex] = Instantiate(powerList[powerIndex],
                    new Vector3(xStart + xIndex + 0.5F, yStart - yIndex - 0.5F, 0), Quaternion.identity);
                    break;
                }
            }

            
    }
    // this code will check if the players are to close to the rolled position
    private bool playersTooClose(float xIndex,float yIndex){
        //convert to world coordinates
        xIndex = xIndex + xStart;
        yIndex = yStart - yIndex;
        // find distance to player coordinates
        Vector2 p1Diff = new Vector2(player1.transform.position.x - xIndex, player1.transform.position.y - yIndex);
        Vector2 p2Diff = new Vector2(player2.transform.position.x - xIndex, player2.transform.position.y - yIndex);
        if (p1Diff.magnitude > 2 & p2Diff.magnitude > 2){
            return false;
        }
        return true;
    } 

    

    // Start is called before the first frame update
    void Start()
    {
        lastPowerUpTime = Time.time;
        powerGrid = new GameObject[powerGridW, powerGridH];
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
    }

    private List<Vector2> GetAvailableTiles(){
        List<Vector2> availibleTiles = new List<Vector2>();
        for (int i = 0; i < powerGridW - 1; i++){
            for (int y = 0; y < powerGridH - 1; y++){
                Debug.Log("Loop: " + i + " " + y);
                if(tileHandler.GetComponent<TileManager>().TileIsAccessible(i,y)){
                    Debug.Log("Is accessable");
                    if(powerGrid[i,y] == null){
                        Debug.Log("is empty");
                        availibleTiles.Add(new Vector2(i,y));
                        Debug.Log("Added" + i + "." + y);
                    }
                }
            }
        }
        return availibleTiles;
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
