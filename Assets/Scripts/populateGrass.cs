using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populateGrass : MonoBehaviour
{
    public float xStart, yStart;
    public int columnLenght, rowLenght;
    public float xSpace, ySpace;
    public GameObject grass;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < columnLenght*rowLenght; i++)
        {
            Instantiate(grass, new Vector3(xStart + xSpace * (i % columnLenght),yStart + -ySpace * (i / columnLenght)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
