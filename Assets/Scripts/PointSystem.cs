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

    // Variables related to moving the points from center of screen to corresponding player
    private GameObject pointsToMove;
    private bool movingPoints = false;
    private string moveTarget;
    private int MoveValue;
    private Vector3 player1PointsDestination = new(11.5F, 7F, 0); // done like this to avoid changing the worldMap file
    private Vector3 player2PointsDestination = new(-11.5F, 7F, 0);

    void Start()
    {
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        MovePoints();
    }
    void Update()
    {
        grassPercentage = tilemanager.GetGrassPercentage();
        timer += Time.deltaTime;

        // Reset timer when one team looses their 60% area control
        if(grassPercentage < 60 && grassPercentage > 40){
            timer = 0;
        }

        // Give points to team covering >50% for 5s
        if (timer >= pointTimer ) {
            AreaPoints();
            timer = 0;
        }
        updateMultiplyText();
        // MovePoints();
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

    /// <summary>
    /// Instantiates the text rendered component. Assigns target and sets "moving points" to true
    /// </summary>
    void SpawnPoints(string target, int points)
    {
        pointsToMove = Instantiate(animatedPoints, new Vector3(0,7,0), Quaternion.identity);
        pointsToMove.GetComponent<PixelFontRenderer>().SetText(points.ToString());
        pointsToMove.GetComponent<PixelFontRenderer>().scale = 2;
        movingPoints = true;
        moveTarget = target;
    }

    /// <summary>
    /// Moves points step by step towards target players score
    /// When they reach the position the element is destroyed and points incremented
    /// </summary>
    void MovePoints()
    {
        if (movingPoints)
        {
            if (moveTarget == "Player1")
            {
                if (Mathf.Abs(player1PointsDestination.x - pointsToMove.transform.position.x) < 0.5F)
                {
                    Destroy(pointsToMove);
                    movingPoints = false;
                    pointsMowers += MoveValue;
                    pointsMowersT.SetText(pointsMowers.ToString());
                    return;
                }
                pointsToMove.transform.position = Vector3.MoveTowards(pointsToMove.transform.position, player1PointsDestination, 11F * Time.fixedDeltaTime);
                Debug.Log("moved to: " + pointsToMove.transform.position);
            }
            else
            {
                if (Mathf.Abs(player2PointsDestination.x - pointsToMove.transform.position.x) < 0.5F)
                {
                    Destroy(pointsToMove);
                    movingPoints = false;
                    pointsPlanters += MoveValue;
                    pointsPlantersT.SetText(pointsPlanters.ToString());
                    return;
                }
                pointsToMove.transform.position = Vector3.MoveTowards(pointsToMove.transform.position, player2PointsDestination, 11F * Time.fixedDeltaTime);
                Debug.Log("moved to: " + pointsToMove.transform.position);
            }
        }
    }

    // Adds points to the team covering over 50% of the area
    void AreaPoints(){
        if(grassPercentage > 60){
            pointsAudio.Play();
            int pointsToAdd = calcPoints();
            MoveValue = pointsToAdd;
            SpawnPoints("Player2", pointsToAdd);
        }
        else if(grassPercentage < 40){
            pointsAudio.Play();
            int pointsToAdd = calcPoints();
            MoveValue = pointsToAdd;
            SpawnPoints("Player1", pointsToAdd);

        }
    }
}
