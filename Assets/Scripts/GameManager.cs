using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include the TextMeshPro namespace
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Include the UI namespace

public class GameManager : MonoBehaviour
{
    public float time { get; private set; } // The current time
    public int coins { get; private set; } // The number of coins
    public float gameDuration = 20; // The duration of a month in seconds
    public Slider timeSlider; // Reference to the slider
    public TextMeshProUGUI coinText; // Reference to the TextMeshPro text
    public int[] currentLevelsMiniGames = new int[3] { 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        coins = 0;
        coinText.text = "<color=green>$</color>:" + coins; // Initialize the text
        timeSlider.maxValue = gameDuration; // Initialize the slider's max value

        // Subscribe to the onMiniGameEnd event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is a mini-game scene
        if (scene.name == "MiniGameUNO")
        {
            // Find the MiniGameManager in the loaded scene
            MiniGameManager miniGameManager = GameObject.FindObjectOfType<MiniGameManager>();

            // Subscribe to the onMiniGameEnd event
            miniGameManager.onMiniGameEnd.AddListener(() => EndMiniGame(scene.name));
        }
        else if (scene.name == "minigame2")
        {
            // Find the MiniGameManager in the loaded scene
            MiniGame2Manager miniGameManager2 = GameObject.FindObjectOfType<MiniGame2Manager>();

            // Subscribe to the onMiniGameEnd event
            miniGameManager2.onMiniGameEnd.AddListener(() => EndMiniGame(scene.name));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the time by the time since the last frame
        time += Time.deltaTime;
        coinText.text = "<color=green>$</color>:" + coins; // Update the text
        timeSlider.value = time; // Update the slider's value

        if (time >= gameDuration)
        {
            // End the game
            EndGame();
        }
    }

    void EndGame()
    {   
        // Get the player's name
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        // Save the high score
        HighScoreManager.Instance.AddHighScore(playerName , coins);
         // Save the player's score
        PlayerPrefs.SetInt("Score", coins);
        // Load the end screen
        SceneManager.LoadScene("EndScreen");

    }


    public void LoadMiniGame(string sceneName)
    {

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void EndMiniGame(string miniGameSceneName)
    {
        // Unload the mini-game scene
        SceneManager.UnloadSceneAsync(miniGameSceneName);
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

    void OnDestroy()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}
}