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
            pannel.SetActive(true);//�ǳ��Ѵ��ڣ�������ʾ��
            return;
        }

        string name1 = newName.text;  //���ǳ�

        SaveData savedata = SaveGameManager.SaveData;

        string avatar1 = savedata.avatarPath;
        int record1 = savedata.record;  //��ʱ����������������ֵ
        int level1 = savedata.level;
        int game1 = savedata.stealth;
        int game2 = savedata.agility;
        int game3 = savedata.mathematics;

        int uid = savedata.UID;
        int vic = savedata.victory;
        int los = savedata.lose;

        PlayerPrefs.DeleteKey(SaveGameManager.Nickname);  //ɾ��ָ���ǳƵĴ浵����

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
