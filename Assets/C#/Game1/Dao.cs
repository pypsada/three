using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dao : MonoBehaviour
{
    public GameObject prefab; // Ԥ�������
    public Vector2 spawnRange; // ���ɷ�Χ

    void Start()
    {
        SpawnPrefab();
    }

    public void SpawnPrefab()
    {
        // �ڷ�Χ���������λ��
        float spawnX = Random.Range(-spawnRange.x, spawnRange.x);
        float spawnY = Random.Range(-spawnRange.y, spawnRange.y);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        // ����Ԥ����
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
