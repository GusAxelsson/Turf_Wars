using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public Score score;

    public void GameIsOver()
    {
        gameObject.SetActive(true);
        winnerText.text = score.finalScore(); 
    }
}
