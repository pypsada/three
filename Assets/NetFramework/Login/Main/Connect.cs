using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

//Used by Canvas
public class Connect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, ConnectSucc);
        NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, ConnectFail);
    }

    // Update is called once per frame
    void Update()
    {
        DelegateTrigger();
        return;
    }

    [Header("Ref")]
    public NetMain netMain;

    [Header("����")]
    public GameObject beforeConnect;
    public GameObject connecting;
    public GameObject login;
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
        trigger = 1;
    }

    private void ConnectFail(string msg)
    {
        trigger = 2;
    }

    //Ϊ�˽��һ���ǳ���ֵ�bug
    int trigger = 0;
    private void DelegateTrigger()
    {
        if (trigger == 1)
        {
            connecting.SetActive(false);
            connectSucc.SetActive(true);
            trigger = 0;
        }
        else if(trigger==2)
        {
            connecting.SetActive(false);
            connectFail.SetActive(true);
            trigger = 0;
        }
    }

    private void RemoveConnFunc()
    {
        NetManager.RemoveEventListener(NetManager.NetEvent.ConnectFail, ConnectFail);
        NetManager.RemoveEventListener(NetManager.NetEvent.ConnectSucc, ConnectSucc);
    }

    //�������ӳɹ��İ�ť
    public void OnClickConnSucc()
    {
        connectSucc.SetActive(false);
        login.SetActive(true);
        RemoveConnFunc();
    }

    //��������ʧ�ܵİ�ť
    public void OnClickConnFail()
    {
        connectFail.SetActive(false);
        beforeConnect.SetActive(true);
    }
}
