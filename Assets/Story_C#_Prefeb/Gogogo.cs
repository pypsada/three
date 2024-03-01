using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gogogo : MonoBehaviour
{
    private int record;

    void Start()
    {
        record = SaveGameManager.SaveData.record;
    }
    public void Go()
    {
        if (record < 120)
        {
            if (record < 70)
            {
                SaveGameManager.SaveData.record = 60;
                SceneManager.LoadScene("Act6");
            }
            else if (record < 80)
            {
                SaveGameManager.SaveData.record = 70;
                SceneManager.LoadScene("Act7");
            }
            else if (record < 90)
            {
                SaveGameManager.SaveData.record = 80;
                SceneManager.LoadScene("Act8");
            }
            else if (record < 100)
            {
                SaveGameManager.SaveData.record = 90;
                SceneManager.LoadScene("Act9");
            }
            else if (record < 110)
            {
                SaveGameManager.SaveData.record = 100;
                SceneManager.LoadScene("Act10");
            }
            else
            {
                SaveGameManager.SaveData.record = 110;
                SceneManager.LoadScene("Act11");
            }
        }
        else if (record < 180)
        {
            if (record < 130)
            {
                SaveGameManager.SaveData.record = 120;
                SceneManager.LoadScene("Act12");
            }
            else if (record < 140)
            {
                SaveGameManager.SaveData.record = 130;
                SceneManager.LoadScene("Act13");
            }
            else if (record < 150)
            {
                SaveGameManager.SaveData.record = 140;
                SceneManager.LoadScene("Act14");
            }
            else if (record < 160)
            {
                SaveGameManager.SaveData.record = 150;  //一般在主界面的Go就进入了，不用调用这一句
                SceneManager.LoadScene("Act15");
            }
            else if (record < 170)
            {
                SaveGameManager.SaveData.record = 160;
                SceneManager.LoadScene("Act16");
            }
            else
            {
                SaveGameManager.SaveData.record = 170;
                SceneManager.LoadScene("Act17");
            }
        }

    }
}
