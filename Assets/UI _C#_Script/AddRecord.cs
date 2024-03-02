using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRecord : MonoBehaviour
{
    /*增加历练度专属代码*/

    public int increment = 10;

    public void Add()
    {
        Time.timeScale = 1;
        if (SaveGameManager.SaveData.record < 250)
        {
            SaveGameManager.SaveData.record += increment;
            PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
            PlayerPrefs.Save();
        }
    }
}
