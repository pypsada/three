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
            str = "刺客";
        else if (car == "Thief")
            str = "盗贼";
        else if (car == "Guard")
            str = "卫士";
        else if (car == "Arrogance")
            str = "傲慢之人";
        else if (car == "King")
            str = "国王";
        else if (car == "Turtle")
            str = "乌龟";
        else if (car == "Rascally")
            str = "老赖";
        my.text = "我方：<color=cyan>" + Whole.Characterlevel.ToString() + "</color>级刺客";
        your.text = "对手：<color=cyan>" + Whole.AICharacterlevel.ToString() + "</color>级" + str;
    }

}
