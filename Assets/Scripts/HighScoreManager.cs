using System.Collections;
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

    public void AddHighScore(int score)
    {
        // Get the current high scores
        List<int> highScores = GetHighScores();

        // Add the new score
        highScores.Add(score);

        // Sort the list in descending order
        highScores.Sort((a, b) => b.CompareTo(a));

        // If there are more than the max number of high scores, remove the lowest ones
        while (highScores.Count > MaxHighScores)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        // Save the high scores
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }
    }

    public List<int> GetHighScores()
    {
        List<int> highScores = new List<int>();

        for (int i = 0; i < MaxHighScores; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i))
            {
                highScores.Add(PlayerPrefs.GetInt("HighScore" + i));
            }
            else
            {
                break;
            }
        }

        return highScores;
    }
}
