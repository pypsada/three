using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAppear : MonoBehaviour
{
    public GameObject prefab; // 预制体对象
    public float UpY;
    public float DownY;
    public float LeftX;
    public float RightX;
    public float time;
    public int Ci;
    public NumberDisplayGame1 NumberDisplayGame1;
    void Start()
    {
        time = 0f;
        Ci = 0;
        NumberDisplayGame1 = FindObjectOfType<NumberDisplayGame1>();
    }

    public void Update()
    {
        time += Time.deltaTime;
        if (time > 2.5f)
        {
            if (NumberDisplayGame1.Number>7)
            {
                SpawnPrefab();
                SpawnPrefab();
            }
            else if (NumberDisplayGame1.Number > 14)
            {
                SpawnPrefab();
                SpawnPrefab();
                SpawnPrefab();
            }
            else
            {
                SpawnPrefab();
            }
            time = 0f;
        }

    }

    public void SpawnPrefab()
    {
        // 在范围内生成随机位置
        float spawnX = Random.Range(0,2);
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

        // 生成预制体
        GameObject newPrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // 如果spawnX = RightX，就旋转预制体180度
        if (spawnX == RightX)
        {
            newPrefab.transform.Rotate(0, 180, 0);
        }

        // 检查当前预制体是否存在
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
}
