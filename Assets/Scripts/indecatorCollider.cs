using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indecatorCollider : MonoBehaviour
{
    public PlacementIndicator parentScript;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("trigger"))
        {
            return;
        }

        Debug.Log("Collided with " + other.gameObject.name);

        if (!other.gameObject.CompareTag("Steps"))
        {
            parentScript.canPlace = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("trigger"))
        {
            return;
        }

        if (!other.gameObject.CompareTag("Steps"))
        {
            parentScript.canPlace = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("trigger"))
        {
            return;
        }

        if (!other.gameObject.CompareTag("Steps"))
        {
            parentScript.canPlace = true;
        }
    }
}