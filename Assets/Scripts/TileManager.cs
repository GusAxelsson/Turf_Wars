using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject grassPrefab;
    public float xStart = -9.5F;
    public float yStart = 2.5F;
    public float tileSize = 0.5F;
    public int gridWidth = 40;
    public int gridHeight = 16;
    public float positioningOffset = 0.1F;
    private GameObject[,] grid;
    private int totalGrassTiles;

    void Start()
    {
        grid = new GameObject[gridWidth, gridHeight];
        totalGrassTiles = 0;
        InitializeGrid();
    }

    void Update()
    {
        //Debug.Log(GetGrassPercentage());   
    }

    /// <summary>
    /// Instantiates a grass tile with slight randomness in position.
    /// </summary>
    private GameObject instantiateGrass(int x, int y)
    {
        float xOffset = Random.Range(-positioningOffset, positioningOffset);
        float yOffset = Random.Range(-positioningOffset, positioningOffset);
        return Instantiate(grassPrefab, new Vector3(xStart + x * tileSize + xOffset, yStart - y * tileSize - yOffset, 0), Quaternion.identity);
    }

    /// <summary>
    /// Initializes the grid with grass tiles.
    /// </summary>
    private void InitializeGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            // Currently, what we're doing is that the left side of the screen has a higher probability to start out with grass.
            float floatingProbability = ((float) (gridWidth - x) / gridWidth);
            for (int y = 0; y < gridHeight; y++)
            {
                if (Random.Range(0F, 1F) < floatingProbability)
                {
                    GameObject grass = instantiateGrass(x, y);
                    grid[x, y] = grass;
                    totalGrassTiles++;
                }
                else
                {
                    grid[x, y] = null;
                }
            }
        }
    }

    /// <summary>
    /// Converts world position to grid position.
    /// </summary>
    private Vector2Int WorldToGrid(float worldX, float worldY)
    {
        int x = Mathf.FloorToInt((worldX - xStart) / tileSize);
        int y = Mathf.FloorToInt((yStart - worldY) / tileSize);
        x = Mathf.Clamp(x, 0, gridWidth - 1);
        y = Mathf.Clamp(y, 0, gridHeight - 1);
        return new Vector2Int(x, y);
    }

    /// <summary>
    /// Modifies an area in the grid around a certain position by either planting or mowing grass.
    /// Depending on the player type, this will either create or destroy grass.
    /// </summary>
    private void AffectArea(Vector2 playerPosition, bool plant)
    {
        // Get the player's position within the grid.
        Vector2Int gridPosition = WorldToGrid(playerPosition.x, playerPosition.y);
        
        // Helper variables for calculating the range of candidate tiles...
        float radius = 0.7F;
        int maxDistance = Mathf.CeilToInt(radius / tileSize);

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
                if ((!plant && grid[x, y] == null) || (plant && grid[x, y] != null))
                {
                    continue;
                }

                // Now we need to calculate the actual distance of the candidate tile to the player,
                // to check if it is really within their radius.
                Vector2 grassPosition = new Vector2(xStart + x * tileSize, yStart - y * tileSize);
                float distance = Vector2.Distance(playerPosition, grassPosition);

                // GUARD: Bail, if the player is too far away.
                if (distance > radius)
                {
                    continue;
                }

                if (plant)
                {
                    // Create a new grass tile if the player is a planter.
                    GameObject grass = instantiateGrass(x, y);
                    grid[x, y] = grass;
                    totalGrassTiles++;
                } else
                {
                    // Remove a pre-existing grass tile if the player is a mower.
                    Destroy(grid[x, y]);
                    grid[x, y] = null;
                    totalGrassTiles--;
                }
            }
        }
    }

    public void Mow(Vector2 playerPosition)
    {
        AffectArea(playerPosition, false);
    }

    public void Plant(Vector2 playerPosition)
    {
        AffectArea(playerPosition, true);
    }

    public float GetGrassPercentage()
    {
        return (float)totalGrassTiles / (gridWidth * gridHeight) * 100F;
    }
}