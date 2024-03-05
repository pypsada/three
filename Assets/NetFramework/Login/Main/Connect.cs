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

    [Header("界面")]
    public GameObject beforeConnect;
    public GameObject connecting;
    //public GameObject login;
    public GameObject connectSucc;
    public GameObject connectFail;

    //点击连接按钮
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

    //按下连接成功的按钮
    public void OnClickConnSucc()
    {
        connectSucc.SetActive(false);
        //login.SetActive(true);
        RemoveMsgListener();
        SceneManager.LoadScene("GameRooms");
    }

    //按下连接失败的按钮
    public void OnClickConnFail()
    {
        ClickClose();
        connectFail.SetActive(false);
        beforeConnect.SetActive(true);
        RemoveMsgListener();
        SceneManager.LoadScene("FightPrepare");
    }

    //主动点击关闭按钮
    public void ClickClose()
    {
        Debug.Log("登出");
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
                //登录成功
                Debug.Log("登录成功");
                ConnectSuccCompletely();
                //RemoveMsgListener();
                //SceneManager.LoadScene("GameRooms");
            }
            else if (msgLogin.result == 1)
            {
                //登录失败
                Debug.Log("登录失败");
                NetManager.Close();
                ConnectFail("登录失败");
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
                //注册成功
                Debug.Log("注册成功");

                MsgLogin msgLogin = new();
                msgLogin.id = SaveGameManager.SaveData.UID.ToString();
                msgLogin.pw = SaveGameManager.SaveData.UID.ToString();
                msgLogin.nickName = SaveGameManager.Nickname;
                NetManager.Send(msgLogin);
            }
            else if (msg.result == 1)
            {
                //注册失败
                Debug.Log("注册失败");

                SaveGameManager.SaveData.UID = 0;
                PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
                PlayerPrefs.Save();

                NetManager.Close();
                ConnectFail("注册失败");
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
