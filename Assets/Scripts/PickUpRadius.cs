using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRadius : MonoBehaviour
{
    public bool canPickUp = false;
    public bool canEnter = false;
    // Start is called before the first frame update
    public float detectionRadius = 100f; // Adjust this value as needed
    void Start()
    {
        
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {   
        Debug.Log("Collided with " + other.gameObject.name);
        if (other.CompareTag("Steps"))
        {
            canPickUp = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        
        if (other.CompareTag("Steps"))
        {
            canPickUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collided with " + other.gameObject.name);

        if (other.CompareTag("Steps"))
        {
            canPickUp = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        
        if (other.gameObject.CompareTag("Steps"))
        {
            canPickUp = true;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        Debug.Log("Collided with " + other.gameObject.name);

        if (other.gameObject.CompareTag("Steps"))
        {
            canPickUp = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("Collided with " + other.gameObject.name);

        if (other.gameObject.CompareTag("Steps"))
        {
            canPickUp = false;
        }
    }
}
