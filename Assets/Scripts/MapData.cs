using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{

    public string mapLayout = "";

    // Start is called before the first frame update
    void Start()
    {
        mapLayout = "0000000000000000000000000000" +
                    "0000111000000000000001110000" +
                    "0000111000000000000001110000" +
                    "0000111000001111000001110000" +
                    "0000000000001111000000000000" +
                    "0000000000001111000000000000" +
                    "0000111000001111000001110000" +
                    "0000111000000000000001110000" +
                    "0000111000000000000001110000" +
                    "0000000000000000000000000000";

    }

}
