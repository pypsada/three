using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public LocalPlayer localPlayer;
    public RemotePlayer remotePlayer;

    public Server server;

    //是否先手
    public bool isFirst;

    //是否该选择
    [HideInInspector]
    public bool chose;

    public Text countDownText; // 倒计时文本
    public float countDownTimer = 5f; // 倒计时时间

    // Start is called before the first frame update
    void Start()
    {
        server = FindObjectOfType<Server>();
        isFirst = server.isServer;

        if(isFirst)
        {
            chose = true;
        }
        else
        {
            chose = false;
        }
        NetManager.AddMsgListener("MsgAct", RemoteAct);
        NetManager.AddMsgListener("MsgResult", RemoteResult);
    }

    // Update is called once per frame
    void Update()
    {
        NetManager.MsgUpdate();

        if(chose)
        {
            countDownText.text = "等待对手出招";
            return;
        }
        if (countDownTimer > 0f)
        {
            countDownTimer -= Time.deltaTime;

            // 将剩余时间显示在UI界面上的倒计时文本中
            countDownText.text = Mathf.RoundToInt(countDownTimer).ToString();
        }
        else
        {
            //remotePlayer.AIplaying();
            localPlayer.RubbingEnergy();
            Despare();
        }
    }

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

    private void Win()
    {
        Debug.Log("Win");
        Destroy(remotePlayer.gameObject);
    }
    private void Lost()
    {
        Debug.Log("Lost");
        Destroy(localPlayer.gameObject);
    }
    private void Continue()
    {
        chose = !chose;
        Debug.Log("Continue");
    }

    public void RemoteAct(MsgBase msgAct)
    {
        string actStr = ((MsgAct)msgAct).act.ToString();
        MethodInfo mei = typeof(RemotePlayer).GetMethod(actStr);
        mei.Invoke(null, null);
    }

    public void RemoteResult(MsgBase msgResult)
    {
        switch(((MsgResult)msgResult).result)
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
}
