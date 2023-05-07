using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool isPaused = false;
    public ScoreManager scoreManager;
    public balloonscript balloon;
    // Start is called before the first frame update
    void Start()
    {
        if (scoreManager == null) {
            scoreManager = FindObjectOfType<ScoreManager>();
        }
        if (balloon == null) {
            balloon = FindObjectOfType<balloonscript>();
        }

        if (SceneManager.GetActiveScene().name == "level 1")
        {
            scoreManager.score=0;
            balloon.balloonSize = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void gameplay(){
        SceneManager.LoadScene("level 1");
    }
    
    public void reloadScene(){
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void pause(){
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resume(){
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void remenu(){
        if (scoreManager != null)
        {
            Destroy(scoreManager.gameObject);
        }
        SceneManager.LoadScene("menu");
    }

    public void leaderboard(){
        SceneManager.LoadScene("LeaderBoard");
    }
}
