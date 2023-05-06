using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);

        // Check if pin is out of bounds
        if (transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="balloon")
           Destroy(gameObject, 0.1f);         
    }
}
