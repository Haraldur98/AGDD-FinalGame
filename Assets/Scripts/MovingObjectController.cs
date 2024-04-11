using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{
    public float speed; // Speed of the movement
    private bool movingRight = true; // Direction of movement
    public bool stopped = false;
    private bool isInBounds = false; // Flag to check if in bounds
    public float leftBound = -5.0f; // Left boundary of the movement
    public float rightBound = 5.0f; // Right boundary of the movement
    private Vector3 startPosition; // Store the start position
    public Camera mainCamera;
    private MiniGame2Manager gameManager;
    private GameObject currentBoundary; // Store the current collided boundary object
    public bool fixing = false;

    void Awake()
    {
        startPosition = transform.position;
        gameManager = FindObjectOfType<MiniGame2Manager>();
        InitializeMovement();
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

    public float adjustSpeed() {
        switch (gameManager.difficulty) {
            case 1:
                return 5.0f;
            case 2:
                return 10.0f;
            case 3:
                return 15.0f;
            default:
                return 5.0f;
        }
    }

    public void InitializeMovement()
    {
        stopped = false; // Ensure the object is set to move
        speed = adjustSpeed(); // Set initial speed
        movingRight = true; // Set initial direction
        leftBound = - 5.0f;
        rightBound = 5.0f;
        isInBounds = false; // Reset the flag
        // Any other initialization code specific to starting movement
    }

    public void addWelding(GameObject currentBoundary) {
        Debug.Log(currentBoundary.gameObject.name);
        if (currentBoundary.transform.parent.gameObject.name == "leftBoundary")
        {
            // Get object called leftWelded
            Debug.Log("Left boundary");
            GameObject leftWelded = currentBoundary.transform.parent.transform.parent.transform.parent.Find("leftWelded").gameObject; // Find the leftWelded object
            leftWelded.SetActive(true);
        } else if (currentBoundary.transform.parent.gameObject.name == "rightBoundary")
        {
            Debug.Log("Right boundary");
            // Get object called rightWelded
            GameObject rightWelded = currentBoundary.transform.parent.transform.parent.transform.parent.Find("rightWelded").gameObject; // Find the rightWelded object
            rightWelded.SetActive(true);
        }
    }


    private void ToggleMovement()
    {
        stopped = !stopped;
        speed = stopped ? 0 : 5.0f;
        if (stopped && isInBounds && currentBoundary != null)
        {
            if (fixing) addWelding(currentBoundary);
            currentBoundary.transform.parent.gameObject.SetActive(false);
            gameManager.BoundaryDestroyed(); // Notify the GameManager
            Destroy(gameObject); 
            gameManager.SpawnNewMovable(startPosition);
        } else { // Does not hit the boundary
            Destroy(gameObject); 
            mainCamera.GetComponent<ScreenShake>().isShaking = true;
            gameManager.SpawnNewMovable(startPosition);
            gameManager.decrementScore();
        }
        // } else if (stopped && isInBounds && currentBoundary == null && fixing)
        // {
        //     foreach (Transform child in currentBoundary.transform)
        //     {
        //         if (child.gameObject.name == "base") continue;
        //         Destroy(child.gameObject);
        //     }
            
        //     currentBoundary.GetComponent<BoxCollider2D>().enabled = false;
        //     gameManager.BoundaryDestroyed();
        //     Destroy(gameObject); 
        //     gameManager.SpawnNewMovable(startPosition);

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
