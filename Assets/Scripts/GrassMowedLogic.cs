using UnityEngine;

public class GrassMowedLogic : MonoBehaviour
{
    private int show;
    public AudioClip cutGrass;
    public AudioClip plantGrass;
    public AudioSource globalEffectPlayer;
    private void Start()
    {
        // Find global effect audiosource
        globalEffectPlayer = GameObject.Find("GlobalEffectPlayer").GetComponent<AudioSource>();

        // decide if the grass tile should be initilized as grown or cut
        show = Random.Range(0, 2);
        if (show == 1)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the we are player 1 we check if the tile is rendered if so we disable it. Reverse is true for player 2
        if(collision.CompareTag("Player1"))
        {   
            if(gameObject.GetComponent<Renderer>().enabled == true)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                globalEffectPlayer.PlayOneShot(cutGrass);
            }
        }
        if (collision.CompareTag("Player2"))
        {
            if (gameObject.GetComponent<Renderer>().enabled == false)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                globalEffectPlayer.PlayOneShot(plantGrass);
            }
        }
    }
}
