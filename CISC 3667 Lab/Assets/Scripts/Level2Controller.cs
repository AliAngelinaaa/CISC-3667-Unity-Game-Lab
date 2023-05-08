using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level2Controller : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        int score = PlayerPrefs.GetInt("Score"); // retrieve the score from PlayerPrefs
        ScoreManager.instance.score = score; // update the score in ScoreManager
        ScoreManager.instance.UpdateScoreDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
