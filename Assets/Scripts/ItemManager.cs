using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [Header("ladder Prefabs")]
    public GameObject ladderPrefab;
    public GameObject placementIndicator;
    public GameObject currentLadder;

    [Header("ladder Variables")]
    public bool hasLadder = false;
    public UnityEngine.Camera mainCamera; 
    public bool isPlacingItem = false;
    public bool isLadderPlaced = false;
    public float ladderLength = 1f; // The initial length of the ladder
    public float groundDistance = 10f; 
    public float lastHitpont_y = 0;
    
    [Header("Player Variables")]
    private Transform playerTransform;
    public PickUpRadius pickUpRadiusScript;
    
    [Header("UI Manager to show the indicator")]
    public UImanager uImanagerScript;
    
    [Header("Ground Detection Layer")]
    public LayerMask groundLayer; // Layer of the ground
    // public ladderExtension ladderExtensionScript;
 

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = UnityEngine.Camera.main;
        playerTransform = GetComponent<Transform>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void retriveLadder()
    {
        if (isLadderPlaced == true && pickUpRadiusScript.canPickUp == false)
        {
            Destroy(currentLadder);
            hasLadder = false;
            isLadderPlaced = false;
            int cash = PlayerPrefs.GetInt("Score", 0);
            cash -= 1000;
            PlayerPrefs.SetInt("Score", cash);
        }
    }

   public void ladderPlacement()
    {
        
        // Check if the player is picking up an item
        if (isLadderPlaced == true && pickUpRadiusScript.canPickUp == true)
        {   

            if (Input.GetMouseButtonDown(1)) 
            {
                Destroy(currentLadder);
                hasLadder = false;
                isLadderPlaced = false;
            }
        }
        
        // Check if the player is placing an item
        if (Input.GetMouseButtonDown(1))
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


        // Enter placement mode
        if (isPlacingItem)
        {   
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 playerPosition = playerTransform.position;
            Vector3 ladderPosition = playerPosition;


            // Check if the mouse is to the left of the player
            if (mousePosition.x < playerPosition.x)
            {
                ladderPosition.x -= 1; 
            }
            // Check if the mouse is to the right of the player
            else if (mousePosition.x > playerPosition.x)
            {
                // Place the ladder to the right
                ladderPosition.x += 1; 
            }

            // Position the ladder on the ground
            RaycastHit2D hit = Physics2D.Raycast(ladderPosition, Vector2.down, groundDistance, groundLayer);
            if (hit.collider != null)
            {
                lastHitpont_y = hit.point.y;
            }

            ladderPosition.y = lastHitpont_y + 0.15f;
            // ladderPosition.y = -5;
            currentLadder.transform.position = ladderPosition;
            
            // Place the ladder
            // here we place the ladder if the player clicks the mouse
            // the ladder will be placed at the position of the placement indicator
            // the scale of the ladder will be the same as the placement indicator
            if (Input.GetMouseButtonDown(0) && currentLadder.GetComponent<PlacementIndicator>().canPlace)
            {
                isPlacingItem = false;
                hasLadder = false;
                Vector3 newladderPosition = currentLadder.transform.position; // Save the position of the placement indicator

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

                isLadderPlaced = true;
            }
        }
    }
}
