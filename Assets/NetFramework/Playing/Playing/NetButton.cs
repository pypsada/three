using NetGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class NetButton : MonoBehaviour
{
    [Header("set")]
    public bool updata;
    [Header("ref")]
    public GameObject localPlayer;
    public GameObject remotePlayer;
    public ChatFeild chatFeild;
    //public GameObject eventSystem;
    [Header("玩家动画引用")]
    public Animator localAni;
    public Animator remoteAni;

    [HideInInspector]
    //用来指示动画已经播放完毕
    public int aniTrigger = 0;

    private bool canClick = true;

    private enum PlayState
    {
        win,
        lost,
        conti
    }
    //用来记录结算后游戏状态
    private PlayState playState;

    internal object image;

    private NetGame.Player playerScript;
    private NetGame.Player remotePlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        //    Player = FindObjectOfType<Player>(); // 获取Player脚本的引用
        //    ai = FindObjectOfType<AI>();  //获取AI脚本的引用
        if (!updata) return;
        playerScript = localPlayer.GetComponent<NetGame.Player>();
        remotePlayerScript = remotePlayer.GetComponent<NetGame.Player>();

        NetManager.AddMsgListener("MsgYouWin", ThisWin);
        NetManager.AddMsgListener("MsgYouLost", ThisLose);
        NetManager.AddMsgListener("MsgGameContinue", GameContinue);
        NetManager.AddMsgListener("MsgRemoteInfo", RemoteInfo);
        NetManager.AddMsgListener("MsgLocalInfo", LocalInfo);
        NetManager.AddMsgListener("MsgUrge", BeUrged);

        NetManager.Send(new MsgInitPlaying());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIData();
        AfterPlayAnimation();
        IsUrging();
        return;
    }

    private void OnDestroy()
    {
        RemoveListner();
    }

    //玩家胜利
    private void ThisWin(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            MsgYouWin msg = (MsgYouWin)msgBase;
            SaveGameManager.SaveData.victory = msg.victualTimes;
            SaveGameManager.SaveData.lose = msg.failTimes;
            PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
            PlayerPrefs.Save();

            playState = PlayState.win;
            //eventSystem.SetActive(false);
            //tipText.text = "胜利";
            //remotePlayer.SetActive(false);
            AfterGetState();
        });
    }

    //玩家失败
    private void ThisLose(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            MsgYouLost msg = (MsgYouLost)msgBase;
            SaveGameManager.SaveData.victory = msg.victualTimes;
            SaveGameManager.SaveData.lose = msg.failTimes;
            PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
            PlayerPrefs.Save();

            playState = PlayState.lost;
            //eventSystem.SetActive(false);
            //tipText.text = "失败";
            //localPlayer.SetActive(false);
            AfterGetState();
        });
    }

    //游戏继续
    private void GameContinue(MsgBase msgBase)
    {
        playState = PlayState.conti;
        NetMain.actions.Enqueue(() =>
        {
            //eventSystem.SetActive(true);
            //tipText.text = "请你出招";
            //round++;
            AfterGetState();
        });
    }

    //收到State消息，准备播放动画
    private void AfterGetState()
    {
        aniTrigger = 0;
        canClick = false;
        if(Urging)
        {
            chatFeild.TextRoll("系统:对方行动,催促取消");
        }
        Urging = false;
        //播放本地玩家和远程玩家的动画
        localAni.SetTrigger(playerScript.tmpData.skillName);
        remoteAni.SetTrigger(remotePlayerScript.tmpData.skillName);
    }

    //当Tigger达到2时就启动这个函数
    //Used by Update
    private void AfterPlayAnimation()
    {
        if (!updata) return;
        if(aniTrigger>=2)
        {
            localAni.SetTrigger("Idle");
            remoteAni.SetTrigger("Idle");
            //eventSystem.SetActive(true);
            canClick = true;
            aniTrigger = 0;
            switch(playState)
            {
                case PlayState.win:
                    GameWin();
                    break;
                case PlayState.lost:
                    GameLost();
                    break;
                case PlayState.conti:
                    GameConti();
                    break;
            }
        }
    }

    //为了方便以后更改，将游戏胜负的情况放在这里
    private void GameWin()
    {
        NetMain.actions.Enqueue(() =>
        {
            //RemoveListner();
            //eventSystem.SetActive(false);
            canClick = false;
            tipText.text = "胜利";
            remotePlayer.SetActive(false);
            victoryWindown.SetActive(true);
        });
    }

    private void GameLost()
    {
        NetMain.actions.Enqueue(() =>
        {
            //RemoveListner();
            //eventSystem.SetActive(false);
            canClick = false;
            tipText.text = "失败";
            localPlayer.SetActive(false);
            lostWindown.SetActive(true);
        });
    }

    private void GameConti()
    {
        NetMain.actions.Enqueue(() =>
        {
            //eventSystem.SetActive(true);
            canClick = true;
            tipText.text = "请出招";
            round++;
        });
    }

    //按下返回匹配界面
    public void OnClickGoBack()
    {
        SceneManager.LoadScene("GameRooms");
    }

    //远程玩家信息
    private void RemoteInfo(MsgBase msgBase)
    {
        MsgRemoteInfo msg=(MsgRemoteInfo)msgBase;
        remotePlayerScript.tmpData = (PlayerTmpData)JsonUtility.FromJson(msg.tmpData, typeof(PlayerTmpData));
    }

    //本地玩家信息
    private void LocalInfo(MsgBase msgBase)
    {
        MsgLocalInfo msg = (MsgLocalInfo)msgBase;
        playerScript.tmpData = (PlayerTmpData)JsonUtility.FromJson(msg.tmpData, typeof(PlayerTmpData));
    }

    //按下认输按钮，如果是催促时间到也是调用这个函数
    public void OnClickEscape()
    {
        NetManager.Send(new MsgAdmitDefeat());
    }

    //是否正在催促
    private bool Urging = false;
    private int leftTime = 30;
    private float lastUrgeTime = 0;

    //按下催促按钮
    public void OnClickUrge()
    {
        if (Urging) return;
        if(canClick)
        {
            chatFeild.TextRoll("系统:请你先出招");
            return;
        }
        lastUrgeTime = 0;
        leftTime = 30;
        Urging = true;
    }

    //被催促
    private void BeUrged(MsgBase msgBase)
    {
        MsgUrge msg = (MsgUrge)msgBase;
        if(msg.leftTime>0)
        {
            chatFeild.TextRoll("系统:被催促,剩" + msg.leftTime + "秒");
        }
        else
        {
            OnClickEscape();
        }
    }

    //Used by update
    private void IsUrging()
    {
        if (!updata) return;
        if(Urging)
        {
            if(Time.time-lastUrgeTime>5)
            {
                lastUrgeTime = Time.time;

                MsgUrge msgUrge = new();
                msgUrge.leftTime = leftTime;
                NetManager.Send(msgUrge);
                chatFeild.TextRoll("系统:催促对方,剩" + leftTime + "秒");

                leftTime -= 5;
            }
        }
    }

    private void RemoveListner()
    {
        NetManager.RemoveMsgListener("MsgYouWin", ThisWin);
        NetManager.RemoveMsgListener("MsgYouLost", ThisLose);
        NetManager.RemoveMsgListener("MsgGameContinue", GameContinue);
        NetManager.RemoveMsgListener("MsgRemoteInfo", RemoteInfo);
        NetManager.RemoveMsgListener("MsgLocalInfo", LocalInfo);
        NetManager.RemoveMsgListener("MsgUrge",BeUrged);
    }


    [Header("文本框")]
    [Header("代替倒计时文本框")]
    public Text tipText;
    [Header("本地玩家信息")]
    public Text localEnegy;
    public Text localCareer;
    public Text localHealth;
    [Header("远程玩家信息")]
    public Text remoteEnegy;
    public Text remoteCareer;
    public Text remoteHealth;
    [Header("游戏轮数文本")]
    public Text gameRound;
    [Header("弹窗")]
    public GameObject victoryWindown;
    public GameObject lostWindown;

    private int round = 1;

    //在按下按钮之后
    private void AfterAct()
    {
        //eventSystem.SetActive(false);
        canClick = false;
        tipText.text = "对手出招";
        MsgPlayerAct act = new();
        act.tmpData = JsonUtility.ToJson(playerScript.tmpData);
        NetManager.Send(act);
    }

    //更新UI数据
    private void UpdateUIData()
    {
        if (!updata) return;

        PlayerTmpData localData = playerScript.tmpData;
        PlayerTmpData remoteData = remotePlayerScript.tmpData;

        localEnegy.text = localData.Energy.ToString();
        localCareer.text = localData.Career.ToString();
        localHealth.text = localData.health.ToString();

        remoteEnegy.text = remoteData.Energy.ToString();
        remoteCareer.text = remoteData.Career.ToString();
        remoteHealth.text = remoteData.health.ToString();

        gameRound.text = round.ToString();
    }

    public void RubbingEnergy()  //搓能量
    {
        if (!canClick) return;
        playerScript.tmpData.RubbingEnergy();
        AfterAct();
    }

    public void Gun()  //枪
    {
        if (!canClick) return;
        if (playerScript.tmpData.Energy >= 1)
        {
            playerScript.tmpData.Gun();
            AfterAct();
        }
    }

    public void Rebound()  //反弹
    {
        if (!canClick) return;
        if (playerScript.tmpData.Energy >= 2)
        {
            playerScript.tmpData.Rebound();
            AfterAct();
        }

    }

    public void Defense()  //防御
    {
        if (!canClick) return;
        playerScript.tmpData.Defense();
        AfterAct();
    }

    public void HolyGrail()  //大招
    {
        if (!canClick) return;
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

    public void VocationalSkills()  //使用职业技能
    {
        if (!canClick) return;
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
}
