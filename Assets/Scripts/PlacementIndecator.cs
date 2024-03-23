using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    public bool canPlace = true; // This variable will be true if the ladder can be placed and false otherwise
    private SpriteRenderer spriteRenderer; // The SpriteRenderer component
     private void Start()
    {
        // Get the SpriteRenderer and Rigidbody2D components
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Change the color of the sprite based on canPlace
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Change the color of all sprites based on canPlace
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            if (canPlace)
            {
                sr.color = Color.green;
            }
            else
            {
                sr.color = Color.red;
            }
        }
    }

    // private void onTriggerEnter2D(Collider2D other)
    // {
    //     if (!other.gameObject.CompareTag("Steps"))
    //     {
    //         canPlace = false;
    //     }
    // }
    
    // // OnTriggerStay2D is called once per frame for every Collider2D other that is touching the trigger
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (!other.gameObject.CompareTag("Steps"))
    //     {
    //         canPlace = false;
    //     }
    // }

    // // OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (!other.gameObject.CompareTag("Steps"))
    //     {
    //         canPlace = false;
    //     }
    // }

}