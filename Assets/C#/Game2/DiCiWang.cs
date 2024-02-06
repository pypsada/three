using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiCiWang : MonoBehaviour
{
    public GameObject prefab; // Ԥ�������
    public float spawnRadius; // ���ɷ�Χ�İ뾶

    void Start()
    {
        SpawnPrefab();
    }

    public void SpawnPrefab()
    {
        // ��Բ�η�Χ���������λ��
        Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;

        // ����Ԥ����
        GameObject newPrefab = Instantiate(prefab, new Vector3(spawnPosition.x, spawnPosition.y, 0f), Quaternion.identity);

        // �����ɵ�Ԥ��������Ϊ��ǰ�����������
        newPrefab.transform.parent = transform;
    }
}