using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    //是否成功连接？
    private bool connectSucc = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //初始化
        connectSucc = false;
        isFirst = false;
        //添加网络事件监听
        NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, ConnSucc);
        NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, ConnFail);
        NetManager.AddEventListener(NetManager.NetEvent.Close, ConnClose);
        //添加消息监听
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

    //连接成功事件
    public void ConnSucc(string str)
    {
        Debug.Log("[Main]Conn succ");
        connectSucc = true;
        chose = isFirst;
        BeginPlay();
    }
    //连接失败
    public void ConnFail(string str)
    {
        connectSucc = true;
        Debug.Log("[Main]Conn fail "+str);
    }
    //连接关闭
    public void ConnClose(string str)
    {
        connectSucc = false;
        Debug.Log("[Main]Conn close");
    }

    public LocalPlayer localPlayer;
    public RemotePlayer remotePlayer;

    [HideInInspector]
    //是否先手
    public bool isFirst = false;

    //是否该选择
    [HideInInspector]
    public bool chose;

    [Header("倒计时")]
    public Text countDownText; // 倒计时文本
    public float countDownTimer = 5f; // 倒计时时间

    //文本更新
    void TextUpdate()
    {
        //时间同步还没做，暂时不使用倒计时
        if (!chose)
        {
            countDownText.text = "等待对手出招";
            return;
        }
        else
        {
            countDownText.text = "该你出招";
        }
        return;
        //if (countDownTimer > 0f)
        //{
        //    countDownTimer -= Time.deltaTime;

        //    // 将剩余时间显示在UI界面上的倒计时文本中
        //    countDownText.text = Mathf.RoundToInt(countDownTimer).ToString();
        //}
        //else
        //{
        //    //remotePlayer.AIplaying();
        //    localPlayer.RubbingEnergy();
        //    Despare();
        //}
    }

    //对决结果
    public void Despare()
    {
        if (localPlayer.Priority < remotePlayer.Priority && remotePlayer.Rebounding == false && remotePlayer.Defensing == false)  //remotePlayer攻击优先级更高
        {
            MsgResult msg = new();
            msg.result = MsgResult.Result.thisLose;
            NetManager.Send(msg);
            Lost();
        }
        else if (localPlayer.Priority < remotePlayer.Priority && remotePlayer.Rebounding == true && localPlayer.Defensing == false && localPlayer.Priority != 0) //remotePlayer反弹成功
        {
            MsgResult msg = new();
            msg.result = MsgResult.Result.thisLose;
            NetManager.Send(msg);
            Lost();
        }
        else if (localPlayer.Priority == remotePlayer.Priority)  //优先级一样，相互抵消
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
        else if (localPlayer.Defensing == true && remotePlayer.Priority != 2)  //玩家防御，remotePlayer不用大招，继续游戏
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
        else if (localPlayer.Rebounding == true && (remotePlayer.Priority == 0 || (remotePlayer.Priority != 0 && remotePlayer.Defensing == true)))  //玩家反弹失败
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
        else if (localPlayer.Priority == 0 && (remotePlayer.Defensing == true || remotePlayer.Priority == 0 || remotePlayer.Rebounding == true))  //玩家搓能量，remotePlayer不攻击
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
        else  //否则就是玩家赢
        {
            MsgResult msg = new();
            msg.result = MsgResult.Result.thisWin;
            NetManager.Send(msg);
            Win();
        }
    }

    //状态
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

    //远程行为
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
    //远程结果
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

    [Header("联机选择")]
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
