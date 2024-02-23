using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

/*存档核心代码*/

[System.Serializable]
public class SaveData
{
    public string nickname;
    public string avatarPath;
    public int record;
    public int level;
    public float defence;
}

public class SaveGameManager : MonoBehaviour
{
    public GameObject createPannel;  //创建存档的面板
    public InputField nicknameInput;  //输入昵称的栏
    public Dropdown avatorValue;  //选择头像的栏

    public GameObject changePannel;
    public InputField nicknameChoice;  //选择昵称调出

    public GameObject deletePannel;
    public InputField DeleteChoice;  //删档昵称选择

    public GameObject tipPannel;
    public GameObject tipCreate;
    public GameObject tipLoad;

    public GameObject magicPannel;  //内置修改器
    public Text originName;
    public InputField magicName;
    public Image originAvatar;
    public Dropdown magicAvatar;
    public Text originRecord;
    public InputField magicRecord;
    public Text originLevel;
    public InputField magicLevel;

    public Text showName;  //显示名字
    public Image showAvatar;  //显示头像
    public Text showRecord;  //显示游戏主线进度

    private const string LastUsedSaveKey = "LastUsedSave";  //自动登录系统
    private string lastUsedSave;

    public static SaveData SaveData;  //此时的saveData
    public static bool IsLoggedIn = false;
    public static string Nickname;  //全局变量，用于调配这个现在的存档

    private void Load(string nickname)  //显示数据
    {
        string saveDataJson = PlayerPrefs.GetString(nickname, "");
        if (!string.IsNullOrEmpty(saveDataJson))
        {
            SaveData = JsonUtility.FromJson<SaveData>(saveDataJson);  //在界面上显示存档信息
            showName.text = SaveData.nickname;  //其他存档数据的显示
            Sprite avatarSprite = Resources.Load<Sprite>(SaveData.avatarPath);
            showAvatar.sprite = avatarSprite;  //显示头像
            showRecord.text = "进度" + SaveData.record.ToString() + "点";
            DeleteChoice.text = SaveData.nickname;  //删档预输入
        }
        IsLoggedIn = true;
    }

    public void ChangeSave()  //加载已有存档
    {
        if (!PlayerPrefs.HasKey(nicknameChoice.text))
        {
            tipLoad.SetActive(true);//存档不存在，弹出提示框
            return;
        }
        Nickname = nicknameChoice.text;
        Load(Nickname);
        nicknameChoice.text = null;
        changePannel.SetActive(false);

        PlayerPrefs.SetString(LastUsedSaveKey, Nickname);
        PlayerPrefs.Save();
    }

    public void WrongPlease()  //创建存档时重名
    {
        Nickname = nicknameInput.text;
        Load(Nickname);
        nicknameInput.text = null;
        avatorValue.value = 0;
        tipCreate.SetActive(false);
        createPannel.SetActive(false);

        PlayerPrefs.SetString(LastUsedSaveKey, Nickname);
        PlayerPrefs.Save();
    }

    public void CreateSave()  //创建存档
    {
        if (PlayerPrefs.HasKey(nicknameInput.text))
        {
            tipCreate.SetActive(true);//昵称已存在，弹出提示框
            return;
        }

        Nickname = nicknameInput.text;
        string avatarPath;
        if (avatorValue.value == 0)  //获取头像图片的路径
            avatarPath = "MrWu";
        else
            avatarPath = "MissMei";
        SaveData = new SaveData
        {
            nickname = Nickname,
            avatarPath = avatarPath,
            record = 0,
            level = 1,
            defence = 1.0f
        };
        string saveDataJson = JsonUtility.ToJson(SaveData);
        PlayerPrefs.SetString(Nickname, saveDataJson);
        PlayerPrefs.Save();   //保存存档数据

        Load(Nickname);

        nicknameInput.text = null;
        avatorValue.value = 0;

        createPannel.SetActive(false);  //创建结束，关闭界面

        PlayerPrefs.SetString(LastUsedSaveKey, Nickname);
        PlayerPrefs.Save();
    }

    public void DeleteSave()  //删除存档
    {
        PlayerPrefs.DeleteKey(DeleteChoice.text);  //删除指定昵称的存档数据
        IsLoggedIn = false;  //更新登录状态为未登录
        Nickname = null;
        SaveData = null;

        showName.text = "？？？";  //手动初始化界面一下
        showAvatar.sprite = null;
        showRecord.text = "？？？";
        deletePannel.SetActive(false);
    }
    
    private void Start()  //初始化界面
    {
        lastUsedSave = PlayerPrefs.GetString(LastUsedSaveKey, "");
        if (!string.IsNullOrEmpty(lastUsedSave) && PlayerPrefs.HasKey(lastUsedSave))
        {
            Nickname = lastUsedSave;
            IsLoggedIn = true;
        }
        else
            createPannel.SetActive(true);

        if (IsLoggedIn)
            Load(Nickname);
        else
            createPannel.SetActive(true);
    }

    private void Update()  //内置修改器
    {
        if (Nickname != null)
        {
            if (UnityEngine.Input.GetKey(KeyCode.O) &&
                UnityEngine.Input.GetKey(KeyCode.P))
            {
                magicPannel.SetActive(true);
                originName.text = SaveData.nickname;
                Sprite avatarSprite = Resources.Load<Sprite>(SaveData.avatarPath);
                originAvatar.sprite = avatarSprite;  //显示头像
                originRecord.text = SaveData.record.ToString() + "点";
                originLevel.text = SaveData.level.ToString() + "级";
                
                magicName.text = SaveData.nickname;  //预输入
                if (SaveData.avatarPath == "MrWu")
                    magicAvatar.value = 0;
                else
                    magicAvatar.value = 1;
                magicRecord.text = SaveData.record.ToString();
                magicLevel.text = SaveData.level.ToString();
            }
        }
    }

    public void Magic()
    {
        if (PlayerPrefs.HasKey(magicName.text) && !magicName.text.Equals(Nickname))
        {
            tipPannel.SetActive(true);//昵称已存在，弹出提示框
            return;
        }

        DeleteSave();  //The First
        Nickname = magicName.text;

        string avatarPath;  //The Second
        if (magicAvatar.value == 0)
            avatarPath = "MrWu";
        else
            avatarPath = "MissMei";

        string record1 = magicRecord.text;  //The Third
        int record2;
        if (int.TryParse(record1, out int result) && result <= 1000 && result >= 0)
            record2 = result;
        else
            record2 = 0;

        string attack1 = magicLevel.text;  //The Fourth
        int attack2;
        if (int.TryParse(attack1, out int result2) && result2 <= 1000 && result2 >= 0)
            attack2 = result2;
        else
            attack2 = 1;

        SaveData = new SaveData
        {
            nickname = Nickname,
            avatarPath = avatarPath,
            record = record2,
            level = attack2,
            defence = 1.0f
        };

        PlayerPrefs.SetString(Nickname, JsonUtility.ToJson(SaveData));
        PlayerPrefs.Save();

        Load(Nickname);

        magicPannel.SetActive(false);

        PlayerPrefs.SetString(LastUsedSaveKey, Nickname);
        PlayerPrefs.Save();
    }
}
