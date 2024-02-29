using NetGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class NetCareer : MonoBehaviour
{
    public NetGame.Player player;
    // Start is called before the first frame update
    public Text textComponent; // 引用物体上的Text组件
    public bool flag;

    //private bool updateFlags = true;

    void Start()
    {
        //updateFlags = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!updateFlags) return;
        textComponent = GetComponent<Text>();
        if (player.tmpData.StringCareer == "King")
        {
            textComponent.text = "王权";
            //updateFlags = false;
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (player.tmpData.StringCareer == "Assassin")
        {
            textComponent.text = "影刃";
            //updateFlags = false;
            if (flag)
            {
                textComponent.text = "刃:";
            }
        }
        else if (player.tmpData.StringCareer == "Guard")
        {
            textComponent.text = "守护";
            //updateFlags = false;
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (player.tmpData.StringCareer == "Turtle")
        {
            textComponent.text = "刺甲";
            //updateFlags = false;
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (player.tmpData.StringCareer == "Rascally")
        {
            textComponent.text = "蹬鼻子上脸";
            //updateFlags = false;
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (player.tmpData.StringCareer == "Arrogance")
        {
            textComponent.text = "中指";
            //updateFlags = false;
            if (flag)
            {
                textComponent.text = "嘲讽值:";
            }
        }
        else if (player.tmpData.StringCareer == "Thief")
        {
            textComponent.text = "神偷";
            //updateFlags = false;
            if (flag)
            {
                textComponent.text = "偷:";
            }
        }
        else
        {
            //updateFlags = true;
        }
        //else if (player.tmpData.StringCareer == "Pangolin")
        //{
        //    textComponent.text = "叠甲";
        //}
    }
}
