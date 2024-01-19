using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    //�Ƿ�ɹ����ӣ�
    private bool connectSucc = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //��ʼ��
        connectSucc = false;
        isFirst = false;
        //��������¼�����
        NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, ConnSucc);
        NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, ConnFail);
        NetManager.AddEventListener(NetManager.NetEvent.Close, ConnClose);
        //�����Ϣ����
        NetManager.AddMsgListener("MsgAct", RemoteAct);
        NetManager.AddMsgListener("MsgResult", RemoteResult);
    }

    // Update is called once per frame
    void Update()
    {
        if (!connectSucc) return;
        NetManager.MsgUpdate();
        TextUpdate();
    }

    //���ӳɹ��¼�
    public void ConnSucc(string str)
    {
        Debug.Log("[Main]Conn succ");
        connectSucc = true;
        chose = isFirst;
        BeginPlay();
    }
    //����ʧ��
    public void ConnFail(string str)
    {
        connectSucc = true;
        Debug.Log("[Main]Conn fail "+str);
    }
    //���ӹر�
    public void ConnClose(string str)
    {
        connectSucc = false;
        Debug.Log("[Main]Conn close");
    }

    public LocalPlayer localPlayer;
    public RemotePlayer remotePlayer;

    [HideInInspector]
    //�Ƿ�����
    public bool isFirst = false;

    //�Ƿ��ѡ��
    [HideInInspector]
    public bool chose;

    [Header("����ʱ")]
    public Text countDownText; // ����ʱ�ı�
    public float countDownTimer = 5f; // ����ʱʱ��

    //�ı�����
    void TextUpdate()
    {
        //ʱ��ͬ����û������ʱ��ʹ�õ���ʱ
        if (!chose)
        {
            countDownText.text = "�ȴ����ֳ���";
            return;
        }
        else
        {
            countDownText.text = "�������";
        }
        return;
        //if (countDownTimer > 0f)
        //{
        //    countDownTimer -= Time.deltaTime;

        //    // ��ʣ��ʱ����ʾ��UI�����ϵĵ���ʱ�ı���
        //    countDownText.text = Mathf.RoundToInt(countDownTimer).ToString();
        //}
        //else
        //{
        //    //remotePlayer.AIplaying();
        //    localPlayer.RubbingEnergy();
        //    Despare();
        //}
    }

    //�Ծ����
    public void Despare()
    {
        if (localPlayer.Priority < remotePlayer.Priority && remotePlayer.Rebounding == false && remotePlayer.Defensing == false)  //remotePlayer�������ȼ�����
        {
            MsgResult msg = new();
            msg.result = MsgResult.Result.thisLose;
            NetManager.Send(msg);
            Lost();
        }
        else if (localPlayer.Priority < remotePlayer.Priority && remotePlayer.Rebounding == true && localPlayer.Defensing == false && localPlayer.Priority != 0) //remotePlayer�����ɹ�
        {
            MsgResult msg = new();
            msg.result = MsgResult.Result.thisLose;
            NetManager.Send(msg);
            Lost();
        }
        else if (localPlayer.Priority == remotePlayer.Priority)  //���ȼ�һ�����໥����
        {
            localPlayer.Priority = 0;
            localPlayer.Defensing = false;
            localPlayer.Rebounding = false;
            remotePlayer.Rebounding = false;
            countDownTimer = 5f;
            remotePlayer.Priority = 0;
            remotePlayer.Defensing = false;
            MsgResult msg = new();
            msg.result = MsgResult.Result.thisContinue;
            NetManager.Send(msg);
            Continue();
        }
        else if (localPlayer.Defensing == true && remotePlayer.Priority != 2)  //��ҷ�����remotePlayer���ô��У�������Ϸ
        {
            localPlayer.Priority = 0;
            localPlayer.Defensing = false;
            localPlayer.Rebounding = false;
            remotePlayer.Rebounding = false;
            countDownTimer = 5f;
            remotePlayer.Priority = 0;
            remotePlayer.Defensing = false;
            MsgResult msg = new();
            msg.result = MsgResult.Result.thisContinue;
            NetManager.Send(msg);
            Continue();
        }
        else if (localPlayer.Rebounding == true && (remotePlayer.Priority == 0 || (remotePlayer.Priority != 0 && remotePlayer.Defensing == true)))  //��ҷ���ʧ��
        {
            localPlayer.Priority = 0;
            localPlayer.Defensing = false;
            localPlayer.Rebounding = false;
            remotePlayer.Rebounding = false;
            countDownTimer = 5f;
            remotePlayer.Priority = 0;
            remotePlayer.Defensing = false;
            MsgResult msg = new();
            msg.result = MsgResult.Result.thisContinue;
            NetManager.Send(msg);
            Continue();
        }
        else if (localPlayer.Priority == 0 && (remotePlayer.Defensing == true || remotePlayer.Priority == 0 || remotePlayer.Rebounding == true))  //��Ҵ�������remotePlayer������
        {
            localPlayer.Priority = 0;
            localPlayer.Defensing = false;
            localPlayer.Rebounding = false;
            remotePlayer.Rebounding = false;
            countDownTimer = 5f;
            remotePlayer.Priority = 0;
            remotePlayer.Defensing = false;
            MsgResult msg = new();
            msg.result = MsgResult.Result.thisContinue;
            NetManager.Send(msg);
            Continue();
        }
        else  //����������Ӯ
        {
            MsgResult msg = new();
            msg.result = MsgResult.Result.thisWin;
            NetManager.Send(msg);
            Win();
        }
    }

    //״̬
    private void Win()
    {
        Debug.Log("Win");
        chose = false;
        Destroy(remotePlayer.gameObject);
    }
    private void Lost()
    {
        Debug.Log("Lost");
        chose = false;
        Destroy(localPlayer.gameObject);
    }
    private void Continue()
    {
        chose = !chose;
        Debug.Log("Continue");
    }

    //Զ����Ϊ
    public void RemoteAct(MsgBase msgAct)
    {
        Debug.Log("Remote" + ((MsgAct)msgAct).act.ToString());
        switch (((MsgAct)msgAct).act)
        {
            case MsgAct.Act.Defense:
                remotePlayer.Defense();
                break;
            case MsgAct.Act.HolyGrail:
                remotePlayer.HolyGrail();
                break;
            case MsgAct.Act.RubbingEnergy:
                remotePlayer.RubbingEnergy();
                break;
            case MsgAct.Act.Rebound:
                remotePlayer.Rebound();
                break;
            case MsgAct.Act.Gun:
                remotePlayer.Gun();
                break;
            default:
                break;
        }
    }
    //Զ�̽��
    public void RemoteResult(MsgBase msgResult)
    {
        Debug.Log("Remote" + ((MsgResult)msgResult).result.ToString());
        switch (((MsgResult)msgResult).result)
        {
            case MsgResult.Result.thisContinue:
                Continue();
                break;
            case MsgResult.Result.thisWin:
                Lost();
                break;
            case MsgResult.Result.thisLose:
                Win();
                break;
            default:
                break;
        }
    }

    [Header("UI")]
    public GameObject NetUI;
    public GameObject PlayUI;

    public void BeginPlay()
    {
        //PlayUI.SetActive(true);
        //NetUI.SetActive(false);
    }

    [Header("����ѡ��")]
    public InputField ipAndPort;
    public Dropdown isFirstDropDown;
    public void Conn()
    {
        string[] split = ipAndPort.text.Split(":");
        isFirst = false;
        if (isFirstDropDown.value == 0)
        {
            isFirst = true;
        }
        NetManager.Connect(split[0], int.Parse(split[1]));
    }
}
