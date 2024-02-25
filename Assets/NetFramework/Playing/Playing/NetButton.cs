using NetGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class NetButton : MonoBehaviour
{
    [Header("ref")]
    public GameObject localPlayer;
    public GameObject remotePlayer;
    public GameObject eventSystem;

    internal object image;

    private NetGame.Player playerScript;
    private NetGame.Player remotePlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        //    Player = FindObjectOfType<Player>(); // 获取Player脚本的引用
        //    ai = FindObjectOfType<AI>();  //获取AI脚本的引用
        playerScript = localPlayer.GetComponent<NetGame.Player>();
        remotePlayerScript = remotePlayer.GetComponent<NetGame.Player>();

        NetManager.AddMsgListener("MsgYouWin", ThisWin);
        NetManager.AddMsgListener("MsgYouLost", ThisLose);
        NetManager.AddMsgListener("MsgGameContinue", GameContinue);
        NetManager.AddMsgListener("MsgRemoteInfo", RemoteInfo);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIData();
        return;
    }

    //玩家胜利
    private void ThisWin(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            eventSystem.SetActive(false);
            tipText.text = "胜利";
            remotePlayer.SetActive(false);
        });
    }

    //玩家失败
    private void ThisLose(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            eventSystem.SetActive(false);
            tipText.text = "失败";
            localPlayer.SetActive(false);
        });
    }

    //游戏继续
    private void GameContinue(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            eventSystem.SetActive(true);
            tipText.text = "请你出招";
        });
    }

    //远程玩家信息
    private void RemoteInfo(MsgBase msgBase)
    {
        MsgRemoteInfo msg=(MsgRemoteInfo)msgBase;
        remotePlayerScript.tmpData = msg.tmpData;
    }


    [Header("文本框")]
    [Header("代替倒计时文本框")]
    public Text tipText;
    [Header("本地玩家信息")]
    public Text localEnegy;
    public Text localCareer;
    [Header("远程玩家信息")]
    public Text remoteEnegy;
    public Text remoteCareer;

    //在按下按钮之后
    private void AfterAct()
    {
        eventSystem.SetActive(false);
        tipText.text = "等待对方玩家出招";
    }

    //更新UI数据
    private void UpdateUIData()
    {
        PlayerTmpData localData = playerScript.tmpData;
        PlayerTmpData remoteData = remotePlayerScript.tmpData;

        localEnegy.text = localData.Energy.ToString();
        localCareer.text = localData.Career.ToString();

        remoteEnegy.text = remoteData.Energy.ToString();
        remoteCareer.text = remoteData.Career.ToString();
    }

    public void RubbingEnergy()  //搓能量
    {
        playerScript.tmpData.RubbingEnergy();
        AfterAct();
    }

    public void Gun()  //枪
    {
        if (playerScript.tmpData.Energy >= 1)
        {
            playerScript.tmpData.Gun();
            AfterAct();
        }
    }

    public void Rebound()  //反弹
    {
        if (playerScript.tmpData.Energy >= 2)
        {
            playerScript.tmpData.Rebound();
            AfterAct();
        }

    }

    public void Defense()  //防御
    {
        playerScript.tmpData.Defense();
        AfterAct();
    }

    public void HolyGrail()  //大招
    {
        if (playerScript.tmpData.Energy >= 4)
        {
            playerScript.tmpData.HolyGrail();
            AfterAct();
        }
    }

    public void ChoseAssassin()  //选择刺客职业
    {
        Whole.PlayerCareer = "Assassin";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseKing()  //选择国王职业
    {
        Whole.PlayerCareer = "King";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseGuard()  //选择护卫职业
    {
        Whole.PlayerCareer = "Guard";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseTurtle()  //选择乌龟职业
    {
        Whole.PlayerCareer = "Turtle";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseRascally()  //选择老赖职业
    {
        Whole.PlayerCareer = "Rascally";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseArrogance()  //选择傲慢职业
    {
        Whole.PlayerCareer = "Arrogance";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseThief()  //选择盗贼职业
    {
        Whole.PlayerCareer = "Thief";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChosePangolin()  //选择穿山甲职业
    {
        Whole.PlayerCareer = "Pangolin";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    //public void AIChoseAssassin()  //AI选择刺客职业
    //{
    //    Whole.AICareer = "Assassin";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIKing()  //AI选择国王职业
    //{
    //    Whole.AICareer = "King";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIGuard()  //AI选择护卫职业
    //{
    //    Whole.AICareer = "Guard";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIChoseTurtle()  //AI选择乌龟职业
    //{
    //    Whole.AICareer = "Turtle";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIChoseRascally()  //AI选择老赖职业
    //{
    //    Whole.AICareer = "Rascally";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIChoseArrogance()  //AI选择傲慢职业
    //{
    //    Whole.AICareer = "Arrogance";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIChoseThief()  //选择盗贼职业
    //{
    //    Whole.AICareer = "Thief";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIChosePangolin()  //AI选择穿山甲职业
    //{
    //    Whole.AICareer = "Pangolin";
    //    SceneManager.LoadScene("PK&AI");
    //}

    public void VocationalSkills()  //使用职业技能
    {
        if (playerScript.tmpData.Career > 0)
        {
            if (playerScript.tmpData.StringCareer == "Assassin")
            {
                playerScript.tmpData.Assassinate();
                AfterAct();
            }
            if (playerScript.tmpData.StringCareer == "King")
            {
                playerScript.tmpData.King();
                AfterAct();
            }
            if (playerScript.tmpData.StringCareer == "Guard")
            {
                playerScript.tmpData.Guard();
                AfterAct();
            }
            if (playerScript.tmpData.StringCareer == "Turtle")
            {
                playerScript.tmpData.Turtle();
                AfterAct();
            }
            if (playerScript.tmpData.StringCareer == "Rascally")
            {
                playerScript.tmpData.Rascally();
                AfterAct();
            }
            if (playerScript.tmpData.StringCareer == "Arrogance")
            {
                playerScript.tmpData.Arrogance();
                AfterAct();
            }
            if (playerScript.tmpData.StringCareer == "Thief")
            {
                playerScript.tmpData.Thief();
                AfterAct();
            }
            if (playerScript.tmpData.StringCareer == "Pangolin")
            {
                playerScript.tmpData.Pangolin();
                AfterAct();
            }
        }

    }
    //public void Back()
    //{
    //    SceneManager.LoadScene("ChoseCareer");
    //}

    //public void SmallGame1()
    //{
    //    if (Whole.Game1 == 0)
    //    {
    //        SceneManager.LoadScene("Game1Teach");
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene("Game1");
    //    }
    //}
    //public void SmallGame2()
    //{
    //    if (Whole.Game1 == 0)
    //    {
    //        SceneManager.LoadScene("Game2Teach");
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene("Game2");
    //    }
    //}
    //public void SmallGame3()
    //{
    //    if (Whole.Game1 == 0)
    //    {
    //        SceneManager.LoadScene("Game3Teach");
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene("Game3");
    //    }
    //}

    //public void Backto()
    //{
    //    Time.timeScale = 1;
    //    SceneManager.LoadScene("ChoseCareer");
    //}
}
