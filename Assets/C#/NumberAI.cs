using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAI : MonoBehaviour
{
    public Text textComponent; // ���������ϵ�Text���
    public AI AIScript; // ����AI�ű�
    public int energyValue;
    public bool Career;
    void Start()
    {
        // ��ȡ�����ϵ�Text���
        textComponent = GetComponent<Text>();

        // ��ȡAI�ű�������
        AIScript = FindObjectOfType<AI>();
    }

    void Update()
    {
        if (Career)
        {
            int energyValue = AIScript.Career;
            textComponent.text = energyValue.ToString();  //��ʾ���ܵ���
        }
        else
        {
            int energyValue = AIScript.Energy;
            textComponent.text = energyValue.ToString();  //��ʾ��������
        }

    }
}
