using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelImprove : MonoBehaviour
{
    int a, b, c, r, level;

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
        }
        else if (80 <= r)
        {
            if (a > 12) a = 12;
            if (b > 12) b = 12;
            if (c > 8) c = 8;
            level = a + b + 2 * c;
        }
        else if (110 <= r)
        {
            if (a > 18) a = 18;
            if (b > 18) b = 18;
            if (c > 12) c = 12;
            level = a + b + 2 * c;
        }
        else if (140 <= r)
        {
            if (a > 24) a = 24;
            if (b > 24) b = 24;
            if (c > 16) c = 16;
            level = a + b + 2 * c;
        }
        else if (180 <= r)
        {
            if (a > 27) a = 27;
            if (b > 27) b = 27;
            if (c > 18) c = 18;
            level = a + b + 2 * c;
        }
        else if (210 <= r)
        {
            if (a > 30) a = 30;
            if (b > 30) b = 30;
            if (c > 20) c = 20;
            level = a + b + 2 * c;
        }
        SaveGameManager.SaveData.level = level;
        PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
        PlayerPrefs.Save();
        GetComponent<Base>().Show();
    }
}
