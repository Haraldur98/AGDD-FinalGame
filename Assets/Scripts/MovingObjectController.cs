using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{
    public float speed; // Speed of the movement
    private bool movingRight = true; // Direction of movement
    public bool stopped = true;
    private bool isInBounds = false; // Flag to check if in bounds
    public float leftBound = -5.0f; // Left boundary of the movement
    public float rightBound = 5.0f; // Right boundary of the movement
    private Vector3 startPosition; // Store the start position
    // Get camera
    public Camera mainCamera;
    private MiniGame2Manager gameManager;

    private GameObject currentBoundary; // Store the current collided boundary object

    void Start()
    {
        startPosition = transform.position;
        gameManager = FindObjectOfType<MiniGame2Manager>();
    }

    void Update()
    {
        // Move the object left or right until stopped
        if (transform.position.x >= rightBound)
        {
            movingRight = false;
        }
        else if (transform.position.x <= leftBound)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

        // Stop the object when space is pressed or clicked
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ToggleMovement();
        }
    }

    public void InitializeMovement()
    {
        stopped = false; // Ensure the object is set to move
        speed = 10.0f; // Set initial speed
        movingRight = true; // Set initial direction
        leftBound = - 5.0f;
        rightBound = 5.0f;
        isInBounds = false; // Reset the flag
        // Any other initialization code specific to starting movement
    }


    private void ToggleMovement()
    {
        stopped = !stopped;
        speed = stopped ? 0 : 5.0f;

        if (stopped && isInBounds && currentBoundary != null)
        {
            // Destroy the boundary object and its parent
            // Destroy(currentBoundary.transform.parent.gameObject);
            currentBoundary.transform.parent.gameObject.SetActive(false);
            gameManager.BoundaryDestroyed(); // Notify the GameManager
            Destroy(gameObject); // Destroy this movable object
            gameManager.SpawnNewMovable(startPosition);
        } else {
            
            Destroy(gameObject); // Destroy this movable object
            mainCamera.GetComponent<ScreenShake>().isShaking = true;
            Debug.Log("Spawn new movable object");
            gameManager.SpawnNewMovable(startPosition);
            gameManager.decrementScore();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            isInBounds = true;
            currentBoundary = collision.gameObject; // Store the boundary object reference
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            isInBounds = false;
            currentBoundary = null; // Clear the reference when exiting the boundary
        }
    }
}
