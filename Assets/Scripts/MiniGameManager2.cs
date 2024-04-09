using System.Collections;
using TMPro;
using UnityEngine;


public class MiniGame2Manager : MonoBehaviour
{
    public GameObject movablePrefab; // Assign in inspector
    public GameObject miniGame;
    public GameObject mainPart; // Assign in inspector
    public GameObject fixedPartPrefab; // Assign in inspector
    private int boundariesDestroyed = 0; // Set to your movable object's start position
    public TextMeshProUGUI scoreText;
    public GameObject tutorialPanel;
    public int score = 2000;

    // Call this when you need to spawn a new movable object

    void Update()
    {
        // when enter is pressed, close the tutorial panel
        if (Input.GetKeyDown(KeyCode.Return))
        {
            tutorialPanel.SetActive(false);
            miniGame.SetActive(true);
            scoreText.gameObject.SetActive(true);
            movablePrefab.GetComponent<MovingObjectController>().stopped = false;
            movablePrefab.GetComponent<MovingObjectController>().speed = 10.0f;
        }
    }
    public void SpawnNewMovable(Vector3 startPosition)
    {
        // add z position to the start position
        if (movablePrefab != null)
        {
            startPosition.z = -1;
            GameObject newMovable = Instantiate(movablePrefab, startPosition, Quaternion.identity);
            newMovable.GetComponent<MovingObjectController>().enabled = true; // Enable the script
            newMovable.GetComponent<Collider2D>().enabled = true;
            newMovable.GetComponent<MovingObjectController>().InitializeMovement();
            movablePrefab = newMovable;
        }
    }

    // Call this when a boundary is destroyed
    public void BoundaryDestroyed()
    {
        boundariesDestroyed++;
        if (boundariesDestroyed >= 2) // Assuming there are only two boundaries
        {
            // Make the main part fall or change state
            StartCoroutine(MakeMainPartFall());
            movablePrefab = null;
        }
    }

    public void decrementScore()
    {
        score -= 100;
        scoreText.text = "Cash for job: " + score + "$";
    }

    IEnumerator MakeMainPartFall()
    {
        float duration = 3f; // Duration in seconds the part should fall
        float elapsed = 0f; // Time elapsed since the start of the fall
        Vector2 repairPosition = mainPart.transform.position; // Store the repair position

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
        Debug.Log("Main part destroyed");
        fixedPartPrefab.GetComponent<FloatToPosition>().InitializeMovement();
    }
}