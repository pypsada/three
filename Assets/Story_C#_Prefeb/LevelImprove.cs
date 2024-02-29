using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class LevelImprove : MonoBehaviour
{
    int a, b, c, r, m1, m2, m3, max, level;
    string show;
    public Text showTips;

    private void Start()
    {
        a = SaveGameManager.SaveData.stealth;
        b = SaveGameManager.SaveData.agility;
        c = SaveGameManager.SaveData.mathematics;
        r = SaveGameManager.SaveData.record;
        if (r < 60) 
            level = 0;
        else if (60 <= r)
        {
            if (a > 6) a = 6;
            if (b > 6) b = 6;
            if (c > 4) c = 4;
            level = a + b + 2 * c;
            max = 20; m1 = 6; m2 = 6; m3 = 4;
        }
        else if (80 <= r)
        {
            if (a > 12) a = 12;
            if (b > 12) b = 12;
            if (c > 8) c = 8;
            level = a + b + 2 * c;
            max = 40; m1 = 12; m2 = 12; m3 = 8;
        }
        else if (110 <= r)
        {
            if (a > 18) a = 18;
            if (b > 18) b = 18;
            if (c > 12) c = 12;
            level = a + b + 2 * c;
            max = 60; m1 = 18; m2 = 18; m3 = 12;
        }
        else if (140 <= r)
        {
            if (a > 24) a = 24;
            if (b > 24) b = 24;
            if (c > 16) c = 16;
            level = a + b + 2 * c;
            max = 80; m1 = 24; m2 = 24; m3 = 16;
        }
        else if (180 <= r)
        {
            if (a > 27) a = 27;
            if (b > 27) b = 27;
            if (c > 18) c = 18;
            level = a + b + 2 * c;
            max = 90; m1 = 27; m2 = 27; m3 = 18;
        }
        else if (210 <= r)
        {
            if (a > 30) a = 30;
            if (b > 30) b = 30;
            if (c > 20) c = 20;
            level = a + b + 2 * c;
            max = 100; m1 = 30; m2 = 30; m3 = 20;
        }
        SaveGameManager.SaveData.level = level;
        PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
        PlayerPrefs.Save();
        showTips.text = "当前主线可计入上限：" + "<color=yellow>" + max.ToString() + "</color>" + " / "
                        + "<color=cyan>" + m3.ToString() + "</color>" + " / "
                        + "<color=green>" + m1.ToString() + "</color>" + " / "
                        + "<color=blue>" + m2.ToString() + "</color>";
        GetComponent<Base>().Show();
    }
}
