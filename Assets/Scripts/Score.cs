using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TileManager tilemanager;
    public Slider scoreSlider;

    void Update(){
       scoreSlider.value = tilemanager.GetGrassPercentage();
    }

    public string finalScore(){
        if(tilemanager.GetGrassPercentage() >= 50){
            return "PLANTERS WON";}
        else{
            return "MOWERS WON";
        }
    }
}
