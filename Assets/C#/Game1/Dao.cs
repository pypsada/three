using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dao : MonoBehaviour
{
    public GameObject prefab; // 预制体对象
    public Vector2 spawnRange; // 生成范围

    void Start()
    {
        SpawnPrefab();
    }

    public void SpawnPrefab()
    {
        // 在范围内生成随机位置
        float spawnX = Random.Range(-spawnRange.x, spawnRange.x);
        float spawnY = Random.Range(-spawnRange.y, spawnRange.y);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        // 生成预制体
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
