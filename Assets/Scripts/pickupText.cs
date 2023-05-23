using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupText : MonoBehaviour
{
    private MovementController player1;
    private MovementController player2;

    public GameObject speedImage;
    public GameObject invertImage;
    public GameObject rangeImage;
    public GameObject stunImage;

    private List<GameObject> activeText = new List<GameObject>();
    private List<float> timers = new List<float>();

    private bool p1SpeedState = false;
    private bool p1InvertState = false;
    private bool p1RangeState = false;
    private bool p1StunState = false;

    private bool p2SpeedState = false;
    private bool p2InvertState = false;
    private bool p2RangeState = false;
    private bool p2StunState = false;


    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<MovementController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<MovementController>();

        p1SpeedState = player1.speedPower;
        p1InvertState = player1.invertPower;
        p1RangeState = player1.rangePower;
        p1StunState = player1.stunPower;

        p2SpeedState = player2.speedPower; ;
        p2InvertState = player2.invertPower;
        p2RangeState = player2.invertPower;
        p2StunState = player2.stunPower;

}

    void CheckActivatedPowers(MovementController player)
    {
        bool speedState;
        bool invertState;
        bool rangeState;
        bool stunState;
        string playerTag = player.tag;


        if (playerTag == ("Player1"))
        {
            speedState = p1SpeedState;
            invertState = p1InvertState;
            rangeState = p1RangeState;
            stunState = p1StunState;
        }
        else
        {
            speedState = p2SpeedState;
            invertState = p2InvertState;
            rangeState = p2RangeState;
            stunState = p2StunState;
        }
        
        if (speedState == false && player.speedPower == true)
        {
            PlaceText(playerTag, speedImage);
        }
        if (invertState == false && player.invertPower == true)
        {
            PlaceText(playerTag, invertImage);
        }
        if (rangeState == false && player.rangePower == true)
        {
            PlaceText(playerTag, rangeImage);
        }
        if (stunState == false && player.stunPower == true)
        {
            PlaceText(playerTag, stunImage);
        }
    }

    void UpdateStates()
    {
        p1SpeedState = player1.speedPower;
        p1InvertState = player1.invertPower;
        p1RangeState = player1.rangePower;
        p1StunState = player1.stunPower;

        p2SpeedState = player2.speedPower;
        p2InvertState = player2.invertPower;
        p2RangeState = player2.rangePower;
        p2StunState = player2.stunPower;
    }

    void TimeOutText()
    {
        for (int i = 0; i < activeText.Count; i++)
        {
            if(Time.time - timers[i] > 0.6F)
            {
                Destroy(activeText[i]);
                activeText.RemoveAt(i);
                timers.RemoveAt(i);
                // remove at if it does not work
            }
        }
    }

    void PlaceText(string playerTag, GameObject text){
        activeText.Add(Instantiate(text, GameObject.FindGameObjectWithTag(playerTag).transform.position ,Quaternion.identity, this.transform));
        timers.Add(Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        TimeOutText();
        CheckActivatedPowers(player1);
        CheckActivatedPowers(player2);
        UpdateStates();
    }
}
