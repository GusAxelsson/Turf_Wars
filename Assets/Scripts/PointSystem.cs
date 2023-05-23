using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointSystem : MonoBehaviour
{
    public TileManager tilemanager;
    public PixelFontRenderer pointsMowersT;
    public PixelFontRenderer pointsPlantersT;

    public GameObject animatedPoints;
    public PixelFontRenderer multiplyRenderer;

    public AudioSource pointsAudio;
    public int pointsPlanters = 0;
    public int pointsMowers = 0;
    private float startTime;
    private float timer;
    private int pointTimer = 5;
    private float areaPoints = 1000;
    private float grassPercentage;

    private bool spawned = false;

    void Start()
    {
        startTime = Time.time;
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
        updateMultiplyText();
        animatePoints("Player1",3000);
    }
    void updateMultiplyText()
    {
        float cMultiplier = currentMultiplier();
        cMultiplier = Mathf.Round(cMultiplier * 10.0f) * 0.1f;
        multiplyRenderer.SetText(string.Format("x:{0:0}.{1:0}", (int)cMultiplier, (cMultiplier % 1) * 10F));
    }

    float currentMultiplier()
    {
        float gameTime = Time.time - startTime;
        float mult = Mathf.Round((1.0F + ((gameTime / 180.0F) * 9.0F)) * 2);
        return mult / 2;
    }

    // points scale from 1000 per tic in at the start of the game to 10000 at the end of the game
    private int calcPoints()
    {
        float gameTime = Time.time - startTime;
        return Mathf.FloorToInt(areaPoints * currentMultiplier());
    }

    void animatePoints(string target, int points)
    {
        if (Time.time - startTime > 10 && spawned == false)
        {
            GameObject movePoints = Instantiate(animatedPoints, new Vector3(0,7,0), Quaternion.identity);
            movePoints.GetComponent<PixelFontRenderer>().SetText(points.ToString());
            spawned = true;
            Debug.Log("Spawned at: " + movePoints.transform.position);
        }
    }

    // Adds points to the team covering over 50% of the area
    void AreaPoints(){
        if(grassPercentage > 60){
            pointsAudio.Play();
            pointsPlanters += calcPoints();
            pointsPlantersT.SetText(pointsPlanters.ToString());
        }
        else if(grassPercentage < 40){
            pointsAudio.Play();
            pointsMowers += calcPoints();
            pointsMowersT.SetText(pointsMowers.ToString());

        }
    }
}
