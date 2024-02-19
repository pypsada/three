using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    private List<Transform> childObjects = new List<Transform>();
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 6f)
        {
            Chose();
            time = 0f;
        }
    }

    public void Chose()
    {
        // 清空子物体列表
        childObjects.Clear();

        // 获取所有未启用的子物体
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                childObjects.Add(child);
            }
        }

        // 随机选择一个子物体启用，并传送至(0,0,0)
        int index = Random.Range(0, childObjects.Count);
        Transform selectedChild = childObjects[index];
        selectedChild.transform.position = Vector3.zero;
        selectedChild.gameObject.SetActive(true);
    }
}