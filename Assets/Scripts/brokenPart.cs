using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenPart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected!");
        if (collision.gameObject.CompareTag("Cutter"))
        {
            Debug.Log("CUTTER");
        } else {
            Debug.Log("NOT CUTTER");
        }
    }
}
