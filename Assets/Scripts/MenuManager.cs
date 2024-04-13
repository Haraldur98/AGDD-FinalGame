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
    public AudioSource audioSource;
    public AudioClip theme;
    private void Start()
    {
        // Update the high scores
        UpdateHighScores();
        audioSource.volume = 0.01f;
        audioSource.clip = theme;
        audioSource.loop = true;
        audioSource.Play();
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
        SceneManager.LoadScene("halli_test_minigame_working");
    }

    private void UpdateHighScores()
    {
        // Get the high scores
        Dictionary<string, int> highScores = HighScoreManager.Instance.GetHighScores();

        // Find the length of the longest name
        if (highScores.Keys.Count == 0) return;
        int maxLength = highScores.Keys.Max(name => name.Length);

        // Update the high scores text
        highScoresText.text = string.Join("\n", highScores.Select(x => x.Key.PadRight(maxLength) + ": " + x.Value + "<color=green>$</color>"));
    }
}