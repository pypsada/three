using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIName : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textComponent; // 引用物体上的Text组件
    public bool flag;

    void Start()
    {
        textComponent = GetComponent<Text>();
        if (Whole.AICareer == "King")
        {
            textComponent.text = "王权";
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (Whole.AICareer == "Assassin")
        {
            textComponent.text = "影刃";
            if (flag)
            {
                textComponent.text = "刃:";
            }
        }
        else if (Whole.AICareer == "Guard")
        {
            textComponent.text = "守护";
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (Whole.AICareer == "Turtle")
        {
            textComponent.text = "刺甲";
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (Whole.AICareer == "Rascally")
        {
            textComponent.text = "蹬鼻子上脸";
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (Whole.AICareer == "Arrogance")
        {
            textComponent.text = "中指";
            if (flag)
            {
                textComponent.text = "嘲讽值:";
            }
        }
        else if (Whole.AICareer == "Thief")
        {
            textComponent.text = "神偷";
            if (flag)
            {
                textComponent.text = "偷:";
            }
        }
        //else if (Whole.PlayerCareer == "Pangolin")
        //{
        //    textComponent.text = "叠甲";
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
