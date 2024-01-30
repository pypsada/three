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
        AddMsgListener();
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
        NetMain.actions.Enqueue(() =>
        {
            connectSucc.SetActive(true);
            connecting.SetActive(false);
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
        login.SetActive(true);
        RemoveMsgListener();
    }

    //��������ʧ�ܵİ�ť
    public void OnClickConnFail()
    {
        connectFail.SetActive(false);
        beforeConnect.SetActive(true);
    }

    //��������رհ�ť
    public void ClickClose()
    {
        Debug.Log("�ǳ�");
        NetManager.Close();
        NetManager.ping = -1;
        login.SetActive(false);
        beforeConnect.SetActive(true);
        AddMsgListener();
    }

    private void AddMsgListener()
    {
        NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, ConnectSucc);
        NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, ConnectFail);
    }

    private void RemoveMsgListener()
    {
        NetManager.RemoveEventListener(NetManager.NetEvent.ConnectSucc, ConnectSucc);
        NetManager.RemoveEventListener(NetManager.NetEvent.ConnectFail, ConnectFail);
    }
}
