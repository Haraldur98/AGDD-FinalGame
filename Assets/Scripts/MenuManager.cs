using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    public TMPro.TMP_InputField nameInputField;
    public Toggle tutorialToggle; // Assign this in the inspector
    public TMPro.TextMeshProUGUI highScoresText;

    private void Start()
    {
        // Update the high scores
        UpdateHighScores();
    }

    public void StartGame()
    {
        // Save the player's name
        string playerName = nameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);

        // Save the tutorial toggle state
        bool tutorial = tutorialToggle.isOn;
        PlayerPrefs.SetInt("Tutorial", tutorial ? 1 : 0);

        // Load the game scene
        SceneManager.LoadScene("laddertest");
        Debug.Log("StartGame");
    }

    private void UpdateHighScores()
    {
        // Get the high scores
        HighScoreManager highScoreManager = HighScoreManager.Instance;
        Debug.Log("HighScoreManager.Instance: " + highScoreManager);
        
        List<int> highScores = highScoreManager.GetHighScores();
        Debug.Log("highScores: " + highScores);

        // Update the high scores text
        Debug.Log("highScoresText: " + highScoresText);
        highScoresText.text = string.Join("\n", highScores);
    }
}