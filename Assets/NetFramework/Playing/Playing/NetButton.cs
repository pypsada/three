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
    [Header("��Ҷ�������")]
    public Animator localAni;
    public Animator remoteAni;

    [HideInInspector]
    //����ָʾ�����Ѿ��������
    public int aniTrigger = 0;

    private bool canClick = true;

    private enum PlayState
    {
        win,
        lost,
        conti
    }
    //������¼�������Ϸ״̬
    private PlayState playState;

    internal object image;

    private NetGame.Player playerScript;
    private NetGame.Player remotePlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        //    Player = FindObjectOfType<Player>(); // ��ȡPlayer�ű�������
        //    ai = FindObjectOfType<AI>();  //��ȡAI�ű�������
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

    //���ʤ��
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
            //tipText.text = "ʤ��";
            //remotePlayer.SetActive(false);
            AfterGetState();
        });
    }

    //���ʧ��
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
            //tipText.text = "ʧ��";
            //localPlayer.SetActive(false);
            AfterGetState();
        });
    }

    //��Ϸ����
    private void GameContinue(MsgBase msgBase)
    {
        playState = PlayState.conti;
        NetMain.actions.Enqueue(() =>
        {
            //eventSystem.SetActive(true);
            //tipText.text = "�������";
            //round++;
            AfterGetState();
        });
    }

    //�յ�State��Ϣ��׼�����Ŷ���
    private void AfterGetState()
    {
        aniTrigger = 0;
        canClick = false;
        if(Urging)
        {
            chatFeild.TextRoll("ϵͳ:�Է��ж�,�ߴ�ȡ��");
        }
        Urging = false;
        //���ű�����Һ�Զ����ҵĶ���
        localAni.SetTrigger(playerScript.tmpData.skillName);
        remoteAni.SetTrigger(remotePlayerScript.tmpData.skillName);
    }

    //��Tigger�ﵽ2ʱ�������������
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

    //Ϊ�˷����Ժ���ģ�����Ϸʤ���������������
    private void GameWin()
    {
        NetMain.actions.Enqueue(() =>
        {
            //RemoveListner();
            //eventSystem.SetActive(false);
            canClick = false;
            tipText.text = "ʤ��";
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
            tipText.text = "ʧ��";
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
            tipText.text = "�����";
            round++;
        });
    }

    //���·���ƥ�����
    public void OnClickGoBack()
    {
        SceneManager.LoadScene("GameRooms");
    }

    //Զ�������Ϣ
    private void RemoteInfo(MsgBase msgBase)
    {
        MsgRemoteInfo msg=(MsgRemoteInfo)msgBase;
        remotePlayerScript.tmpData = (PlayerTmpData)JsonUtility.FromJson(msg.tmpData, typeof(PlayerTmpData));
    }

    //���������Ϣ
    private void LocalInfo(MsgBase msgBase)
    {
        MsgLocalInfo msg = (MsgLocalInfo)msgBase;
        playerScript.tmpData = (PlayerTmpData)JsonUtility.FromJson(msg.tmpData, typeof(PlayerTmpData));
    }

    //�������䰴ť������Ǵߴ�ʱ�䵽Ҳ�ǵ����������
    public void OnClickEscape()
    {
        NetManager.Send(new MsgAdmitDefeat());
    }

    //�Ƿ����ڴߴ�
    private bool Urging = false;
    private int leftTime = 30;
    private float lastUrgeTime = 0;

    //���´ߴٰ�ť
    public void OnClickUrge()
    {
        if (Urging) return;
        if(canClick)
        {
            chatFeild.TextRoll("ϵͳ:�����ȳ���");
            return;
        }
        lastUrgeTime = 0;
        leftTime = 30;
        Urging = true;
    }

    //���ߴ�
    private void BeUrged(MsgBase msgBase)
    {
        MsgUrge msg = (MsgUrge)msgBase;
        if(msg.leftTime>0)
        {
            chatFeild.TextRoll("ϵͳ:���ߴ�,ʣ" + msg.leftTime + "��");
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
                chatFeild.TextRoll("ϵͳ:�ߴٶԷ�,ʣ" + leftTime + "��");

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


    [Header("�ı���")]
    [Header("���浹��ʱ�ı���")]
    public Text tipText;
    [Header("���������Ϣ")]
    public Text localEnegy;
    public Text localCareer;
    public Text localHealth;
    [Header("Զ�������Ϣ")]
    public Text remoteEnegy;
    public Text remoteCareer;
    public Text remoteHealth;
    [Header("��Ϸ�����ı�")]
    public Text gameRound;
    [Header("����")]
    public GameObject victoryWindown;
    public GameObject lostWindown;

    private int round = 1;

    //�ڰ��°�ť֮��
    private void AfterAct()
    {
        //eventSystem.SetActive(false);
        canClick = false;
        tipText.text = "���ֳ���";
        MsgPlayerAct act = new();
        act.tmpData = JsonUtility.ToJson(playerScript.tmpData);
        NetManager.Send(act);
    }

    //����UI����
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

    public void RubbingEnergy()  //������
    {
        if (!canClick) return;
        playerScript.tmpData.RubbingEnergy();
        AfterAct();
    }

    public void Gun()  //ǹ
    {
        if (!canClick) return;
        if (playerScript.tmpData.Energy >= 1)
        {
            playerScript.tmpData.Gun();
            AfterAct();
        }
    }

    public void Rebound()  //����
    {
        if (!canClick) return;
        if (playerScript.tmpData.Energy >= 2)
        {
            playerScript.tmpData.Rebound();
            AfterAct();
        }

    }

    public void Defense()  //����
    {
        if (!canClick) return;
        playerScript.tmpData.Defense();
        AfterAct();
    }

    public void HolyGrail()  //����
    {
        if (!canClick) return;
        if (playerScript.tmpData.Energy >= 4)
        {
            playerScript.tmpData.HolyGrail();
            AfterAct();
        }
    }

    public void ChoseAssassin()  //ѡ��̿�ְҵ
    {
        Whole.PlayerCareer = "Assassin";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseKing()  //ѡ�����ְҵ
    {
        Whole.PlayerCareer = "King";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseGuard()  //ѡ����ְҵ
    {
        Whole.PlayerCareer = "Guard";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseTurtle()  //ѡ���ڹ�ְҵ
    {
        Whole.PlayerCareer = "Turtle";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseRascally()  //ѡ������ְҵ
    {
        Whole.PlayerCareer = "Rascally";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseArrogance()  //ѡ�����ְҵ
    {
        Whole.PlayerCareer = "Arrogance";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseThief()  //ѡ�����ְҵ
    {
        Whole.PlayerCareer = "Thief";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChosePangolin()  //ѡ��ɽ��ְҵ
    {
        Whole.PlayerCareer = "Pangolin";
        //NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void VocationalSkills()  //ʹ��ְҵ����
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
