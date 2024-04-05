using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Var iables")]
    public float health = 100.0f;

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

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer; 
    public GameObject spriteParent;

    [Header("Ground Detection")]
    public DetectGround detectGround;

    [Header("Item Manager")]
    public ItemManager itemManager;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        detectGround = GetComponentInChildren<DetectGround>();
    }


    // Update is called once per frame
   void Update()
    {
        // handle player movement
        HandleMovement();

        // handle player sprite state
        HandleSpriteState();

        // handle ladder placement
        itemManager.ladderPlacement();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
    
        // handle player running 
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            moveHorizontal *= runSpeed;
        }
        else
        {
            moveHorizontal *= speed;
        }

        // Hanle player jumping
        if (Input.GetButtonDown("Jump") && detectGround.isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

        // Handle player climbing 
        if (isClimping && itemManager.currentLadder != null)
        {
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(0, verticalInput * climbSpeed);
            rb.velocity = climbVelocity;
            rb.gravityScale = 0; // Disable gravity
        }
        else
        {
            rb.gravityScale = 1; // Enable gravity
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

    public void SwitchSpriteState(string stateName)
    {
        // Disable all child objects
        for (int i = 0; i < spriteParent.transform.childCount; i++)
        {
            spriteParent.transform.GetChild(i).gameObject.SetActive(false);
        }

        // Enable the object that matches the state name
        GameObject stateObject = spriteParent.transform.Find(stateName).gameObject;
        if (stateObject != null)
        {
            stateObject.SetActive(true);
        }
    }
}