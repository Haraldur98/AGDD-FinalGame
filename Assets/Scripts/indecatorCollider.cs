using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indecatorCollider : MonoBehaviour
{
    public PlacementIndicator parentScript;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);

        if (!other.gameObject.CompareTag("Steps"))
        {
            parentScript.canPlace = false;
        }
    }
    
    // OnTriggerStay2D is called once per frame for every Collider2D other that is touching the trigger
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Steps"))
        {
            parentScript.canPlace = false;
        }
    }

    // OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Steps"))
        {
            parentScript.canPlace = true;
        }
    }
}
