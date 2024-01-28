using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Player;
    public float speed = 20f; // 子弹速度
    Rigidbody2D rb;
    private float time = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; // 子弹向右飞行
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;
        //if (time>=5f)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(Player);
            Destroy(gameObject);
        }
    }
}
