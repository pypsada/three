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
        int a, b, c, id; float rate; string level;
        showName.text = SaveGameManager.Nickname;
        id = SaveGameManager.SaveData.UID;
        if (id == 0)
            showUID.text = "Œ¥≤Œ”Î";
        else
            showUID.text = id.ToString();
        a = SaveGameManager.SaveData.victory;
        b = SaveGameManager.SaveData.lose;
        c = a + b;
        rate = (float)a / b;
        showVictory.text = a.ToString();
        showLose.text = b.ToString();
        showTotal.text = c.ToString();
        showRate.text = rate.ToString() + "%";
        if (c == 0)
            level = levelRank[0];
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
