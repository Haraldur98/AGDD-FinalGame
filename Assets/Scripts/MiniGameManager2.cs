using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class MiniGame2Manager : MonoBehaviour
{
    public GameObject movablePrefab; // Assign in inspector
    public GameObject miniGame;
    public GameObject mainPart; // Assign in inspector
    public GameObject fixedPartPrefab; // Assign in inspector
    private int boundariesDestroyed = 0; // Set to your movable object's start position
    public TextMeshProUGUI scoreText;
    public GameObject tutorialPanel;
    public int score;
    public int decrement;
    public GameObject leftBoundary;
    public GameObject rightBoundary;
    private Vector3 initalPosition;
    private bool fixing = false;
    private bool lost = false;
    public int difficulty;
    public Vector3 mainCameraPos;
    public UnityEvent onMiniGameEnd;

    void Start()
    {
        adjustScore();
        scoreText.text = "Cash for job: " + score + "$";
    }

    public void adjustScore()
    {
        switch (difficulty)
        {
            case 0:
                score = 1000;
                decrement = 50;
                break;
            case 1:
                score = 3000;
                decrement = 100;
                break;
            case 2:
                score = 5000;
                decrement = 300;
                break;
            default:
                score = 1000;
                decrement = 50;
                break;
        }
    }


    public void SpawnNewMovable(Vector3 startPosition)
    {
        // add z position to the start position
        if (movablePrefab != null) {
            initalPosition = startPosition;
            startPosition.z = -4;
            GameObject newMovable = Instantiate(movablePrefab, startPosition, Quaternion.identity);
            newMovable.GetComponent<MovingObjectController>().enabled = true; // Enable the script
            newMovable.GetComponent<Collider2D>().enabled = true;
            movablePrefab = newMovable;
            newMovable.GetComponent<MovingObjectController>().InitializeMovement();
            if (fixing) {
                movablePrefab.GetComponent<MovingObjectController>().fixing = true;
            }
        }
    }
    private void Endgame() {
        movablePrefab = null;
        fixing = false;
        // Get score from PlayerPrefs 
        int cash = PlayerPrefs.GetInt("Score", 0);
        // Add the score from the mini game to the total score
        cash += score;
        // Save the player's score
        PlayerPrefs.SetInt("Score", cash);
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.isInMiniGame = false;
        onMiniGameEnd?.Invoke();
    }

    // Call this when a boundary is destroyed
    public void BoundaryDestroyed()
    {
        boundariesDestroyed++;
        if (boundariesDestroyed == 2) // Assuming there are only two boundaries
        {
            // Make the main part fall or change state
            StartCoroutine(MakeMainPartFall());
            fixing = true;
        } else if (boundariesDestroyed == 4 && fixing) {
            Endgame();
        }
    }

    public void decrementScore()
    {
        score -= decrement;
        scoreText.text = "Cash for job: " + score + "$";
    }

    IEnumerator MakeMainPartFall()
    {
        float duration = 3f; // Duration in seconds the part should fall
        float elapsed = 0f; // Time elapsed since the start of the fall

        while (elapsed < duration && mainPart != null)
        {
            // Move the main part down
            mainPart.transform.Translate(0, -10f * Time.deltaTime, 0);
            
            // Increment the elapsed time each frame
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        Destroy(mainPart);
        // Destroy the main part after falling for the duration
        fixedPartPrefab.GetComponent<FloatToPosition>().InitializeMovement();
    }

    public void ReappearBoundary() {
        // get left and right boundary objects
        leftBoundary.SetActive(true);
        rightBoundary.SetActive(true);
        
    }
}