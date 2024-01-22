using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public Text textComponent; // 引用物体上的Text组件
    public Player playerScript; // 引用Player脚本
    public int energyValue;
    public bool Career;
    void Start()
    {
        // 获取物体上的Text组件
        textComponent = GetComponent<Text>();

        // 获取Player脚本的引用
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (Career)
        {
            int CareerNumber = playerScript.Career;
            textComponent.text=CareerNumber.ToString();  //显示技能点数
        }
        else
        {
            int energyValue = playerScript.Energy;
            textComponent.text = energyValue.ToString();  //显示能量点数
        }

    }
}