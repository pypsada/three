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

    [Header("½çÃæ")]
    public GameObject login;
    public GameObject register;

    //µã»÷µÇÂ¼°´Å¥
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

    //µÇÂ¼»Øµ÷
    private void OnLogin(MsgBase msgBase)
    {
        MsgLogin msgLogin = (MsgLogin)msgBase;
        if(msgLogin.result==0)
        {
            //µÇÂ¼³É¹¦
            Debug.Log("µÇÂ¼³É¹¦");
        }
        else if(msgLogin.result==1)
        {
            //µÇÂ¼Ê§°Ü
            Debug.Log("µÇÂ¼Ê§°Ü");
        }
    }

    private void OnKick(MsgBase msgBase)
    {
        MsgKick msgKick = (MsgKick)msgBase;
        if(msgKick.reason==0)
        {
            Debug.Log("±»Ìß³ö");
            NetManager.Close();
            NetManager.ping = -1;
        }
    }

    private void OnRegister(MsgBase msgBase)
    {
        MsgRegister msg = (MsgRegister)msgBase;
        if (msg.result == 0)
        {
            //×¢²á³É¹¦
            Debug.Log("×¢²á³É¹¦");
        }
        else if (msg.result == 1)
        {
            //×¢²áÊ§°Ü
            Debug.Log("×¢²áÊ§°Ü");
        }
    }
}
