using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGame3 : MonoBehaviour
{
    public float speed1;
    private Rigidbody2D rb;
    public int health = 3;
    public Animator animator;
    public float WudiTime = 0f;
    public Parent Parent;
    public Health3 Health3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Parent = FindObjectOfType<Parent>();
        Health3 = FindObjectOfType<Health3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, speed1);
        }
        if (WudiTime > 0f)
        {
            WudiTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Upwall"))
        {
            Health3.HealthJiLu();
            animator.SetTrigger("Hurt");
            health -= 1;
            transform.position = new Vector3(0, -8, 0); // 将物体传送至(0, -6, 0)
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        if (collision.CompareTag("UpWallTrue"))
        {
            GameObject parentObject = collision.transform.parent.gameObject;
            parentObject.SetActive(false);
            //Parent.Chose();
        }
    }

}
