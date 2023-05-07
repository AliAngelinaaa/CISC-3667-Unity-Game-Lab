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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
        balloonsPopped++;
        UpdateScoreDisplay(scoreText);

        if (score == 10)
        {
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene("level 2");
        }

        if (score == 30)
        {
            SceneManager.LoadScene("level 3");
        }
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        scoreText.text = "Score: " + score.ToString();
        score++;
        UpdateScoreDisplay(scoreText);

        if (score == 10)
        {
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene("level 2");
        }

        if (score == 30)
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
}
