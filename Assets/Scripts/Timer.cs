using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public float timer = 181;
    public TextMeshProUGUI timeText;

    public GameOver gameOverScreen;

    // Update is called once per frame
    void Update()
    {
        if(timer > 0){
        timer -= Time.deltaTime;  
        }  
        else{
            timer = 0;
            gameOverScreen.GameIsOver();
        }

        DisplayCountdown(timer);
    }

    void DisplayCountdown(float time){
        if(time < 0){
            time = 0;
        }

        float minutes = Mathf.FloorToInt(time/60);
        float seconds = Mathf.FloorToInt(time%60);
        float milliseconds = time%1*100;

        if(time > 10){
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else{
            timeText.text = string.Format("{0:00}:{1:00}", seconds, milliseconds);
        }

        }
    }

