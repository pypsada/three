using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    /*基地页面调用SaveGameManager所用*/

    public Text level;
    public Text game1;
    public Text game2;
    public Text game3;

    public void Show()
    {
        SaveData saveData = SaveGameManager.SaveData;
        if (saveData != null)
        {
            level.text = "搓客等级：<color=yellow>" + saveData.level.ToString() + "</color>";
            game1.text = "潜伏秘境纪录：<color=green>" + saveData.stealth.ToString() + "</color>";
            game2.text = "身法秘境纪录：<color=blue>" + saveData.agility.ToString() + "</color>";
            game3.text = "应变秘境纪录：<color=cyan>" + saveData.mathematics.ToString() + "</color>";
        }
    }
}
