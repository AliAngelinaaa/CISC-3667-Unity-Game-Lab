using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonscript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip pop;
    [SerializeField] float speed = 3.0f;
    public Animator animator;

    private float screenWidth;
    private float screenHeight;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();

        // Get the screen dimensions
        Camera mainCamera = Camera.main;
        screenHeight = mainCamera.orthographicSize;
        screenWidth = screenHeight * mainCamera.aspect;

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
            AudioSource.PlayClipAtPoint(pop, transform.position);
            animator.SetTrigger("pop");
            Destroy(gameObject, 0.1f); 
        
    }

}
