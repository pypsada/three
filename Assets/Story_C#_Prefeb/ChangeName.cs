using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour
{
    public GameObject pannel;
    public InputField newName;
    public GameObject next;
    public Text show;

    private void Start()
    {
        newName.text = SaveGameManager.Nickname;
    }
    public void Change()
    {
        if (PlayerPrefs.HasKey(newName.text) && !newName.text.Equals(SaveGameManager.Nickname))
        {
            pannel.SetActive(true);//昵称已存在，弹出提示框
            return;
        }

        string name1 = newName.text;  //改昵称

        SaveData savedata = SaveGameManager.SaveData;

        string avatar1 = savedata.avatarPath;
        int record1 = savedata.record;  //临时变量保护其他储存值
        int level1 = savedata.level;
        int game1 = savedata.stealth;
        int game2 = savedata.agility;
        int game3 = savedata.mathematics;

        int uid = savedata.UID;
        int vic = savedata.victory;
        int los = savedata.lose;

        PlayerPrefs.DeleteKey(SaveGameManager.Nickname);  //删除指定昵称的存档数据

        SaveGameManager.Nickname = name1;

        SaveGameManager.SaveData = new SaveData
        {
            nickname = SaveGameManager.Nickname,
            avatarPath = avatar1,
            record = record1,
            level = level1,
            stealth = game1,
            agility = game2,
            mathematics = game3,
            UID = uid,
            victory = vic,
            lose = los,
        };

        PlayerPrefs.SetString(name1, JsonUtility.ToJson(savedata));
        PlayerPrefs.Save();

        show.text = name1;
        next.SetActive(true);
        pannel.SetActive(false);

        PlayerPrefs.SetString("LastUsedSave", SaveGameManager.Nickname);
        PlayerPrefs.Save();
    }

    public void Close()
    {
        next.SetActive(true);
        pannel.SetActive(false);
    }
}
