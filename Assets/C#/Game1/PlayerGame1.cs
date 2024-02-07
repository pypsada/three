using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGame1 : MonoBehaviour
{
    public float upwardForce; // 向上的力大小
    public float speed;
    public float speed1;
    private Rigidbody2D rb;
    public Health1 Health1;
    public int health=3;
    public Animator animator;
    public float WudiTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(speed, rb.velocity.y);
        Health1 = FindObjectOfType<Health1>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (health<=0)
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, speed1);
        }
        if (WudiTime>0f)
        {
            WudiTime -= Time.deltaTime;
        }
    }

    public void Hurt()
    {
        Health1.HealthJiLu();
        health -= 1;
        animator.SetTrigger("Hurt");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            speed = -speed;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        if (WudiTime<=0f)
        {
            if (collision.CompareTag("Upwall"))
            {
                Hurt();
                WudiTime = 0.2f;
            }

        }
        if (collision.CompareTag("UpWallTrue"))
        {
            Health1.HealthJiLu();
            if (WudiTime<=0f)
            {
                health -= 1;
            }

            gameObject.transform.position=new Vector3(0,0,0);
            rb.velocity = new Vector3(speed, 0);
            animator.SetTrigger("Hurt");
            WudiTime = 0.2f;
        }
    }
}
