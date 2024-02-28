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

        }

    }
}
