using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGameManager : MonoBehaviour
{
    public GameObject pipeHolder;
    public GameObject startingPipe;
    public GameObject[] pipes;
    public GameObject endPipeGameObject; // Assuming this might be a target pipe for some scenarios
    private MiniGameUnoPipe endPipe;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI cashText;
    public StartMiniGame startMiniGame;
    public int score = 2000;
    private float timeToDisplay = 0;
    private float scoreDecrementTimer = 0f;
    private GameManager gameManager;

    int totalPipes = 0;
    float[] rotations = { 0, 90, 180, 270 };

    IEnumerator Start()
    {
        timerIsRunning = true;
        totalPipes = pipeHolder.transform.childCount;
        endPipe = endPipeGameObject.GetComponent<MiniGameUnoPipe>();
        pipes = new GameObject[totalPipes];
        gameManager = FindObjectOfType<GameManager>();

        // Initialize pipes with random rotations
        for (int i = 0; i < pipes.Length; i++)
        {
            MiniGameUnoPipe pipe = pipeHolder.transform.GetChild(i).GetComponent<MiniGameUnoPipe>();
            int randIdx = Random.Range(0, rotations.Length);
            pipe.transform.eulerAngles = new Vector3(0, 0, rotations[randIdx]);
        }
        yield return null;
        PropagateWater();
    }

    private void Update()
    {
        if (endPipe.hasWater)
        {
            Debug.Log("BINGO");
            timerIsRunning = false; // Stop the timer when the game ends
            gameManager.AddCoins(score);
            gameManager.isInMiniGAme = false;
            startMiniGame.SetIsActive(false);
            endPipe.hasWater = false;
            score = 0;
            timeToDisplay = 0;
            scoreDecrementTimer = 0f;
        }

        if (timerIsRunning)
        {
            timeToDisplay += Time.deltaTime; // Ensure the last second is counted

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            // Update the display text
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            scoreDecrementTimer += Time.deltaTime;
            if (scoreDecrementTimer >= 5f) // Every 5 seconds
            {
                score -= 100; // Decrement score
                cashText.text = "Cash for job: " + score + "$"; // Update the score display
                scoreDecrementTimer = 0f; // Reset the timer
            }
        }
    }

    // Call this method to start the water flow from the starting pipe
    public void PropagateWater()
    {
        Debug.Log("Propagating water");
        // Reset all pipes' water status before starting the propagation
        foreach (Transform child in pipeHolder.transform)
        {
            MiniGameUnoPipe pipe = child.GetComponent<MiniGameUnoPipe>();
            if (pipe != null)
            {
                pipe.hasWater = false; // Reset water status
                // Deactivate 
                pipe.filledPipe.SetActive(false);
            }
        }
        startingPipe.GetComponent<MiniGameUnoPipe>().PropagateWater(true);
    }
}
