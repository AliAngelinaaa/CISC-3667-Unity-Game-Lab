using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] const int SPEED = 10;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] float movement_horizontal;
    [SerializeField] float movement_vertical;

    [SerializeField] GameObject pin;
    private float screenWidth;
    private float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();

        Camera mainCamera = Camera.main;
        screenHeight = mainCamera.orthographicSize;
        screenWidth = screenHeight * mainCamera.aspect;
        Debug.Log("Screen width: " + screenWidth);
        Debug.Log("Screen height: " + screenHeight);

    }

    // Update is called once per frame
    void Update()
    {
        movement_horizontal = Input.GetAxis("Horizontal");
        movement_vertical = Input.GetAxis("Vertical");
        
        if (Input.GetButtonDown("Fire1"))
            ShootPin();
        
    }

    private void FixedUpdate(){
        rigid.velocity = new Vector2(SPEED * movement_horizontal, SPEED * movement_vertical);
        if (movement_horizontal < 0 && isFacingRight || movement_horizontal > 0 && !isFacingRight)
            Flip();
        float clampedX = Mathf.Clamp(transform.position.x, -screenWidth, screenWidth);
    float clampedY = Mathf.Clamp(transform.position.y, -screenHeight, screenHeight);
    transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void ShootPin()
    {
        Instantiate(pin, transform.position, Quaternion.identity);
    }

}
