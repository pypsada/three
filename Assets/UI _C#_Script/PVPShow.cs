using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PVPShow : MonoBehaviour
{
    public Text showName;
    public Text showUID;
    public Text showVictory;
    public Text showLose;
    public Text showTotal;
    public Text showRate;
    public Text showLevel;
    public string[] levelRank;

    void Start()
    {
        SaveData saveData = SaveGameManager.SaveData;
        int a, b, c, id; float rate; string level;
        showName.text = saveData.nickname;
        id = saveData.UID;
        if (id == 0)
            showUID.text = "未参与";
        else if (id < 10)
            showUID.text = "0000" + id.ToString();
        else if (id < 100)
            showUID.text = "000" + id.ToString();
        else if (id < 1000)
            showUID.text = "00" + id.ToString();
        else if (id < 10000)
            showUID.text = "0" + id.ToString();
        else
            showUID.text = id.ToString();
        a = saveData.victory;
        b = saveData.lose;
        c = a + b;
        rate = (float)a / c;
        showVictory.text = a.ToString();
        showLose.text = b.ToString();
        showTotal.text = c.ToString();
        if (c != 0)
            showRate.text = rate.ToString() + "%";
        if (c == 0)
        {
            showRate.text = "未参与";
            level = levelRank[0];
        }
        else if (c <= 5)
            level = levelRank[1];
        else if (c <= 10)
            level = levelRank[2];
        else if (c <= 20)
            level = levelRank[3];
        else if (c <= 30)
            level = levelRank[4];
        else
            level = levelRank[5];
        showLevel.text = level;
    }
}
