using System;
using System.Collections;
using System.Collections.Generic;
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
    public float defence;
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

    public GameObject tipPannel;
    public GameObject tipCreate;
    public GameObject tipLoad;

    public GameObject magicPannel;  //�����޸���
    public Text originName;
    public InputField magicName;
    public Image originAvatar;
    public Dropdown magicAvatar;
    public Text originRecord;
    public InputField magicRecord;
    public Text originLevel;
    public InputField magicLevel;

    public Text showName;  //��ʾ����
    public Image showAvatar;  //��ʾͷ��
    public Text showRecord;  //��ʾ��Ϸ���߽���

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
            defence = 1.0f
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
        PlayerPrefs.DeleteKey(DeleteChoice.text);  //ɾ��ָ���ǳƵĴ浵����
        IsLoggedIn = false;  //���µ�¼״̬Ϊδ��¼
        Nickname = null;
        SaveData = null;

        showName.text = "������";  //�ֶ���ʼ������һ��
        showAvatar.sprite = null;
        showRecord.text = "������";
        deletePannel.SetActive(false);
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
