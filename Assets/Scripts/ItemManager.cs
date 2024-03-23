using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [Header("ladder Variables")]
    public GameObject ladderPrefab;
    public GameObject currentLadder;
    public GameObject placementIndicator;
    public UnityEngine.Camera mainCamera;
    public bool isPlacingItem = false;
    private bool hasLadder = false;
    public Transform playerTransform;
    public ladderExtension ladderExtensionScript;
    public float ladderLength = 1f; // The initial length of the ladder


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = UnityEngine.Camera.main;
        playerTransform = GetComponent<Transform>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

   public void ladderPlacement()
    {
        // Check if the player is placing an item
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPlacingItem)
            {
                isPlacingItem = false;
                Destroy(currentLadder);
                hasLadder = false;
            }
            else
            {
                if (currentLadder == null && !hasLadder)
                {
                    isPlacingItem = true;
                    currentLadder = Instantiate(placementIndicator);
                    hasLadder = true;
                }
            }
        }

        // Check if the player is picking up an item
        if (Input.GetKeyDown(KeyCode.E))
        {  
            
                Destroy(currentLadder);
                hasLadder = false;
        }

        // Enter placement mode
        if (isPlacingItem)
        {   
            // Get mouse and player position
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 playerPosition = playerTransform.position;

            // Check if the mouse is to the left of the player
            if (mousePosition.x < playerPosition.x)
            {
                // Place the ladder to the left
                Vector3 ladderPosition = playerPosition;
                ladderPosition.x -= 1; 
                ladderPosition.y -= 0.5f; 
                currentLadder.transform.position = ladderPosition;
            }
            // Check if the mouse is to the right of the player
            else if (mousePosition.x > playerPosition.x)
            {
            // Place the ladder to the right
                Vector3 ladderPosition = playerPosition;
                ladderPosition.x += 1; 
                ladderPosition.y -= 0.5f; 
                currentLadder.transform.position = ladderPosition;
            }
            // The mouse is directly above or below the player
            else
            {
                // Place the ladder above the player
                Vector3 ladderPosition = playerPosition;
                ladderPosition.y += 1; 
                currentLadder.transform.position = ladderPosition;
            }

            // Place the ladder
            // here we place the ladder if the player clicks the mouse
            // the ladder will be placed at the position of the placement indicator
            // the scale of the ladder will be the same as the placement indicator
            if (Input.GetMouseButtonDown(0))
            {
                isPlacingItem = false;
                hasLadder = false;
                Vector3 ladderPosition = currentLadder.transform.position; // Save the position of the placement indicator

                // Get the scale from a child component of currentLadder
                Transform childTransform = currentLadder.transform.GetChild(0); // Replace 0 with the index of the child
                Vector3 childScale = childTransform.localScale;

                // Set canPlace to false before destroying the currentLadder
                currentLadder.GetComponent<PlacementIndicator>().canPlace = false;
                
                Destroy(currentLadder);
                currentLadder = Instantiate(ladderPrefab, ladderPosition, Quaternion.identity); // Instantiate the ladder at the saved position

                // Apply the scale to a child component of the new ladder
                childTransform = currentLadder.transform.GetChild(0); // Replace 0 with the index of the child
                childTransform.localScale = childScale;
            }
        }
    }
}
