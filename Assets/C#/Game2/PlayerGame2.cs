using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGame : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Huan Huan;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(speed, rb.velocity.y);
        Huan = FindObjectOfType<Huan>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            speed = -speed;
            rb.velocity = new Vector2(speed, -rb.velocity.y);
            Huan.PanDing();
        }
        else if (collision.CompareTag("Upwall"))
        {
            Destroy(gameObject);
        }
    }
}
