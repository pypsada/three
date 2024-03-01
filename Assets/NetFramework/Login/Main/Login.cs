using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AddMsgListener();
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    [Header("ref")]
    public InputField lid;
    public InputField lpw;
    [Space]
    public InputField rid;
    public InputField rpw;

    [Header("界面")]
    public GameObject login;
    public GameObject register;
    public GameObject loginFail;

    //点击登录按钮
    public void ClickLogin()
    {
        MsgLogin msgLogin = new();
        msgLogin.id = lid.text;
        msgLogin.pw = lpw.text;
        NetManager.Send(msgLogin);
    }

    public void ClickGotoRigister()
    {
        login.SetActive(false);
        register.SetActive(true);
    }

    public void ClickRigster()
    {
        MsgRegister msgRegister = new();
        msgRegister.id = rid.text;
        msgRegister.pw = rpw.text;
        NetManager.Send(msgRegister);
    }

    public void ClickBackLogin()
    {
        login.SetActive(true);
        register.SetActive(false);
    }

    //登录回调
    private void OnLogin(MsgBase msgBase)
    {
        MsgLogin msgLogin = (MsgLogin)msgBase;
        if(msgLogin.result==0)
        {
            //登录成功
            NetMain.actions.Enqueue(() =>
            {
                //更新胜败场数据
                SaveGameManager.SaveData.victory = msgLogin.winTimes;
                SaveGameManager.SaveData.lose = msgLogin.lostTimes;
                PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
                PlayerPrefs.Save();
            });
            Debug.Log("登录成功");
            RemoveMsgListener();
            SceneManager.LoadScene("GameRooms");
        }
        else if(msgLogin.result==1)
        {
            //登录失败
            Debug.Log("登录失败");
            //loginFail.SetActive(true);
        }
    }

    //重复登录被提出
    private void OnKick(MsgBase msgBase)
    {
        MsgKick msgKick = (MsgKick)msgBase;
        if(msgKick.reason==0)
        {
            Debug.Log("被踢出");
            NetManager.Close();
            NetManager.ping = -1;
            Connect connect = new();
            connect.ClickClose();
        }
    }

    private void OnRegister(MsgBase msgBase)
    {
        MsgRegister msg = (MsgRegister)msgBase;
        if (msg.result == 0)
        {
            //注册成功
            Debug.Log("注册成功");
        }
        else if (msg.result == 1)
        {
            //注册失败
            Debug.Log("注册失败");
        }
    }

    private void AddMsgListener()
    {
        NetManager.AddMsgListener("MsgLogin", OnLogin);
        NetManager.AddMsgListener("MsgRegister", OnRegister);
        NetManager.AddMsgListener("MsgKick", OnKick);
    }

    private void RemoveMsgListener()
    {
        NetManager.RemoveMsgListener("MsgLogin", OnLogin);
        NetManager.RemoveMsgListener("MsgRegister", OnRegister);
        NetManager.RemoveMsgListener("MsgKick", OnKick);
    }
}
