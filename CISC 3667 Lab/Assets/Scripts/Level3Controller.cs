using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level3Controller : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] TextMeshProUGUI finalscore;
    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getScore(){
        finalscore.text = "Score: " + PlayerPrefs.GetInt("Score", 0).ToString();
    }
}
