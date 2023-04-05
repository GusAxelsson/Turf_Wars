using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassDisplay : MonoBehaviour
{
    public GameObject grass;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            Destroy(gameObject);
            Debug.Log("collision");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
