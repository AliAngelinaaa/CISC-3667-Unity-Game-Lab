using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    [SerializeField] float speed = 5f;
    [SerializeField] int damage = 1;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] ScoreManager scoreManager;
    private Vector2 moveInput;
    private Transform player;

    private void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }
    }

    private void Update()
    {
        // Follow the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Update animator parameters
        Vector2 direction = (player.position - transform.position).normalized;
        animator.SetFloat("XInput", direction.x);
        animator.SetFloat("YInput", direction.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check for collision with player
        if (other.gameObject.CompareTag("Player"))
        {
            // Player dies
            Destroy(other.gameObject);
            gameOverMenu.SetActive(true);
            // Check if new high score
            int score = scoreManager.score;
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            Time.timeScale = 0f;
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void MovePlayer(Vector2 direction)
    {
        RaycastHit2D[] castCollisions = new RaycastHit2D[10];
        ContactFilter2D movementFilter = new ContactFilter2D();
        movementFilter.SetLayerMask(LayerMask.GetMask("Wall"));
        int count = rigid.Cast(direction, movementFilter, castCollisions, speed * Time.fixedDeltaTime + 0.1f);
        if (count == 0)
        {
            Vector2 moveVector = direction * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + moveVector);
        }
    }
}
