using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score;

    [SerializeField] TextMeshProUGUI scoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        score = ScoreManager.instance.score;
    }

    // Update is called once per frame
    void Update()
    {
        GetScore();
        DisplayScore();
    }

    public void DisplayScore()
    {
        scoreTxt.text = "Score: " + score.ToString();
    }

    public void GetScore()
    {
        score = ScoreManager.instance.score;
    }
}