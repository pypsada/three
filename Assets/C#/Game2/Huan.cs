using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huan : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private int Ci;
    public PlayerGame2 PlayerGame2;
    public NumberDisplayGame1 NumberDisplayGame1;
    private void Start()
    {
        Ci = 0;
        EnableRandomChildren(transform.GetChild(0).GetChild(0), 5);
        PlayerGame2 = GetComponent<PlayerGame2>();
        NumberDisplayGame1 = FindObjectOfType<NumberDisplayGame1>();
    }

    void Update()
    {
        float rotationAmount = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, rotationAmount);
    }

    public void PanDing()
    {
        Ci += 1;
        StartCoroutine(DisableThenEnable());
    }


    IEnumerator DisableThenEnable()
    {
        DisableAllChildren(transform.GetChild(0).GetChild(0));
        yield return new WaitForSeconds(0.1f);

        if (NumberDisplayGame1.Number >= 10)
        {
            EnableRandomChildren(transform.GetChild(0).GetChild(0), 10);
        }
        else
        {
            EnableRandomChildren(transform.GetChild(0).GetChild(0), 5);
        }
    }

    void DisableAllChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }

    void EnableRandomChildren(Transform parent, int count)
    {
        // 获取所有子物体
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            children.Add(child);
        }

        // 随机启用指定数量的子物体
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, children.Count);
            GameObject childObject = children[randomIndex].gameObject;

            // 激活子物体
            childObject.SetActive(true);
            children.RemoveAt(randomIndex);
        }
    }
}