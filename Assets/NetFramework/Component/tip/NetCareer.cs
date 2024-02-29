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
    public Text textComponent; // ���������ϵ�Text���
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
            textComponent.text = "��Ȩ";
            //updateFlags = false;
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (player.tmpData.StringCareer == "Assassin")
        {
            textComponent.text = "Ӱ��";
            //updateFlags = false;
            if (flag)
            {
                textComponent.text = "��:";
            }
        }
        else if (player.tmpData.StringCareer == "Guard")
        {
            textComponent.text = "�ػ�";
            //updateFlags = false;
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (player.tmpData.StringCareer == "Turtle")
        {
            textComponent.text = "�̼�";
            //updateFlags = false;
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (player.tmpData.StringCareer == "Rascally")
        {
            textComponent.text = "�ű�������";
            //updateFlags = false;
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (player.tmpData.StringCareer == "Arrogance")
        {
            textComponent.text = "��ָ";
            //updateFlags = false;
            if (flag)
            {
                textComponent.text = "����ֵ:";
            }
        }
        else if (player.tmpData.StringCareer == "Thief")
        {
            textComponent.text = "��͵";
            //updateFlags = false;
            if (flag)
            {
                textComponent.text = "͵:";
            }
        }
        else
        {
            //updateFlags = true;
        }
        //else if (player.tmpData.StringCareer == "Pangolin")
        //{
        //    textComponent.text = "����";
        //}
    }
}
