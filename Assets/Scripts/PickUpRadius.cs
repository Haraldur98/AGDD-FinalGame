using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRadius : MonoBehaviour
{
    public bool canPickUp = false;
    public bool canEnter = false;

    public UImanager uImanagerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        Debug.Log("Collided with " + other.gameObject.name);
        if (other.CompareTag("Steps"))
        {
            canPickUp = true;
            uImanagerScript.ShowIndicator(UImanager.ActionState.PickUp);
        }
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     Debug.Log("Collided with " + other.gameObject.name);
        
    //     if (other.CompareTag("Steps"))
    //     {
    //         canPickUp = true;
    //     }
    // }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);

        if (other.CompareTag("Steps"))
        {
            canPickUp = false;
            uImanagerScript.ShowIndicator(UImanager.ActionState.None);
        }
    }
}
