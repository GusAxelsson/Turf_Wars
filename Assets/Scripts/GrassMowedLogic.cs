using UnityEngine;

public class GrassMowedLogic : MonoBehaviour
{
    private int show;
    private void Start()
    {
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
        if(collision.CompareTag("Player1"))
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            Debug.Log("collision");
        }
        if (collision.CompareTag("Player2"))
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            Debug.Log("collision");
        }
    }
}
