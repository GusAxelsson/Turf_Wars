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
    public GameObject plantRangePowerInfo;

    public GameObject plantSpeedBoosterText;
    public GameObject plantInvertText;
    public GameObject plantStunText;
    public GameObject plantRangePowerText;

    public GameObject plantAbilityInfo;

    public GameObject lawnSpeedBoosterInfo;
    public GameObject lawnInvertInfo;
    public GameObject lawnStunInfo;
    public GameObject lawnRangePowerInfo;

    public GameObject lawnSpeedBoosterText;
    public GameObject lawnInvertText;
    public GameObject lawnStunText;
    public GameObject lawnRangePowerText;

    public GameObject lawnAbilityInfo;

    private float abilityCooldown;

    // private Vector3 temp;


    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        plantSpeedBoosterInfo = GameObject.Find("PlantSpeedBoosterInfo");
        plantInvertInfo = GameObject.Find("PlantInvertInfo");
        plantStunInfo = GameObject.Find("PlantStunInfo");
        plantRangePowerInfo = GameObject.Find("PlantRangePowerInfo");

        plantSpeedBoosterText = GameObject.Find("PlantSpeedBoosterText");
        plantInvertText = GameObject.Find("PlantInvertText");
        plantStunText = GameObject.Find("PlantStunText");
        plantRangePowerText = GameObject.Find("PlantRangePowerText");

        plantAbilityInfo = GameObject.Find("PlantAbilityInfo");

        lawnSpeedBoosterInfo = GameObject.Find("LawnSpeedBoosterInfo");
        lawnInvertInfo = GameObject.Find("LawnInvertInfo");
        lawnStunInfo = GameObject.Find("LawnStunInfo");
        lawnRangePowerInfo = GameObject.Find("LawnRangePowerInfo");

        lawnSpeedBoosterText = GameObject.Find("LawnSpeedBoosterText");
        lawnInvertText = GameObject.Find("LawnInvertText");
        lawnStunText = GameObject.Find("LawnStunText");
        lawnRangePowerText = GameObject.Find("LawnRangePowerText");

        lawnAbilityInfo = GameObject.Find("LawnAbilityInfo");

        

    }

    // Update is called once per frame
    void Update()
    {

        // player 1 Lawn mower booster info
        if (player2.GetComponent<MovementController>().speedPower == true){
            plantSpeedBoosterInfo.SetActive(true);
            plantSpeedBoosterText.SetActive(true);
        } else {
             plantSpeedBoosterInfo.SetActive(false);
             plantSpeedBoosterText.SetActive(false);
        }
        if (player1.GetComponent<MovementController>().invertPower == true){
            lawnInvertInfo.SetActive(true);
            lawnInvertText.SetActive(true);
        } else {
            lawnInvertInfo.SetActive(false);
            lawnInvertText.SetActive(false);
        }
        if (player1.GetComponent<MovementController>().stunPower == true){
            lawnStunInfo.SetActive(true);
            lawnStunText.SetActive(true);
        } else {
            lawnStunInfo.SetActive(false);
            lawnStunText.SetActive(false);
        }
        if (player1.GetComponent<MovementController>().rangePower == true){
            lawnRangePowerInfo.SetActive(true);
            lawnRangePowerText.SetActive(true);
        } else {
            lawnRangePowerInfo.SetActive(false);
            lawnRangePowerText.SetActive(false);
        }
        if (player1.GetComponent<FlamethrowerCapability>().flameCapability == true ){
            lawnAbilityInfo.SetActive(true);
        } else {
            lawnAbilityInfo.SetActive(false);
            // lawnAbilityInfo.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            // temp = lawnAbilityInfo.transform.localScale;
            // temp.x += Time.deltaTime;
            // lawnAbilityInfo.transform.localScale = temp;
        }

        // player 2 Plant booster info
        if (player1.GetComponent<MovementController>().speedPower == true){
            lawnSpeedBoosterInfo.SetActive(true);
            lawnSpeedBoosterText.SetActive(true);
        } else {
             lawnSpeedBoosterInfo.SetActive(false);
            lawnSpeedBoosterText.SetActive(false);
        }
        if (player2.GetComponent<MovementController>().invertPower == true){
            plantInvertInfo.SetActive(true);
            plantInvertText.SetActive(true);
        } else {
            plantInvertInfo.SetActive(false);
            plantInvertText.SetActive(false);
        }
        if (player2.GetComponent<MovementController>().stunPower == true){
            plantStunInfo.SetActive(true);
            plantStunText.SetActive(true);
        } else {
            plantStunInfo.SetActive(false);
            plantStunText.SetActive(false);
        }
        if (player2.GetComponent<MovementController>().rangePower == true ){
            plantRangePowerInfo.SetActive(true);
            plantRangePowerText.SetActive(true);
        } else {
            plantRangePowerInfo.SetActive(false);
            plantRangePowerText.SetActive(false);
        }
        if (player2.GetComponent<SuperRootCapability>().rootCapability == true ){
            // plantAbilityInfo.SetActive(true);
            plantAbilityInfo.transform.localScale = new Vector3(1f, 1f, 1f);
        } else {
            // plantAbilityInfo.SetActive(false);
            plantAbilityInfo.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

    }
}
