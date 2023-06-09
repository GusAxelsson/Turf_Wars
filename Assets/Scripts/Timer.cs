using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public float timer = 181;
    public PixelFontRenderer timeText;
    public VolcanoEvent volcano;

    public PointSystem pointsystem;

    private bool triggeredVolcano = false;

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (!triggeredVolcano && timer < 65.0F)
            {
                triggeredVolcano = true;
                volcano.activate();
            }
        }
        else
        {
            timer = 0;
            if (pointsystem.pointsMowers > pointsystem.pointsPlanters)
            {
                SceneManager.LoadScene("GameOver_MowerWin");
            }
            else
            {
                SceneManager.LoadScene("GameOver_PlanterWin");
            }
        }

        DisplayCountdown(timer);
    }

    void DisplayCountdown(float time)
    {
        if (time < 0)
        {
            time = 0;
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float milliseconds = time % 1 * 100;

        if (time > 30)
        {
            timeText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
        else
        {
            timeText.SetText(string.Format("{0:00}:{1:00}", seconds, milliseconds));
        }
    }
}

