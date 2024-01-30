using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetManager.AddMsgListener("MsgLogin", OnLogin);
        NetManager.AddMsgListener("MsgRegister", OnRegister);
        NetManager.AddMsgListener("MsgKick", OnKick);
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
            Debug.Log("登录成功");
        }
        else if(msgLogin.result==1)
        {
            //登录失败
            Debug.Log("登录失败");
        }
    }

    //重复登录被提出（不应该放在这里
    private void OnKick(MsgBase msgBase)
    {
        MsgKick msgKick = (MsgKick)msgBase;
        if(msgKick.reason==0)
        {
            Debug.Log("被踢出");
            NetManager.Close();
            NetManager.ping = -1;
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
}
