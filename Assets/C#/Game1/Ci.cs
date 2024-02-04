using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ci : MonoBehaviour
{
    public float speed = 4f;
    Rigidbody2D rb;
    public float time;
    public float XRight;
    public float YDown;
    public float XLeft;
    public float YUp;
    public float Z;
    public bool Left;
    void Start()
    {
        Left = true;
        time = 0f;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Left)
        {
            if (transform.position.x==XRight)
            {
                Left=false;
                gameObject.transform.eulerAngles=new Vector3(0,180,-90);
            }
        }
        else
        {
            if(transform.position.x == XLeft)
            {
                Left=true;
                gameObject.transform.eulerAngles = new Vector3(0, 0, -90);
            }
        }
        time += Time.deltaTime;
        if(time >=12f)
        {
            speed = 6f;
        }
        if (time>18f)
        {
            gameObject.transform.localScale = new(2.3f, 1.1f , 1f);
        }
        if(transform.position.y>=9 || transform.position.y<=-9)
        {
            int s=Random.Range(0, 4);
            if(s==0)
            {
                back1();
            }
            else if (s==1)
            {
                back2();
            }
            else if(s==2)
            {
                back3();
            }
            else if(s==3)
            {
                back4();
            }
        }
    }

    public void back1()
    {
        rb.velocity = transform.right * speed;
        gameObject.transform.position = new Vector3(XRight, YUp, Z);
    }

    public void back2()
    {
        rb.velocity = transform.right * speed * -1;
        gameObject.transform.position = new Vector3(XRight, YDown, Z);
    }

    public void back3()
    {
        rb.velocity = transform.right * speed;
        gameObject.transform.position = new Vector3(XLeft, YUp, Z);
    }
    public void back4()
    {
        rb.velocity = transform.right * speed*-1;
        gameObject.transform.position = new Vector3(XLeft, YDown, Z);
    }
}
