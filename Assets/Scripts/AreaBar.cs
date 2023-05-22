using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaBar : MonoBehaviour
{
    public TileManager tilemanager;
    public Slider scoreSlider;

    public Image[] fillImg;

    public Color planterColor = Color.green;
    public Color mowerColor = Color.red;
    public Color flashColor = Color.black;

    [Range(1, 5)]
    public float flickerSpeed = 1;

    void Start(){
        fillImg = GetComponentsInChildren<Image>();
        fillImg[0].color = mowerColor;
        fillImg[1].color = planterColor;
    }

    void Update(){
        // Update the value of the slider
        scoreSlider.value = tilemanager.GetGrassPercentage();

        // Color flickering of the team covering most of the area
        if(tilemanager.GetGrassPercentage() > 60){
            fillImg[1].color = Color.Lerp(planterColor, flashColor, Mathf.PingPong(Time.time * flickerSpeed, 1));
            fillImg[0].color = mowerColor;
        }
        else if(tilemanager.GetGrassPercentage() < 40){
            fillImg[0].color = Color.Lerp(mowerColor, flashColor, Mathf.PingPong(Time.time * flickerSpeed, 1)); 
            fillImg[1].color = planterColor;
        }
        else{
            fillImg[0].color = mowerColor;
            fillImg[1].color = planterColor;
        }
    }
}
