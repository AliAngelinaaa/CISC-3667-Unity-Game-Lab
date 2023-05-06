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
    public Animator animator;
    public GameObject balloonPrefab;
    public ScoreManager scoreManager;

    private float screenWidth;
    private float screenHeight;
    private Vector3 targetPosition;
    private int score = 0;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="pin")
        {            
            AudioSource.PlayClipAtPoint(pop, transform.position);
            animator.SetBool("isPopping", true);
            Destroy(gameObject, 0.1f);
            scoreManager.AddScore();
            StartCoroutine(RespawnBalloon());
        }
    }

    IEnumerator RespawnBalloon()
    {
        Debug.Log("Respawning balloon");
        // Wait for 1 second before spawning a new balloon
        
        // Create a new instance of the balloon prefab at a random position within the screen
        Vector3 randomPosition = new Vector3(Random.Range(-screenWidth, screenWidth), Random.Range(-screenHeight, screenHeight), 0.0f);
        GameObject newBalloon = Instantiate(balloonPrefab, randomPosition, Quaternion.identity);

        // Set the score text on the new balloon to the current score
        TextMeshProUGUI newScoreText = newBalloon.GetComponentInChildren<TextMeshProUGUI>();
        if (newScoreText != null)
        {
            newScoreText.text = "Score: " + score.ToString();
        }

        yield return null; 
    }
}
