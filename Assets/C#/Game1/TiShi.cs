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
                // ����x����С��x��ֵ��ֱ������Ԥ����
                GameObject newBullet=Instantiate(Bullet, transform.position, Quaternion.identity);
            }
            else
            {
                // ����x������ڵ���x��ֵ����תԤ����180�Ⱥ�������
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
