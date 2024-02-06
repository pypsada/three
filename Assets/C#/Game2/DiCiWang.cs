using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiCiWang : MonoBehaviour
{
    public GameObject prefab; // 预制体对象
    public float spawnRadius; // 生成范围的半径

    void Start()
    {
        SpawnPrefab();
    }

    public void SpawnPrefab()
    {
        // 在圆形范围内生成随机位置
        Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;

        // 生成预制体
        GameObject newPrefab = Instantiate(prefab, new Vector3(spawnPosition.x, spawnPosition.y, 0f), Quaternion.identity);

        // 将生成的预制体设置为当前物体的子物体
        newPrefab.transform.parent = transform;
    }
}