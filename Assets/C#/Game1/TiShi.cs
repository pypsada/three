using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiShi : MonoBehaviour
{
    private float time=0f;
    public float x;
    public GameObject Bullet;
    public bool flag=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1f)
        {
            if (transform.position.x < x)
            {
                // 物体x坐标小于x的值，直接生成预制体
                GameObject newBullet=Instantiate(Bullet, transform.position, Quaternion.identity);
            }
            else
            {
                // 物体x坐标大于等于x的值，翻转预制体180度后再生成
                GameObject newBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
                newBullet.transform.Rotate(0, 180, 0);
            }
            if (flag)
            {
                gameObject.SetActive(false);
            }
            else
            {
                time = -100000f;
            }

        }
    }
}
