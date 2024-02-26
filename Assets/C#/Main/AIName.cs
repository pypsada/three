using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIName : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textComponent; // ���������ϵ�Text���
    public bool flag;

    void Start()
    {
        textComponent = GetComponent<Text>();
        if (Whole.AICareer == "King")
        {
            textComponent.text = "��Ȩ";
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (Whole.AICareer == "Assassin")
        {
            textComponent.text = "Ӱ��";
            if (flag)
            {
                textComponent.text = "��:";
            }
        }
        else if (Whole.AICareer == "Guard")
        {
            textComponent.text = "�ػ�";
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (Whole.AICareer == "Turtle")
        {
            textComponent.text = "�̼�";
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (Whole.AICareer == "Rascally")
        {
            textComponent.text = "�ű�������";
            if (flag)
            {
                gameObject.SetActive(false);
            }
        }
        else if (Whole.AICareer == "Arrogance")
        {
            textComponent.text = "��ָ";
            if (flag)
            {
                textComponent.text = "����ֵ:";
            }
        }
        else if (Whole.AICareer == "Thief")
        {
            textComponent.text = "��͵";
            if (flag)
            {
                textComponent.text = "͵:";
            }
        }
        //else if (Whole.PlayerCareer == "Pangolin")
        //{
        //    textComponent.text = "����";
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
