using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

//Used by Canvas
public class Connect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AddMsgListener();
        ClickConnect();
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    [Header("Ref")]
    public NetMain netMain;

    [Header("����")]
    public GameObject beforeConnect;
    public GameObject connecting;
    //public GameObject login;
    public GameObject connectSucc;
    public GameObject connectFail;

    //������Ӱ�ť
    public void ClickConnect()
    {
        NetManager.Connect(netMain.ip,netMain.port);
        beforeConnect.SetActive(false);
        connecting.SetActive(true);
    }

    private void ConnectSucc(string msg)
    {
        NetMain.actions.Enqueue(() =>
        {
            //Debug.Log(SaveGameManager.SaveData==null);
            //SaveGameManager.SaveData.UID = 0;
            //PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
            //PlayerPrefs.Save();
            NetManager.Send(new MsgPing());
            if (SaveGameManager.SaveData.UID == 0)
            {
                NetManager.Send(new MsgAskNewUid());
            }
            else
            {
                MsgLogin msgLogin = new();
                msgLogin.id = SaveGameManager.SaveData.UID.ToString();
                msgLogin.pw = SaveGameManager.SaveData.UID.ToString();
                msgLogin.nickName = SaveGameManager.Nickname;
                NetManager.Send(msgLogin);
            }
        });
    }

    private void ConnectSuccCompletely()
    {
        NetMain.actions.Enqueue(() =>
        {
            connectSucc.SetActive(true);
            connecting.SetActive(false);
            OnClickConnSucc();
        });
    }

    private void ConnectFail(string msg)
    {
        NetMain.actions.Enqueue(() =>
        {
            connectFail.SetActive(true);
            connecting.SetActive(false);
        });
    }

    //�������ӳɹ��İ�ť
    public void OnClickConnSucc()
    {
        connectSucc.SetActive(false);
        //login.SetActive(true);
        RemoveMsgListener();
        SceneManager.LoadScene("GameRooms");
    }

    //��������ʧ�ܵİ�ť
    public void OnClickConnFail()
    {
        ClickClose();
        connectFail.SetActive(false);
        beforeConnect.SetActive(true);
        RemoveMsgListener();
        SceneManager.LoadScene("FightPrepare");
    }

    //��������رհ�ť
    public void ClickClose()
    {
        Debug.Log("�ǳ�");
        NetManager.Close();
        NetManager.ping = -1;
        //login.SetActive(false);
        beforeConnect.SetActive(true);
        AddMsgListener();
    }

    private void AddMsgListener()
    {
        NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, ConnectSucc);
        NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, ConnectFail);
        NetManager.AddMsgListener("MsgLogin", OnLogin);
        NetManager.AddMsgListener("MsgRegister", OnRegister);
        NetManager.AddMsgListener("MsgAskNewUid", GetNewUid);
    }

    private void RemoveMsgListener()
    {
        NetManager.RemoveEventListener(NetManager.NetEvent.ConnectSucc, ConnectSucc);
        NetManager.RemoveEventListener(NetManager.NetEvent.ConnectFail, ConnectFail);
        NetManager.RemoveMsgListener("MsgLogin", OnLogin);
        NetManager.RemoveMsgListener("MsgRegister", OnRegister);
        NetManager.RemoveMsgListener("MsgAskNewUid", GetNewUid);
    }

    private void OnLogin(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            MsgLogin msgLogin = (MsgLogin)msgBase;
            if (msgLogin.result == 0)
            {
                //��¼�ɹ�
                Debug.Log("��¼�ɹ�");
                ConnectSuccCompletely();
                //RemoveMsgListener();
                //SceneManager.LoadScene("GameRooms");
            }
            else if (msgLogin.result == 1)
            {
                //��¼ʧ��
                Debug.Log("��¼ʧ��");
                NetManager.Close();
                ConnectFail("��¼ʧ��");
            }
        });
    }

    private void OnRegister(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            MsgRegister msg = (MsgRegister)msgBase;
            if (msg.result == 0)
            {
                //ע��ɹ�
                Debug.Log("ע��ɹ�");

                MsgLogin msgLogin = new();
                msgLogin.id = SaveGameManager.SaveData.UID.ToString();
                msgLogin.pw = SaveGameManager.SaveData.UID.ToString();
                msgLogin.nickName = SaveGameManager.Nickname;
                NetManager.Send(msgLogin);
            }
            else if (msg.result == 1)
            {
                //ע��ʧ��
                Debug.Log("ע��ʧ��");

                SaveGameManager.SaveData.UID = 0;
                PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
                PlayerPrefs.Save();

                NetManager.Close();
                ConnectFail("ע��ʧ��");
            }
        });  
    }

    private void GetNewUid(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            MsgAskNewUid msg = (MsgAskNewUid)msgBase;
            SaveGameManager.SaveData.UID = msg.newUid;
            PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
            PlayerPrefs.Save();

            MsgRegister msgRigiser = new();
            msgRigiser.id = SaveGameManager.SaveData.UID.ToString();
            msgRigiser.pw = SaveGameManager.SaveData.UID.ToString();
            NetManager.Send(msgRigiser);
        });
    }
}
