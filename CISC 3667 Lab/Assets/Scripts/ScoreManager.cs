using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public int balloonsPopped = 0;
    public bool islevel1;

    private void Awake()
    {
        // Ensure that only one instance of ScoreManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if(islevel1)
                score = 0;
                balloonsPopped = 0;
                islevel1 = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(!islevel1)
        {
            // If this is not level 1, retrieve the score from PlayerPrefs
            score = PlayerPrefs.GetInt("Score", 0);
            UpdateScoreDisplay(scoreText);
        }
    }
    

    public void AddScore()
    {
        score++;
        balloonsPopped++;
        //scoreText.text = "Score: " + score.ToString();
        UpdateScoreDisplay(scoreText);

        if (score == 10)
        {
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene("level 2");
        }
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        //scoreText.text = "Score: " + score.ToString();
        balloonsPopped++;
        UpdateScoreDisplay(scoreText);

        if (score == 50)
        {   
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene("level 3");
        }
    }

    public void UpdateScoreDisplay(TextMeshProUGUI scoreDisplay)
    {
        scoreText = scoreDisplay;
        scoreDisplay.text = "Score: " + score.ToString();
    }

    public void SaveScoreToLeaderboard()
    {
        // Retrieve the player name from PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");

        // Get the current high scores
        List<int> highScores = GetHighScores();

        // Add the player's score to the list
        highScores.Add(score);

        // Sort the high scores in decreasing order of score
        highScores.Sort((x, y) => y.CompareTo(x));

        // Save the updated high scores to PlayerPrefs
        for (int i = 0; i < highScores.Count && i < 5; i++)
        {
            PlayerPrefs.SetInt("HighScore" + (i+1), highScores[i]);
        }

        // Save the player's name and score to PlayerPrefs
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("PlayerScore", score);
    }

    private List<int> GetHighScores()
    {
        List<int> highScores = new List<int>();

        for (int i = 1; i <= 5; i++)
        {
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);
            if (score > 0)
            {
                highScores.Add(score);
            }
        }
        return highScores;
    }
}
