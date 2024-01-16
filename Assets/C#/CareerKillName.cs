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
            textComponent.text = "能防";
        }
        else if (Whole.PlayerCareer == "Turtle")
        {
            textComponent.text = "龟缩";
        }
        else if (Whole.PlayerCareer == "Rascally")
        {
            textComponent.text = "汲能";
        }
        else if (Whole.PlayerCareer == "Arrogance")
        {
            textComponent.text = "嘲讽";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
