using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    public float health = 100.0f;

    [Header("Movement Variables")]
    public float speed = 5.0f;
    public float runSpeed = 7.5f;
    public float jumpForce = 7.0f;
    public float climbSpeed = 5.0f;

    [Header("Actions")]
    public bool isPlacingItem = false;
    public bool isClimbing = false;
    public bool isOnStairs = false;
    public bool isClimbingStairs = false;

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public GameObject spriteParent;

    [Header("Ground Detection")]
    public DetectGround detectGround;

    [Header("Item Manager")]
    public ItemManager itemManager;
    public Vector2 ladderDirection;

    public UImanager uImanagerScript;

    private Rigidbody2D rb;

    [Header("Animation")]
    public Animator animator;

     public enum PlayerState
    {
        Idle,
        Walking,
        Running,
        ClimbingLadder,
        ClimbingLadderIdle,
        PlacingItem
    }

      public PlayerState currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        detectGround = GetComponentInChildren<DetectGround>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        handleAnimations();
        stateHandler();
        HandleInput();
        HandleMovement();
        HandleSpriteState();
        itemManager.ladderPlacement();
        isPlacingItem = itemManager.isPlacingItem;
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (detectGround.isGrounded || isClimbing || isClimbingStairs)
            {
                Jump();
            }

            if (isOnStairs)
            {
                isClimbingStairs = false;
            }
        }

        if (isOnStairs && Input.GetKey(KeyCode.W))
        {
            isClimbingStairs = true;
        }
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed);
        rb.velocity = new Vector2(moveHorizontal, rb.velocity.y);

        if (isClimbing && itemManager.currentLadder != null)
        {
            ClimbLadder();
        }
        else if (isClimbingStairs)
        {
            ClimbStairs();
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    private void ClimbLadder()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Transform stepsTransform = itemManager.currentLadder.transform.Find("Steps");
        Vector2 ladderDirection = stepsTransform.up;
        Vector3 climbVelocity = ladderDirection * verticalInput * climbSpeed * Time.deltaTime;
        transform.position += climbVelocity;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        // Align the player's up vector with the ladder's up vector
        transform.up = stepsTransform.up;
    }

    private void ClimbStairs()
    {
        float verticalInput = Input.GetAxis("Vertical");
        rb.gravityScale = 0;
        rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
    }

    private void Jump()
    {   
        rb.gravityScale = 1;
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isClimbing = false;
    }

    private void HandleSpriteState()
    {
        if (rb.velocity.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), 1, 1);
        }
    }

    public void SwitchSpriteState(string stateName)
    {
        foreach (Transform child in spriteParent.transform)
        {
            child.gameObject.SetActive(false);
        }

        GameObject stateObject = spriteParent.transform.Find(stateName)?.gameObject;
        if (stateObject != null)
        {
            stateObject.SetActive(true);
        }
    }

    public void handleAnimations()
    {
         // Update the current state based on the player's actions
        if (isPlacingItem)
        {
            currentState = PlayerState.PlacingItem;
        }
        else if (isClimbing || isClimbingStairs)
        {
            currentState = PlayerState.ClimbingLadderIdle;
            if (Input.GetKey(KeyCode.W))
            {
                currentState = PlayerState.ClimbingLadder;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > speed)
        {
            currentState = PlayerState.Running;
        }
        else if (Mathf.Abs(rb.velocity.x) > 0)
        {
            currentState = PlayerState.Walking;
        }
        else
        {
            currentState = PlayerState.Idle;
        }
    }

    public void stateHandler()
    {   
        if (currentState == PlayerState.Walking)
        {
            animator.SetBool("walk", true);
        }
        else 
        {
            animator.SetBool("walk", false);

        }

        if (currentState == PlayerState.Running)
        {
            animator.SetBool("run", true);
        }
        else 
        {
            animator.SetBool("run", false);
        }

        if (currentState == PlayerState.PlacingItem)
        {
            animator.SetBool("place", true);
        }
        else 
        {
            animator.SetBool("place", false);
        }

        if (currentState == PlayerState.ClimbingLadder)
        {
            animator.SetBool("climb", true);
        }
        else 
        {
            animator.SetBool("climb", false);
        }
        if (currentState == PlayerState.ClimbingLadderIdle)
        {
            animator.SetBool("climbIdle", true);
        }
        else 
        {
            animator.SetBool("climbIdle", false);
        }
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.CompareTag("Steps"))
        {
            isClimbing = true;
        }
        else 
        {
            transform.up = Vector2.up;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Steps"))
        {
            isClimbing = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {   

        if (other.gameObject.CompareTag("Stairs"))
        {   
            isOnStairs = true;
            uImanagerScript.ShowIndicator(UImanager.ActionState.Climb);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag("Stairs"))
        {
            isOnStairs = false;
            isClimbingStairs = false;
            uImanagerScript.ShowIndicator(UImanager.ActionState.None);
        }
    }


    IEnumerator ResetUpVector()
    {
        // Wait for 2 frames
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        // Reset the player's up vector
        transform.up = Vector2.up;
    }
}