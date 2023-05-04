using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DisplayActivePower : MonoBehaviour
{
    public GameObject player1; //lawn
    public GameObject player2; //plant

    public GameObject plantSpeedBoosterInfo;
    public GameObject plantInvertInfo;
    public GameObject plantStunInfo;

    public GameObject lawnSpeedBoosterInfo;
    public GameObject lawnInvertInfo;
    public GameObject lawnStunInfo;


    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        plantSpeedBoosterInfo = GameObject.Find("PlantSpeedBoosterInfo");
        plantInvertInfo = GameObject.Find("PlantInvertInfo");
        plantStunInfo = GameObject.Find("PlantStunInfo");

        lawnSpeedBoosterInfo = GameObject.Find("LawnSpeedBoosterInfo");
        lawnInvertInfo = GameObject.Find("LawnInvertInfo");
        lawnStunInfo = GameObject.Find("LawnStunInfo");

    }

    // Update is called once per frame
    void Update()
    {

        // player 1 Lawn mower booster info
        if (player2.GetComponent<playerMovement>().speedPower == true){
            plantSpeedBoosterInfo.SetActive(true);
        } else {
             plantSpeedBoosterInfo.SetActive(false);
        }
        if (player1.GetComponent<playerMovement>().invertPower == true){
            lawnInvertInfo.SetActive(true);
        } else {
            lawnInvertInfo.SetActive(false);
        }
        if (player1.GetComponent<playerMovement>().stunPower == true){
            lawnStunInfo.SetActive(true);
        } else {
            lawnStunInfo.SetActive(false);
        }

        // player 2 Plant booster info
        if (player1.GetComponent<playerMovement>().speedPower == true){
            lawnSpeedBoosterInfo.SetActive(true);
        } else {
             lawnSpeedBoosterInfo.SetActive(false);
        }
        if (player2.GetComponent<playerMovement>().invertPower == true){
            plantInvertInfo.SetActive(true);
        } else {
            plantInvertInfo.SetActive(false);
        }
        if (player2.GetComponent<playerMovement>().stunPower == true){
            plantStunInfo.SetActive(true);
        } else {
            plantStunInfo.SetActive(false);
        }

    }
}
