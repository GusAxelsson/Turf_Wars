using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameIsPaused =! gameIsPaused;
            PauseGame();
        }

        void PauseGame(){
            if(gameIsPaused){
                Time.timeScale = 0f;
            }else{
                Time.timeScale = 1;
            }
        }
    }
}
