using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class NumberDisplayGame1 : MonoBehaviour
{
    public Text textComponent; // 引用物体上的Text组件
    public int Number;
    void Start()
    {
        textComponent = GetComponent<Text>();
        Number = 0;
    }

    void Update()
    {
        textComponent.text = Number.ToString();  //显示技能点数
    }
}