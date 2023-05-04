using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointSystem : MonoBehaviour
{
    public TileManager tilemanager;
    public TextMeshProUGUI pointsPlantersText;
    public TextMeshProUGUI pointsMowersText;
    public int pointsPlanters = 0;
    public int pointsMowers = 0;
    private float timer;
    private int pointTimer = 5;
    private int areaPoints = 1000;
    private float grassPercentage;

    void Start()
    {
        pointsPlantersText.text = pointsPlanters.ToString();
    }

    void Update()
    {
        grassPercentage = tilemanager.GetGrassPercentage();
        timer += Time.deltaTime;

        // Reset timer when one team looses their 50% area control
        if(grassPercentage == 50){
            timer = 0;
        }

        // Give points to team covering >50% for 5s
        if (timer >= pointTimer ) {
            AreaPoints();
            timer = 0;
        }
    }

    // Adds points to the team covering over 50% of the area
    void AreaPoints(){
        if(grassPercentage > 50){
            pointsPlanters += areaPoints;
            pointsPlantersText.text = pointsPlanters.ToString();
        }
        else if(grassPercentage < 50){
            pointsMowers += areaPoints;
            pointsMowersText.text = pointsMowers.ToString();
        }
    }
}
