using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightShow : MonoBehaviour
{
    public Text my;
    public Text your;
    string car, str;

    void Start()
    {
        car = Whole.AICareer;
        if (car == "Assassin")
            str = "�̿�";
        else if (car == "Thief")
            str = "����";
        else if (car == "Guard")
            str = "��ʿ";
        else if (car == "Arrogance")
            str = "����֮��";
        else if (car == "King")
            str = "����";
        else if (car == "Turtle")
            str = "�ڹ�";
        else if (car == "Rascally")
            str = "����";
        my.text = "�ҷ���<color=cyan>" + Whole.Characterlevel.ToString() + "</color>���̿�";
        your.text = "���֣�<color=cyan>" + Whole.AICharacterlevel.ToString() + "</color>��" + str;
    }

}
