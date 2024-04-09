using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include the TextMeshPro namespace

public class EndScreenManager : MonoBehaviour
{
    public static EndScreenManager Instance { get; private set; } // Singleton instance
    public GameObject endScreen; // Reference to the end screen
    public TextMeshProUGUI scoreText; // Reference to the score text

    void Awake()
    {
        // Initialize the singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowEndScreen(int score)
    {
        // Update the score text
        scoreText.text = "Score: " + score;

        // Show the end screen
        endScreen.SetActive(true);
    }
}
