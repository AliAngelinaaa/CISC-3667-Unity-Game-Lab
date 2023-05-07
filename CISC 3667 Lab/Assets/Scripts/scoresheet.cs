using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class scoresheet : MonoBehaviour
{
    public TextMeshProUGUI[] highScoreTexts;

    void Start()
    {
        DisplayHighScores();
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

    private void DisplayHighScores()
    {
        List<int> highScores = GetHighScores();

        // Sort the high scores in decreasing order of score
        highScores.Sort((x, y) => y.CompareTo(x));

        // Display the high scores
        for (int i = 0; i < highScores.Count && i < highScoreTexts.Length; i++)
        {
            int rank = i + 1;
            string text = rank + ". " + highScores[i];
            highScoreTexts[i].text = text;
        }
    }
}
