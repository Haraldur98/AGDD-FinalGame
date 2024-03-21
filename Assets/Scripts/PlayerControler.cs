using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    public float speed = 5.0f;
    public float runSpeed = 7.5f;
    public float jumpForce = 5.0f;
    private bool isJumping = false;
    private float climbSpeed = 5.0f;
    private Rigidbody2D rb;

    [Header("Actions")]
    public bool isPlacingItem = false; 
    public bool isClimping = false;


    [Header("Game Objects / Items")]
    public GameObject ladderPrefab;
    private GameObject currentLadder;


    public UnityEngine.Camera mainCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // handle player movement
        HandleMovement();

        // handle player sprite state
        HandleSpriteState();

         // handle ladder placement
        ladderPlacement();

    }

    // This function is called when the player collides with a platform
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("Steps"))
        {
            isClimping = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        
        if (collision.gameObject.CompareTag("Steps"))
        {   
            isClimping = false; 
        }
        
    }

    private void HandleMovement()
    {
        // player movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Check if the player is running
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            moveHorizontal *= runSpeed;
        }
        else
        {
            moveHorizontal *= speed;
        }

        // Check if the player is jumping
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

        if (isClimping && currentLadder != null)
        {
            float verticalInput = Input.GetAxis("Horizontal");
            Vector2 climbDirection = (currentLadder.transform.position - transform.position).normalized;
            Vector2 climbVelocity = climbDirection * verticalInput * climbSpeed;
            rb.velocity = climbVelocity;
        }

        rb.velocity = new Vector2(moveHorizontal, rb.velocity.y);
    }

    private void HandleSpriteState()
    {
        // Check if the player is moving
        if (rb.velocity.x > 0)
        {
            // Player is moving right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            // Player is moving left
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void ladderPlacement()
    {   

        // Check if the player is placing an item
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPlacingItem)
            {
                isPlacingItem = false;
                Destroy(currentLadder);
            }
            else
            {
                if (currentLadder == null)
                {   
                    isPlacingItem = true;
                    currentLadder = Instantiate(ladderPrefab);
                }
            }
        }

        // Enter placement modee
        if (isPlacingItem)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 playerPosition = transform.position;

            // Check if the mouse is to the left of the player
            if (mousePosition.x < playerPosition.x)
            {
                // Place the ladder to the left
                Vector3 ladderPosition = playerPosition;
                ladderPosition.x -= 2; // Adjust this value as needed
                ladderPosition.y -= 0.5f; // Adjust this value as needed
                currentLadder.transform.position = ladderPosition;
            }
            // Check if the mouse is to the right of the player
            else if (mousePosition.x > playerPosition.x)
            {
                // Place the ladder to the right
                Vector3 ladderPosition = playerPosition;
                ladderPosition.x += 2; // Adjust this value as needed
                ladderPosition.y -= 0.5f; // Adjust this value as needed
                currentLadder.transform.position = ladderPosition;
            }
            // The mouse is directly above or below the player
            else
            {
                // Place the ladder above the player
                Vector3 ladderPosition = playerPosition;
                ladderPosition.y += 1; // Adjust this value as needed
                currentLadder.transform.position = ladderPosition;
            }

            // Place the ladder
            if (Input.GetMouseButtonDown(0))
            {
                isPlacingItem = false;
            }
        }
    }

}