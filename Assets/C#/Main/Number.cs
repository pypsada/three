using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public Text textComponent; // ���������ϵ�Text���
    public Player playerScript; // ����Player�ű�
    public int energyValue;
    public bool Career;
    void Start()
    {
        // ��ȡ�����ϵ�Text���
        textComponent = GetComponent<Text>();

        // ��ȡPlayer�ű�������
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (Career)
        {
            int CareerNumber = playerScript.Career;
            textComponent.text=CareerNumber.ToString();  //��ʾ���ܵ���
        }
        else
        {
            int energyValue = playerScript.Energy;
            textComponent.text = energyValue.ToString();  //��ʾ��������
        }

    }
}