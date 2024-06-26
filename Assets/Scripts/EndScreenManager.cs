using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include the TextMeshPro namespace
using UnityEngine.SceneManagement; // Include the UI namespace


public class EndScreenManager : MonoBehaviour
{
    public TextMeshProUGUI playername; // Reference to the score text
    public TextMeshProUGUI scoreText; // Reference to the score text

    // Start is called before the first frame update
    void Start()
    {
        // Get the player's name
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        playername.text = playerName;

        // Get the score
        int score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "<color=green>$</color>" + score;
    }

    public void PlayAgainButton()
    {
        // Reload the game scene
        SceneManager.LoadScene("halli_test_minigame_working");
    }

    public void MainMenuButton()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
