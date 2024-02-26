using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AINumber : MonoBehaviour
{
    public Text textComponent; // 引用物体上的Text组件
    public AI AI; // 引用AI脚本
    public int energyValue;
    public bool Career;
    void Start()
    {
        // 获取物体上的Text组件
        textComponent = GetComponent<Text>();

        // 获取AI脚本的引用
        AI = FindObjectOfType<AI>();
    }

    void Update()
    {
        if (Career)
        {
            if (Whole.AICareer == "Arrogance")
            {
                int CareerNumber = AI.ArroganceNumber;
                textComponent.text = CareerNumber.ToString();  //显示技能点数
            }
            else
            {
                int CareerNumber = AI.Career;
                textComponent.text = CareerNumber.ToString();  //显示技能点数
            }

        }
        else
        {
            int energyValue = AI.Energy;
            textComponent.text = energyValue.ToString();  //显示能量点数
        }

    }
}