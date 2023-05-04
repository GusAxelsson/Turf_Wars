using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public AreaBar areaBar;

    public void GameIsOver()
    {
        gameObject.SetActive(true);
        winnerText.text = areaBar.finalScore(); 
    }
}
