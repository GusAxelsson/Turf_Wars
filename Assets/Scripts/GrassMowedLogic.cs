using UnityEngine;

public class GrassMowedLogic : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player1"))
        {
            Destroy(gameObject);
            Debug.Log("collision");
        }
    }
}
