using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AINumber : MonoBehaviour
{
    public Text textComponent; // ���������ϵ�Text���
    public AI AI; // ����AI�ű�
    public int energyValue;
    public bool Career;
    void Start()
    {
        // ��ȡ�����ϵ�Text���
        textComponent = GetComponent<Text>();

        // ��ȡAI�ű�������
        AI = FindObjectOfType<AI>();
    }

    void Update()
    {
        if (Career)
        {
            if (Whole.AICareer == "Arrogance")
            {
                int CareerNumber = AI.ArroganceNumber;
                textComponent.text = CareerNumber.ToString();  //��ʾ���ܵ���
            }
            else
            {
                int CareerNumber = AI.Career;
                textComponent.text = CareerNumber.ToString();  //��ʾ���ܵ���
            }

        }
        else
        {
            int energyValue = AI.Energy;
            textComponent.text = energyValue.ToString();  //��ʾ��������
        }

    }
}