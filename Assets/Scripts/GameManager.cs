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
    // public Slider timeSlider; // Reference to the slider
    public TextMeshProUGUI coinText; // Reference to the TextMeshPro text
    public TextMeshProUGUI digitalClock; // Reference to the TextMeshPro text for the timer
    public int[] currentLevelsMiniGames = new int[3] { 0, 0, 0 };
    public GameObject miniGameOne;
    public GameObject miniGameTwo;
    public Camera mainCamera;
    private int currentLevel;

    public bool isInMiniGame;
    public bool isTimeRunning;
    // Start is called before the first frame update
    public GameObject whereToSpawn;
    void Start()
    {
        time = gameDuration ;
        coins = 0;
        PlayerPrefs.SetInt("Score", 0); // Initialize the player's score
        coinText.text = "<color=green>$</color>:" + coins; // Initialize the text


        // Subscribe to the onMiniGameEnd event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // check if user wants tutorial
        bool tutorial = PlayerPrefs.GetInt("Tutorial", 0) == 1;

        if (tutorial)
        {
            whereToSpawn.transform.position = new Vector3(-30f, 9.5f, 0.4f);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is a mini-game scene
        if (scene.name == "MiniGameUNO")
        {
            // Find the MiniGameManager in the loaded scene
            isInMiniGame = true;
            StartMiniGame startMiniGame = GameObject.FindObjectOfType<StartMiniGame>();
            startMiniGame.StartGame(currentLevel);
            MiniGameManager miniGameManager = GameObject.FindObjectOfType<MiniGameManager>();
            miniGameManager.mainCameraPos = mainCamera.transform.position;

            // Subscribe to the onMiniGameEnd event
            startMiniGame.onMiniGameEnd.AddListener(() => 
                EndMiniGame(scene.name));
        }
        else if (scene.name == "minigame2")
        {
            isInMiniGame = true;
            // Find the MiniGameManager in the loaded scene
            MiniGame2Manager miniGameManager2 = GameObject.FindObjectOfType<MiniGame2Manager>();
            Debug.Log("AM HERE:" + mainCamera.transform.position);
            miniGameManager2.mainCameraPos = mainCamera.transform.position;

            // Subscribe to the onMiniGameEnd event
            miniGameManager2.onMiniGameEnd.AddListener(() => EndMiniGame(scene.name));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the time by the time since the last frame
        if (isTimeRunning) 
        {
            time -= Time.deltaTime;
        }

        if (time < 60)
        {
            // warning text to show the player that the game is about to end
             digitalClock.color = new Color(1.0f, 0.5f, 0.0f); // Orange color
        }
        else if (time < 30)
        {
            // warning text to show the player that the game is about to end
            digitalClock.color = Color.red;
        }
    

        if (time < 0)
        {
            EndGame();
            time = 0;
        }

        // Update the digital clock's text
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        digitalClock.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // Get score from PlayerPrefs
        int cash = PlayerPrefs.GetInt("Score", 0);
        // Update coin text
        coinText.text = "<color=green>$</color>:" + cash;
        coins = cash;
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


    public void LoadMiniGame(string sceneName, int level)
    {
        currentLevel = level;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void EndMiniGame(string miniGameSceneName)
    {   
        isInMiniGame = false;
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