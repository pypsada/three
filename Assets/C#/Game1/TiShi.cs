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
                GameObject newPrefab=Instantiate(Bullet, transform.position, Quaternion.identity);
                if (newPrefab != null)
                {
                    // �޸ĵ�ǰԤ���������
                    TiShi tiShi = newPrefab.GetComponent<TiShi>();
                    if (tiShi != null)
                    {
                        tiShi.flag = true;
                    }
                }
            }
            else
            {
                // ����x������ڵ���x��ֵ����תԤ����180�Ⱥ�������
                GameObject newPrefab = Instantiate(Bullet, transform.position, Quaternion.identity);
                newPrefab.transform.Rotate(0, 180, 0);
                if (newPrefab != null)
                {
                    // �޸ĵ�ǰԤ���������
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
