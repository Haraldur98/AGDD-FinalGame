using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }
    private const int MaxHighScores = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddHighScore(string playerName, int score)
    {
        // Get the current high scores
        Dictionary<string, int> highScores = GetHighScores();

        // Add the new score
        highScores[playerName] = score;

        // Sort the list in descending order and keep the top 5 scores
        highScores = highScores.OrderByDescending(x => x.Value).Take(MaxHighScores).ToDictionary(x => x.Key, x => x.Value);

        // Save the high scores
        PlayerPrefs.SetString("HighScores", string.Join(",", highScores.Select(x => x.Key + ":" + x.Value)));
    }

    public Dictionary<string, int> GetHighScores()
    {
        Dictionary<string, int> highScores = new Dictionary<string, int>();

        // Get the high scores string from PlayerPrefs
        string highScoresString = PlayerPrefs.GetString("HighScores", "");

        // Split the string into individual scores
        string[] highScoresArray = highScoresString.Split(',');

        // Parse each score and add it to the dictionary
        foreach (string highScore in highScoresArray)
        {
            string[] parts = highScore.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[1], out int score))
            {
                highScores[parts[0]] = score;
            }
        }

        return highScores;
    }
}
