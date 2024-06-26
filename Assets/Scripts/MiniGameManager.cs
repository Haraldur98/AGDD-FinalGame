using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public int score;
    public int decrement;
    private float timeToDisplay;
    private float scoreDecrementTimer = 0f;
    public Slider cashSlider;
    public UnityEvent onMiniGameEnd;

    public Vector3 mainCameraPos;
    int totalPipes = 0;
    float[] rotations = { 0, 90, 180, 270 };
    public int difficulty;
    public AudioSource audioSource;
    public AudioClip pipeTurnClip;
    StartMiniGame startMiniGame;

    IEnumerator Start()
    {
        timerIsRunning = true;
        totalPipes = pipeHolder.transform.childCount;
        endPipe = endPipeGameObject.GetComponent<MiniGameUnoPipe>();
        pipes = new GameObject[totalPipes];
        startMiniGame = GameObject.FindObjectOfType<StartMiniGame>();
        difficulty = startMiniGame.difficulty;
        adjustScore();
        // Initialize pipes with random rotations
        for (int i = 0; i < pipes.Length; i++)
        {
            audioSource.clip = pipeTurnClip;
            audioSource.volume = 0.05f;
            MiniGameUnoPipe pipe = pipeHolder.transform.GetChild(i).GetComponent<MiniGameUnoPipe>();
            pipe.audioSource = audioSource;
            int randIdx = Random.Range(0, rotations.Length);
            pipe.transform.eulerAngles = new Vector3(0, 0, rotations[randIdx]);
        }
        yield return null;
        PropagateWater();

        Transform miniGameHolder = gameObject.transform.parent;
        miniGameHolder.transform.position = new Vector3(mainCameraPos.x - 0.354f, mainCameraPos.y - 0.444f, -1);
        GameObject miniGameCamera = GameObject.Find("MiniGameUNOCamera");
        miniGameCamera.transform.position = mainCameraPos;
    }
    public void adjustScore()
    {
        switch (difficulty)
        {
            case 0:
                score = 100;
                decrement = 5;
                break;
            case 1:
                score = 1000;
                decrement = 20;
                break;
            case 2:
                score = 2000;
                decrement = 50;
                break;
            case 3:
                score = 4000;
                decrement = 100;
                break;
            default:
                score = 1000;
                decrement = 20;
                break;
        }
        cashSlider.maxValue = score / decrement;
    }

    private void Update()
    {
        if (endPipe.hasWater)
        {
            EndGame();
        }

        if (timerIsRunning)
        {
            // Ensure the last second is counted
            // decrement score every second:
            scoreDecrementTimer += Time.deltaTime;
            timeToDisplay += Time.deltaTime;
            cashSlider.value = timeToDisplay;
            if (scoreDecrementTimer >= 1)
            {
                score -= decrement;
                cashText.text = "<color=green>$</color>:" + score;
                scoreDecrementTimer = 0;
            }


        }
    }
    private void EndGame()
    {
        timerIsRunning = false; // Stop the timer when the game ends
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.isInMiniGame = false;
        // Get score from PlayerPrefs 
        int cash = PlayerPrefs.GetInt("Score", 0);
        // Add the score from the mini game to the total score
        cash += score;
        // Save the player's score
        PlayerPrefs.SetInt("Score", cash);
        startMiniGame.onMiniGameEnd?.Invoke();
    }

    // Call this method to start the water flow from the starting pipe
    public void PropagateWater()
    {
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
