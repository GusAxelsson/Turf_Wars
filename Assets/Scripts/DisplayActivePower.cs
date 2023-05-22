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

    public GameObject plantAbilityInfo;

    public GameObject lawnSpeedBoosterInfo;
    public GameObject lawnInvertInfo;
    public GameObject lawnStunInfo;
    public GameObject lawnRangePowerInfo;

    public GameObject lawnAbilityInfo;

    private float abilityCooldown;
    private float temppp;

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

        plantAbilityInfo = GameObject.Find("PlantAbilityInfo");

        lawnSpeedBoosterInfo = GameObject.Find("LawnSpeedBoosterInfo");
        lawnInvertInfo = GameObject.Find("LawnInvertInfo");
        lawnStunInfo = GameObject.Find("LawnStunInfo");
        lawnRangePowerInfo = GameObject.Find("LawnRangePowerInfo");

        lawnAbilityInfo = GameObject.Find("LawnAbilityInfo");

        

    }

    // Update is called once per frame
    void Update()
    {

        // player 1 Lawn mower booster info
        if (player2.GetComponent<MovementController>().speedPower == true){
            plantSpeedBoosterInfo.SetActive(true);
        } else {
             plantSpeedBoosterInfo.SetActive(false);
        }
        if (player1.GetComponent<MovementController>().invertPower == true){
            lawnInvertInfo.SetActive(true);
        } else {
            lawnInvertInfo.SetActive(false);
        }
        if (player1.GetComponent<MovementController>().stunPower == true){
            lawnStunInfo.SetActive(true);
        } else {
            lawnStunInfo.SetActive(false);
        }
        if (player1.GetComponent<MovementController>().rangePower == true){
            lawnRangePowerInfo.SetActive(true);
        } else {
            lawnRangePowerInfo.SetActive(false);
        }
        if (player1.GetComponent<FlamethrowerCapability>().flameCapability == true ){
            lawnAbilityInfo.GetComponent<Image>().color  = new Color(1f, 1f, 1f);
            lawnAbilityInfo.transform.localScale = new Vector3(0.7f, 1f, 1f);
        } else {
            lawnAbilityInfo.GetComponent<Image>().color  = new Color(0.5f, 0.5f, 0.5f);
            if (player1.GetComponent<FlamethrowerCapability>().cooldown < 15 ){
                lawnAbilityInfo.transform.localScale = new Vector3(0.1f, 0.4f, 0.4f);
            } 
            if (player1.GetComponent<FlamethrowerCapability>().cooldown < 14 ){
                lawnAbilityInfo.transform.localScale = new Vector3(0.2f, 0.5f, 0.5f);
            } 
            if (player1.GetComponent<FlamethrowerCapability>().cooldown < 12 ){
                lawnAbilityInfo.transform.localScale = new Vector3(0.3f, 0.6f, 0.6f);
            } 
            if (player1.GetComponent<FlamethrowerCapability>().cooldown < 10 ){
                lawnAbilityInfo.transform.localScale = new Vector3(0.4f, 0.7f, 0.7f);
            } 
            if (player1.GetComponent<FlamethrowerCapability>().cooldown < 7 ){
                lawnAbilityInfo.transform.localScale = new Vector3(0.5f, 0.8f, 0.8f);
            } 
            if (player1.GetComponent<FlamethrowerCapability>().cooldown < 3 ){
                lawnAbilityInfo.transform.localScale = new Vector3(0.6f, 0.9f, 0.9f);
            }     
        }

        // player 2 Plant booster info
        if (player1.GetComponent<MovementController>().speedPower == true){
            lawnSpeedBoosterInfo.SetActive(true);
        } else {
             lawnSpeedBoosterInfo.SetActive(false);
        }
        if (player2.GetComponent<MovementController>().invertPower == true){
            plantInvertInfo.SetActive(true);
        } else {
            plantInvertInfo.SetActive(false);
        }
        if (player2.GetComponent<MovementController>().stunPower == true){
            plantStunInfo.SetActive(true);
        } else {
            plantStunInfo.SetActive(false);
        }
        if (player2.GetComponent<MovementController>().rangePower == true ){
            plantRangePowerInfo.SetActive(true);
        } else {
            plantRangePowerInfo.SetActive(false);
        }
        if (player2.GetComponent<SuperRootCapability>().rootCapability == true ){
            plantAbilityInfo.GetComponent<Image>().color  = new Color(1f, 1f, 1f);
            plantAbilityInfo.transform.localScale = new Vector3(1f, 1f, 1f);
        } else {
            plantAbilityInfo.GetComponent<Image>().color  = new Color(0.5f, 0.5f, 0.5f);
            if (player2.GetComponent<SuperRootCapability>().cooldown < 15 ){
                plantAbilityInfo.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            } 
            if (player2.GetComponent<SuperRootCapability>().cooldown < 14 ){
                plantAbilityInfo.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            } 
            if (player2.GetComponent<SuperRootCapability>().cooldown < 12 ){
                plantAbilityInfo.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            } 
            if (player2.GetComponent<SuperRootCapability>().cooldown < 10 ){
                plantAbilityInfo.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            } 
            if (player2.GetComponent<SuperRootCapability>().cooldown < 7 ){
                plantAbilityInfo.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            } 
            if (player2.GetComponent<SuperRootCapability>().cooldown < 3 ){
                plantAbilityInfo.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }     
        }

    }
}
