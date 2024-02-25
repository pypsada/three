using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

/*�浵���Ĵ���*/

[System.Serializable]
public class SaveData
{
    public string nickname;
    public string avatarPath;
    public int record;
    public int level;
    public int stealth;
    public int agility;
    public int mathematics;
}

public class SaveGameManager : MonoBehaviour
{
    public GameObject createPannel;  //�����浵�����
    public InputField nicknameInput;  //�����ǳƵ���
    public Dropdown avatorValue;  //ѡ��ͷ�����

    public GameObject changePannel;
    public InputField nicknameChoice;  //ѡ���ǳƵ���

    public GameObject deletePannel;
    public InputField DeleteChoice;  //ɾ���ǳ�ѡ��

    public GameObject tipPannel;  //������ʾ���
    public GameObject tipCreate;
    public GameObject tipLoad;
    public Text WhoLogOut;
    public GameObject tipLogOut;

    public GameObject magicPannel;  //�����޸���
    public Text originName;
    public InputField magicName;
    public Image originAvatar;
    public Dropdown magicAvatar;
    public Text originRecord;
    public InputField magicRecord;
    public Text originLevel;
    public InputField magicLevel;
    public InputField stealth;
    public InputField agility;
    public InputField mathematics;

    public Text showName;  //��ʾ����
    public Image showAvatar;  //��ʾͷ��
    public Text showRecord;  //��ʾ��Ϸ���߽���
    public Text showLevel;  //��ʾ��������
    public Text showStealth;
    public Text showAgility;
    public Text showMathematics;

    private const string LastUsedSaveKey = "LastUsedSave";  //�Զ���¼ϵͳ
    private string lastUsedSave;

    public static SaveData SaveData;  //��ʱ��saveData
    public static bool IsLoggedIn = false;
    public static string Nickname;  //ȫ�ֱ��������ڵ���������ڵĴ浵

    private void Load(string nickname)  //��ʾ����
    {
        string saveDataJson = PlayerPrefs.GetString(nickname, "");
        if (!string.IsNullOrEmpty(saveDataJson))
        {
            SaveData = JsonUtility.FromJson<SaveData>(saveDataJson);  //�ڽ�������ʾ�浵��Ϣ
            showName.text = SaveData.nickname;  //�����浵���ݵ���ʾ
            Sprite avatarSprite = Resources.Load<Sprite>(SaveData.avatarPath);
            showAvatar.sprite = avatarSprite;  //��ʾͷ��
            showRecord.text = "����" + SaveData.record.ToString() + "��";
            DeleteChoice.text = SaveData.nickname;  //ɾ��Ԥ����
            showLevel.text = SaveData.level.ToString() + "��";
            showStealth.text = SaveData.stealth.ToString() + "��";
            showAgility.text = SaveData.agility.ToString() + "��";
            showMathematics.text = SaveData.mathematics.ToString() + "��";
        }
        IsLoggedIn = true;
    }

    public void ChangeSave()  //�������д浵
    {
        if (!PlayerPrefs.HasKey(nicknameChoice.text))
        {
            tipLoad.SetActive(true);//�浵�����ڣ�������ʾ��
            return;
        }
        Nickname = nicknameChoice.text;
        Load(Nickname);
        nicknameChoice.text = null;
        changePannel.SetActive(false);

        PlayerPrefs.SetString(LastUsedSaveKey, Nickname);
        PlayerPrefs.Save();
    }

    public void LogOutPannel()
    {
        if (IsLoggedIn)
        {
            tipLogOut.SetActive(true);
            WhoLogOut.text = "�浵��<color=blue>" + Nickname + "</color>";
        }
    }

    public void LogOut()  //�˳���¼
    {
        IsLoggedIn = false;  //���µ�¼״̬Ϊδ��¼
        nicknameChoice.text = Nickname;  //�ص�Ԥ��
        Nickname = null;
        SaveData = null;

        showName.text = "������";  //�ֶ���ʼ������
        showAvatar.sprite = null;
        showRecord.text = "������";
        showLevel.text = "������";
        showStealth.text = "������";
        showAgility.text = "������";
        showMathematics.text = "������";

        tipLogOut.SetActive(false);

        PlayerPrefs.SetString(LastUsedSaveKey, null);
        PlayerPrefs.Save();
    }

    public void WrongPlease()  //�����浵ʱ����
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

