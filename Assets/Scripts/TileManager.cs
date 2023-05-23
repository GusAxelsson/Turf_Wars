using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // public vars
    public GameObject grassPrefab;
    public AudioSource audioSourcePlant;
    public AudioSource audioSourceCut;

    public float xStart = -14.0F;
    public float yStart = 2.5F;
    public float tileSize = 0.5F;
    public int gridWidth = 56;
    public int gridHeight = 20;
    public float positioningOffset = 0.1F;
    // private vars
    private GameObject[,] grid;
    private float grassSpriteH = 0.0F;
    private float grassSpriteW = 0.0F;
    private int totalGrassTiles;
    private int availibleTiles;

    public string mapLayout = "0000000000000000000000000000" +
                              "0000111000000000000001110000" +
                              "0000111000000000000001110000" +
                              "0000111000001111000001110000" +
                              "0000000000001111000000000000" +
                              "0000000000001111000000000000" +
                              "0000111000001111000001110000" +
                              "0000111000000000000001110000" +
                              "0000111000000000000001110000" +
                              "0000000000000000000000000000";

    void Start()
    {
        /*
        grassSpriteH = grassPrefab.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        grassSpriteW = grassPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        */
        
        xStart = xStart + grassPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        yStart = yStart + grassPrefab.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        
        grid = new GameObject[gridWidth, gridHeight];
        totalGrassTiles = 0;
        
        CountAvailibleTiles();
        NullGrid();
        InitializeGrid();
    }

    void Update()
    {
        //Debug.Log(GetGrassPercentage());   
    }

    public void DebugGridPos(Vector2 currentPos)
    {
        Debug.Log(WorldToGrid(currentPos.x,currentPos.y));
    }

    private void NullGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y] = null;
            }
        }
    }

    private void CountAvailibleTiles()
    {
        int counter = 0;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (TileIsAccessible(Mathf.FloorToInt(x / 2), Mathf.FloorToInt(y / 2)))
                {
                    counter++;
                }
                    
            } 
        }
        availibleTiles = counter;
    }

    /// <summary>
    /// Instantiates a grass tile with slight randomness in position.
    /// </summary>
    private GameObject instantiateGrass(int x, int y)
    {
        float xOffset = Random.Range(-positioningOffset, positioningOffset);
        float yOffset = Random.Range(-positioningOffset, positioningOffset);
        if (x == 0) 
        {
            xOffset = Random.Range(0, positioningOffset);
        }
        if (x == gridWidth - 1)
        {
            xOffset = Random.Range(-positioningOffset, 0);
        }
        if (y == 0)
        {
            yOffset = Random.Range(-positioningOffset, 0);
        }
        if (y == gridHeight - 1)
        {
            yOffset = Random.Range(0, positioningOffset);
        }
        return Instantiate(grassPrefab, new Vector3(xStart + grassSpriteW +(x * tileSize) + xOffset, yStart + grassSpriteH - (y * tileSize) + yOffset, 0), Quaternion.identity);
    }

    public bool TileIsAccessible(int x,int y)
    {
        if (mapLayout[(x + (y * 28))] == '0')
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Initializes the grid with grass tiles.
    /// </summary>
    private void InitializeGrid()
    {
        if (totalGrassTiles < ((gridHeight * gridWidth) / 2))
        {
            for (int x = 0; x < gridWidth; x++)
            {
                // Currently, what we're doing is that the left side of the screen has a higher probability to start out with grass.
                float floatingProbability = ((float)(gridWidth - x) / gridWidth);
                for (int y = 0; y < gridHeight; y++)
                {
                    // if we have populated half of the area with grass we are done: return
                    if (totalGrassTiles >= (availibleTiles / 2))
                    {
                        return;
                    }
                    if ((Random.Range(0F, 1F) <= floatingProbability) & TileIsAccessible(Mathf.FloorToInt(x / 2), Mathf.FloorToInt(y / 2)) & grid[x,y] == null) // need to check if grid is null 
                    {
                        GameObject grass = instantiateGrass(x, y);
                        grid[x, y] = grass;
                        totalGrassTiles++;
                    }
                }
            }
            // if we didnt fill half of the map with grass go again
            if(totalGrassTiles < (availibleTiles / 2))
            {
                InitializeGrid();
            }
        }
    }

    /// <summary>
    ///  Debug function
    /// </summary>
    private void FillGrass()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y] == null)
                {
                    GameObject grass = instantiateGrass(x, y);
                    grid[x, y] = grass;
                    totalGrassTiles++;
                }
            }
        }
    }

    /// <summary>
    /// Converts world position to grid position.
    /// </summary>
    private Vector2Int WorldToGrid(float worldX, float worldY)
    {
        int x = Mathf.RoundToInt((worldX - xStart) / tileSize);
        int y = Mathf.RoundToInt((yStart - worldY) / tileSize);
        x = Mathf.Clamp(x, 0, gridWidth - 1);
        y = Mathf.Clamp(y, 0, gridHeight - 1);
        return new Vector2Int(x, y);
    }

    /// <summary>
    /// Modifies an area in the grid around a certain position by either planting or mowing grass.
    /// Depending on the player type, this will either create or destroy grass.
    /// </summary>
    private void AffectArea(Vector2 playerPosition, bool plant, float range)
    {
        // Get the player's position within the grid.
        Vector2Int gridPosition = WorldToGrid(playerPosition.x, playerPosition.y);
        
        // Helper variables for calculating the range of candidate tiles...
        int maxDistance = Mathf.CeilToInt(range / tileSize);

        // Calculate the range of candidate tiles.
        int minX = Mathf.Clamp(gridPosition.x - maxDistance, 0, gridWidth - 1);
        int maxX = Mathf.Clamp(gridPosition.x + maxDistance, 0, gridWidth - 1);
        int minY = Mathf.Clamp(gridPosition.y - maxDistance, 0, gridHeight - 1);
        int maxY = Mathf.Clamp(gridPosition.y + maxDistance, 0, gridHeight - 1);

        // Go through all the candidate tiles and apply the desired behavior.
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                // GUARD: We do not change anything if the tile is already in its desired state.
                if ((!plant && grid[x, y] == null) || (plant && grid[x, y] != null) || !(TileIsAccessible(Mathf.FloorToInt(x / 2), Mathf.FloorToInt(y / 2))))
                {
                    continue;
                }

                // Now we need to calculate the actual distance of the candidate tile to the player,
                // to check if it is really within their radius.
                Vector2 grassPosition = new Vector2(xStart + x * tileSize, yStart - y * tileSize);
                float distance = Vector2.Distance(playerPosition, grassPosition);

                // GUARD: Bail, if the player is too far away.
                if (distance > range)
                {
                    continue;
                }

                if (plant)
                {
                    // Create a new grass tile if the player is a planter.
                    GameObject grass = instantiateGrass(x, y);
                    audioSourcePlant.Play();
                    grid[x, y] = grass;
                    totalGrassTiles++;
                }
                else
                {
                    // Remove a pre-existing grass tile if the player is a mower.
                    Destroy(grid[x, y]);
                    audioSourceCut.Play();
                    grid[x, y] = null;
                    totalGrassTiles--;
                }
            }
        }
    }

    public void Mow(Vector2 playerPosition, float range = 0.7F)
    {
        AffectArea(playerPosition, false, range);
    }

    public void Plant(Vector2 playerPosition, float range = 0.7F)
    {
        AffectArea(playerPosition, true, range);
    }

    public float GetGrassPercentage()
    {
        return (float)totalGrassTiles / (availibleTiles) * 100F;
    }
}