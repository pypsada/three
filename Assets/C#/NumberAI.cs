using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAI : MonoBehaviour
{
    public Text textComponent; // 引用物体上的Text组件
    public AI AIScript; // 引用AI脚本
    public int energyValue;
    void Start()
    {
        // 获取物体上的Text组件
        textComponent = GetComponent<Text>();

        // 获取AI脚本的引用
        AIScript = FindObjectOfType<AI>();
    }

    void Update()
    {
        int energyValue = AIScript.Energy;
        textComponent.text = energyValue.ToString();
    }
}
