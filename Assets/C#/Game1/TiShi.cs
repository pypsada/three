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
                GameObject newPrefab=Instantiate(Bullet, transform.position, Quaternion.identity);
                if (newPrefab != null)
                {
                    // 修改当前预制体的属性
                    TiShi tiShi = newPrefab.GetComponent<TiShi>();
                    if (tiShi != null)
                    {
                        tiShi.flag = true;
                    }
                }
            }
            else
            {
                // 物体x坐标大于等于x的值，翻转预制体180度后再生成
                GameObject newPrefab = Instantiate(Bullet, transform.position, Quaternion.identity);
                newPrefab.transform.Rotate(0, 180, 0);
                if (newPrefab != null)
                {
                    // 修改当前预制体的属性
                    TiShi tiShi = newPrefab.GetComponent<TiShi>();
                    if (tiShi != null)
                    {
                        tiShi.flag = true;
                    }
                }
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
