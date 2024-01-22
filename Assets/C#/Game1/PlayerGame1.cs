using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGame1 : MonoBehaviour
{
    public float upwardForce; // 向上的力大小
    public float speed;
    public float speed1;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(speed, rb.velocity.y);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, speed1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            speed = -speed;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (collision.CompareTag("Upwall"))
        {
            Destroy(gameObject);
        }
    }
}
