using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populateGrass : MonoBehaviour
{
    public float xStart, yStart;
    public int columnLenght, rowLenght;
    public float xSpace, ySpace;
    public GameObject grass;
    public GameObject rock;

    // On start populate gameplan with 90% grass tiles and 10% rocks. (This was randomly picked and the layout of the map should be custom :) )
    void Start()
    {
        for (int i = 0; i < columnLenght * rowLenght; i++)
        {
            if (Random.Range(0, 10) < 9)
            {
                Instantiate(grass, new Vector3(xStart + xSpace * (i % columnLenght), yStart + -ySpace * (i / columnLenght)), Quaternion.identity);
            }
            else
            {
                Instantiate(rock, new Vector3(xStart + xSpace * (i % columnLenght), yStart + -ySpace * (i / columnLenght)), Quaternion.identity);
            }
        }
    }
}

