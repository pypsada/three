using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("����")]
    public GameObject login;
    public GameObject register;
    public GameObject loginFail;

    //�����¼��ť
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

    //��¼�ص�
    private void OnLogin(MsgBase msgBase)
    {
        MsgLogin msgLogin = (MsgLogin)msgBase;
        if(msgLogin.result==0)
        {
            //��¼�ɹ�
            Debug.Log("��¼�ɹ�");
            RemoveMsgListener();
            SceneManager.LoadScene("GameRooms");
        }
        else if(msgLogin.result==1)
        {
            //��¼ʧ��
            Debug.Log("��¼ʧ��");
            loginFail.SetActive(true);
        }
    }

    //�ظ���¼�����
    private void OnKick(MsgBase msgBase)
    {
        MsgKick msgKick = (MsgKick)msgBase;
        if(msgKick.reason==0)
        {
            Debug.Log("���߳�");
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
            //ע��ɹ�
            Debug.Log("ע��ɹ�");
        }
        else if (msg.result == 1)
        {
            //ע��ʧ��
            Debug.Log("ע��ʧ��");
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
