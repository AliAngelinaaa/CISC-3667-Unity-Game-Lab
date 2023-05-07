using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class balloonscript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip pop;
    [SerializeField] float speed = 3.0f;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isLevel2;
    public Animator animator;
    public GameObject balloonPrefab;
    public ScoreManager scoreManager;
    private bool isIncreasing = true;


    private float screenWidth;
    private float screenHeight;
    private Vector3 targetPosition;
    private int score = 0;

    public float balloonSize = 1.0f; 
    private float minBalloonSize = 0.5f;
    private float maxBalloonSize = 2.0f; 
    private float balloonGrowthRate = 0.05f;
    private float maxGrowthRate = 0.2f; 
    private float minGrowthRate = 0.01f; 

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();

        // Get the screen dimensions
        Camera mainCamera = Camera.main;
        screenHeight = mainCamera.orthographicSize;
        screenWidth = screenHeight * mainCamera.aspect;

        //animator stuff
        animator = GetComponent<Animator>();
        animator.SetBool("isPopping", false);

        scoreManager = ScoreManager.instance;

        // Generate a random position within the screen
        targetPosition = new Vector3(Random.Range(-screenWidth, screenWidth), Random.Range(-screenHeight, screenHeight), 0.0f);

        if(isLevel2)
        {
            // Invoke the BalloonSizeIncrease method to increase the size of the balloon every X seconds
            InvokeRepeating("BalloonSizeIncrease", 0f, 1f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        // Check if the balloon has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Generate a new random position within the screen
            targetPosition = new Vector3(Random.Range(-screenWidth, screenWidth), Random.Range(-screenHeight, screenHeight), 0.0f);
        }
        else
        {
            // Move towards the target position
            Vector3 direction = (targetPosition - transform.position).normalized;
            rigid.velocity = direction * speed;
        }
    }

    private void BalloonSizeIncrease(){
        if (isIncreasing)
        {
            if (balloonSize < maxBalloonSize)
            {
                balloonSize += balloonGrowthRate;
                transform.localScale = new Vector3(balloonSize, balloonSize, 1.0f);
            }
            else
            {
                isIncreasing = false;
            }
        }
        else
        {
            if (balloonSize > minBalloonSize)
            {
                balloonSize -= balloonGrowthRate;
                transform.localScale = new Vector3(balloonSize, balloonSize, 1.0f);
            }
            else
            {
                isIncreasing = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="pin")
        {
            if(isLevel2 && balloonSize >= maxBalloonSize)
            {
                // Balloon is too big, don't give any points
                Destroy(gameObject, 0.1f);
                StartCoroutine(RespawnBalloon());
            }
            else
            {
                AudioSource.PlayClipAtPoint(pop, transform.position);
                animator.SetBool("isPopping", true);
                Destroy(gameObject, 0.1f);
                if(isLevel2)
                {
                    scoreManager.AddScore(Mathf.RoundToInt(10 * (1.0f - balloonSize/maxBalloonSize)));
                }
                else
                {
                    scoreManager.AddScore();
                }
                StartCoroutine(RespawnBalloon());
            }
        }
    }

    IEnumerator RespawnBalloon()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-screenWidth, screenWidth), Random.Range(-screenHeight, screenHeight), 0.0f);
        GameObject newBalloon = Instantiate(balloonPrefab, randomPosition, Quaternion.identity);

        // Set the score text on the new balloon to the current score
        TextMeshProUGUI newScoreText = newBalloon.GetComponentInChildren<TextMeshProUGUI>();
        if (newScoreText != null)
        {
            newScoreText.text = "Score: " + score.ToString();
        }

        // Set the size of the new balloon to be the same as the old balloon's starting size
        balloonscript newBalloonScript = newBalloon.GetComponent<balloonscript>();
        if (newBalloonScript != null)
        {
            newBalloonScript.balloonSize = balloonSize;;
        }
            yield return null;

    }

}
