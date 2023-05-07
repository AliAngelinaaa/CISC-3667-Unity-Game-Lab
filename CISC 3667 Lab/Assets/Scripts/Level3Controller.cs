using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Controller : MonoBehaviour
{
    private GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("gameOverMenu");
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
