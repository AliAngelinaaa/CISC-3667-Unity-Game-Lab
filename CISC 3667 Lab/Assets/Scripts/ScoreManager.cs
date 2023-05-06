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

    private void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject); // mark the object to persist between scene loads
        }
        else
            Destroy(gameObject);
    }

    public void AddScore(){
        score++;
        scoreText.text = "Score: " + score.ToString();
        balloonsPopped++;
        if (balloonsPopped == 10) {
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene("level 2");
        }
        if(balloonsPopped == 30){
            SceneManager.LoadScene("level 3");
        }
    }

    public void UpdateScoreDisplay(TextMeshProUGUI scoreDisplay){
        scoreText = scoreDisplay;
        scoreDisplay.text = "Score: " + score.ToString();
    }
    
}
