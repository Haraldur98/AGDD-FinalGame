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
    public float monthDuration = 30; // The duration of a month in seconds
    public int payment = 100; // The payment for each month
    public Slider timeSlider; // Reference to the slider
    public TextMeshProUGUI coinText; // Reference to the TextMeshPro text
    public int[] currentLevelsMiniGames = new int[3] { 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        coins = 10000;
        coinText.text = "Coins: " + coins; // Initialize the text
        timeSlider.maxValue = monthDuration; // Initialize the slider's max value
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the time by the time since the last frame
        time += Time.deltaTime;
        coinText.text = "Coins: " + coins; // Update the text
        timeSlider.value = time; // Update the slider's value

        // Check if a month has passed
        if (time >= monthDuration)
        {
            time = 0; // Reset the time
            coins -= payment; // Deduct the payment from the coins
            coinText.text = "Coins: " + coins; // Update the text
            timeSlider.value = time; // Update the slider's value
        }

    }


    public void LoadMiniGame(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
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
}