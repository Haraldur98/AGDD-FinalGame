using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Toggle tutorialToggle;
    public TextMeshProUGUI highScoresText;
    private void Start()
    {
        // Update the high scores
        UpdateHighScores();
        LogPlayerPrefs();
    }

    public void StartGame()
    {
        // Check if the name input field is empty
        if (string.IsNullOrEmpty(nameInputField.text))
        {
            // Change the color of the placeholder text to red
            var placeholderText = nameInputField.placeholder as TMP_Text;
            if (placeholderText != null)
            {
                placeholderText.color = Color.red;
            }

            // Return from the method
            return;
        }


        // Save the player's name
        string playerName = nameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);

        // Save the tutorial toggle state
        bool tutorial = tutorialToggle.isOn;
        PlayerPrefs.SetInt("Tutorial", tutorial ? 1 : 0);

        // Load the game scene
        SceneManager.LoadScene("DefaultScene");
        Debug.Log("StartGame");
    }

    private void UpdateHighScores()
    {
        // Get the high scores
        Dictionary<string, int> highScores = HighScoreManager.Instance.GetHighScores();

        // Find the length of the longest name
        int maxLength = highScores.Keys.Max(name => name.Length);

        // Update the high scores text
        highScoresText.text = string.Join("\n", highScores.Select(x => x.Key.PadRight(maxLength) + ": " + x.Value + "<color=green>$</color>"));
    }

    public void LogPlayerPrefs()
    {
        // List of keys you want to check
        string[] keys = new string[] { "PlayerName", "Tutorial", "HighScores" };

        foreach (string key in keys)
        {
            if (PlayerPrefs.HasKey(key))
            {
                Debug.Log(key + ": " + PlayerPrefs.GetString(key));
            }
            else
            {
                Debug.Log("No value found for key: " + key);
            }
        }
    }
}