using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAppear : MonoBehaviour
{
    public GameObject prefab; // Ԥ�������
    public float UpY;
    public float DownY;
    public float LeftX;
    public float RightX;
    public float time;
    public int Ci;

    void Start()
    {
        time = 0f;
        Ci = 0;
    }

    public void Update()
    {
        time += Time.deltaTime;
        if (time > 2f)
        {
            if (Ci>9)
            {
                SpawnPrefab();
                SpawnPrefab();
                Ci += 1;
            }
            else if (Ci>18f)
            {
                SpawnPrefab();
                SpawnPrefab();
                SpawnPrefab();
                Ci += 1;
            }
            else
            {
                SpawnPrefab();
                Ci += 1;
            }

            time = 0f;
        }

    }

    public void SpawnPrefab()
    {
        // �ڷ�Χ���������λ��
        float spawnX = Random.Range(0,2);
        Debug.Log(spawnX);
        if (spawnX ==0 )
        {
            spawnX = LeftX;
        }
        else
        {
            spawnX = RightX;
        }
        float spawnY = Random.Range(UpY,DownY);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        // ����Ԥ����
        GameObject newPrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // ���spawnX = RightX������תԤ����180��
        if (spawnX == RightX)
        {
            newPrefab.transform.Rotate(0, 180, 0);
        }

        // ��鵱ǰԤ�����Ƿ����
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
}