using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CareerKillName : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textComponent; // 引用物体上的Text组件

    void Start()
    {
        textComponent = GetComponent<Text>();
        if (Whole.PlayerCareer=="King")
        {
            textComponent.text = "王权";
        }
        else if (Whole.PlayerCareer== "Assassin")
        {
            textComponent.text = "暗杀";
        }
        else if (Whole.PlayerCareer == "Guard")
        {
            textComponent.text = "守护";
        }
        else if (Whole.PlayerCareer == "Turtle")
        {
            textComponent.text = "刺甲";
        }
        else if (Whole.PlayerCareer == "Rascally")
        {
            textComponent.text = "蹬鼻子上脸";
        }
        else if (Whole.PlayerCareer == "Arrogance")
        {
            textComponent.text = "中指";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