    public void CreateSave()  //�����浵
    {
        if (PlayerPrefs.HasKey(nicknameInput.text))
        {
            tipCreate.SetActive(true);//�ǳ��Ѵ��ڣ�������ʾ��
            return;
        }

        Nickname = nicknameInput.text;
        string avatarPath;
        if (avatorValue.value == 0)  //��ȡͷ��ͼƬ��·��
            avatarPath = "MrWu";
        else
            avatarPath = "MissMei";
        SaveData = new SaveData
        {
            nickname = Nickname,
            avatarPath = avatarPath,
            record = 0,
            level = 1,
            stealth = 0,
            agility = 0,
            mathematics = 0,
        };
        string saveDataJson = JsonUtility.ToJson(SaveData);
        PlayerPrefs.SetString(Nickname, saveDataJson);
        PlayerPrefs.Save();   //����浵����

        Load(Nickname);

        nicknameInput.text = null;
        avatorValue.value = 0;

        createPannel.SetActive(false);  //�����������رս���

        PlayerPrefs.SetString(LastUsedSaveKey, Nickname);
        PlayerPrefs.Save();
    }

    public void DeleteSave()  //ɾ���浵
    {
        if (PlayerPrefs.HasKey(DeleteChoice.text))
        {
            PlayerPrefs.DeleteKey(DeleteChoice.text);  //ɾ��ָ���ǳƵĴ浵����
            IsLoggedIn = false;  //���µ�¼״̬Ϊδ��¼
            Nickname = null;
            SaveData = null;

            showName.text = "������";  //�ֶ���ʼ������һ��
            showAvatar.sprite = null;
            showRecord.text = "������";
            showLevel.text = "������";
            showStealth.text = "������";
            showAgility.text = "������";
            showMathematics.text = "������";

            deletePannel.SetActive(false);

            PlayerPrefs.SetString(LastUsedSaveKey, null);
            PlayerPrefs.Save();
        }
    }
    
    private void Start()  //��ʼ������
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

    private void Update()  //�����޸���
    {
        if (Nickname != null)
        {
            if (UnityEngine.Input.GetKey(KeyCode.O) &&
                UnityEngine.Input.GetKey(KeyCode.P))
            {
                magicPannel.SetActive(true);
                originName.text = SaveData.nickname;
                Sprite avatarSprite = Resources.Load<Sprite>(SaveData.avatarPath);
                originAvatar.sprite = avatarSprite;  //��ʾͷ��
                originRecord.text = SaveData.record.ToString() + "��";
                originLevel.text = SaveData.level.ToString() + "��";
                
                magicName.text = SaveData.nickname;  //Ԥ����
                if (SaveData.avatarPath == "MrWu")
                    magicAvatar.value = 0;
                else
                    magicAvatar.value = 1;
                magicRecord.text = SaveData.record.ToString();
                magicLevel.text = SaveData.level.ToString();
                stealth.text = SaveData.stealth.ToString();
                agility.text = SaveData.agility.ToString();
                mathematics.text = SaveData.mathematics.ToString();
            }
        }
    }

    public void Magic()
    {
        if (PlayerPrefs.HasKey(magicName.text) && !magicName.text.Equals(Nickname))
        {
            tipPannel.SetActive(true);//�ǳ��Ѵ��ڣ�������ʾ��
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
        if (int.TryParse(attack1, out int result2) && result2 <= 1000 && result2 >= 1)
            attack2 = result2;
        else
            attack2 = 1;

        string magicGame1 = stealth.text;  //The Fifth
        int game1;
        if (int.TryParse(magicGame1, out int result3) && result3 <= 100 && result3 >= 0)
            game1 = result3;
        else
            game1 = 0;
        string magicGame2 = agility.text;  //The Sixth
        int game2;
        if (int.TryParse(magicGame2, out int result4) && result4 <= 100 && result4 >= 0)
            game2 = result4;
        else
            game2 = 0;
        string magicGame3 = mathematics.text;  //The Seventh
        int game3;
        if (int.TryParse(magicGame3, out int result5) && result5 <= 100 && result5 >= 0)
            game3 = result5;
        else
            game3 = 0;

        SaveData = new SaveData
        {
            nickname = Nickname,
            avatarPath = avatarPath,
            record = record2,
            level = attack2,
            stealth = game1,
            agility = game2,
            mathematics = game3,
        };

        PlayerPrefs.SetString(Nickname, JsonUtility.ToJson(SaveData));
        PlayerPrefs.Save();

        Load(Nickname);

        magicPannel.SetActive(false);

        PlayerPrefs.SetString(LastUsedSaveKey, Nickname);
        PlayerPrefs.Save();
    }
}