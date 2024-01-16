using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CareerKillName : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textComponent; // ���������ϵ�Text���

    void Start()
    {
        textComponent = GetComponent<Text>();
        if (Whole.PlayerCareer=="King")
        {
            textComponent.text = "��Ȩ";
        }
        else if (Whole.PlayerCareer== "Assassin")
        {
            textComponent.text = "��ɱ";
        }
        else if (Whole.PlayerCareer == "Guard")
        {
            textComponent.text = "�ܷ�";
        }
        else if (Whole.PlayerCareer == "Turtle")
        {
            textComponent.text = "����";
        }
        else if (Whole.PlayerCareer == "Rascally")
        {
            textComponent.text = "����";
        }
        else if (Whole.PlayerCareer == "Arrogance")
        {
            textComponent.text = "����";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
