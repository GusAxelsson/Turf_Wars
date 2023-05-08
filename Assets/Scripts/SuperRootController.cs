using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRootController : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    void Update()
    {
        
    }
}
