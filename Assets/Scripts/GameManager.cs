using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include the TextMeshPro namespace
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI; // Include the UI namespace

public class GameManager : MonoBehaviour
{
    public float time { get; private set; } // The current time
    public int coins { get; private set; } // The number of coins
    public float gameDuration = 4; // The duration of a month in seconds
    public Slider timeSlider; // Reference to the slider
    public TextMeshProUGUI coinText; // Reference to the TextMeshPro text
    public int[] currentLevelsMiniGames = new int[3] { 0, 0, 0 };
    public GameObject miniGameOne;
    public GameObject miniGameTwo;
    public Camera mainCamera;
    public int miniGameCurrentlyIn = 0;
    public bool isInMiniGAme = false;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        coins = 0;
        coinText.text = "Coins: " + coins; // Initialize the text
        timeSlider.maxValue = gameDuration; // Initialize the slider's max value
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the time by the time since the last frame
        time += Time.deltaTime;
        coinText.text = "Coins: " + coins; // Update the text
        timeSlider.value = time; // Update the slider's value

        if (time >= gameDuration)
        {
            // End the game
            EndGame();
        }

        if (!isInMiniGAme)
        {
            DisableMiniGame();
        }
    }

    void DisableMiniGame()
    {
        if (miniGameCurrentlyIn == 1)
        {
            miniGameOne.SetActive(false);
        }
        else if (miniGameCurrentlyIn == 2)
        {
            miniGameTwo.SetActive(false);
        }
        miniGameCurrentlyIn = 0;
    }

    void EndGame()
    {
        // // Get the player's name
        // string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        // // Save the high score
        // HighScoreManager.Instance.AddHighScore(playerName, coins);
        // // Save the player's score
        // PlayerPrefs.SetInt("Score", coins);
        // // Load the end screen
        // SceneManager.LoadScene("EndScreen");
    }


    public void LoadMiniGame(int miniGameNr, int miniGameLevel)
    {
        if (miniGameNr == 1)
        {
            miniGameOne.SetActive(true);
            GameObject startMiniGameObject = miniGameOne.transform.Find("StartMiniGame").gameObject;
            if (startMiniGameObject != null)
            {
                StartMiniGame startMiniGame = startMiniGameObject.GetComponent<StartMiniGame>();
                if (startMiniGame != null)
                {
                    Vector3 newPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -1);
                    startMiniGame.StartGame(newPos, miniGameLevel);
                    isInMiniGAme = true;
                    miniGameCurrentlyIn = 1;
                }
            }
        }
        else if (miniGameNr == 2)
        {
            miniGameTwo.SetActive(true);
            // miniGameTwo.GetComponent<StartMiniGame>().SetIsActive(true);
        }
    }

    public void UnloadMiniGame(int miniGameNr)
    {
        if (miniGameNr == 1)
        {
            miniGameOne.SetActive(false);
        }
        else if (miniGameNr == 2)
        {
            miniGameTwo.SetActive(false);
        }
    }

    // Method to increase the number of coins
    public void AddCoin()
    {
        coins++;
        coinText.text = "Coins: " + coins; // Update the text
    }

    public void UpdateMiniGameOne()
    {
        currentLevelsMiniGames[0]++;
    }

    public void UpdateMiniGameTwo()
    {
        currentLevelsMiniGames[1]++;
    }

    public void UpdateMiniGameThree()
    {
        currentLevelsMiniGames[2]++;
    }

    public int[] GetCurrentLevels()
    {
        return currentLevelsMiniGames;
    }

    public void AddCoins(int newCoins)
    {
        coins += newCoins;
    }
}