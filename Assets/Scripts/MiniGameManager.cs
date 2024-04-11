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
        cashSlider.maxValue = score / decrement;
        // Initialize pipes with random rotations
        for (int i = 0; i < pipes.Length; i++)
        {
            MiniGameUnoPipe pipe = pipeHolder.transform.GetChild(i).GetComponent<MiniGameUnoPipe>();
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
    }

    private void Update()
    {
        if (endPipe.hasWater)
        {
            Debug.Log("BINGO");
            timerIsRunning = false; // Stop the timer when the game ends

            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            gameManager.isInMiniGame = false;
            // Trigger the onMiniGameEnd event
            startMiniGame.onMiniGameEnd?.Invoke();
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
