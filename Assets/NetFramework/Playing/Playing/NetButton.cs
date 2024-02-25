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
        //    Player = FindObjectOfType<Player>(); // ��ȡPlayer�ű�������
        //    ai = FindObjectOfType<AI>();  //��ȡAI�ű�������
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

    //���ʤ��
    private void ThisWin(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            eventSystem.SetActive(false);
            tipText.text = "ʤ��";
            remotePlayer.SetActive(false);
        });
    }

    //���ʧ��
    private void ThisLose(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            eventSystem.SetActive(false);
            tipText.text = "ʧ��";
            localPlayer.SetActive(false);
        });
    }

    //��Ϸ����
    private void GameContinue(MsgBase msgBase)
    {
        NetMain.actions.Enqueue(() =>
        {
            eventSystem.SetActive(true);
            tipText.text = "�������";
        });
    }

    //Զ�������Ϣ
    private void RemoteInfo(MsgBase msgBase)
    {
        MsgRemoteInfo msg=(MsgRemoteInfo)msgBase;
        remotePlayerScript.tmpData = msg.tmpData;
    }


    [Header("�ı���")]
    [Header("���浹��ʱ�ı���")]
    public Text tipText;
    [Header("���������Ϣ")]
    public Text localEnegy;
    public Text localCareer;
    [Header("Զ�������Ϣ")]
    public Text remoteEnegy;
    public Text remoteCareer;

    //�ڰ��°�ť֮��
    private void AfterAct()
    {
        eventSystem.SetActive(false);
        tipText.text = "�ȴ��Է���ҳ���";
    }

    //����UI����
    private void UpdateUIData()
    {
        PlayerTmpData localData = playerScript.tmpData;
        PlayerTmpData remoteData = remotePlayerScript.tmpData;

        localEnegy.text = localData.Energy.ToString();
        localCareer.text = localData.Career.ToString();

        remoteEnegy.text = remoteData.Energy.ToString();
        remoteCareer.text = remoteData.Career.ToString();
    }

    public void RubbingEnergy()  //������
    {
        playerScript.tmpData.RubbingEnergy();
        AfterAct();
    }

    public void Gun()  //ǹ
    {
        if (playerScript.tmpData.Energy >= 1)
        {
            playerScript.tmpData.Gun();
            AfterAct();
        }
    }

    public void Rebound()  //����
    {
        if (playerScript.tmpData.Energy >= 2)
        {
            playerScript.tmpData.Rebound();
            AfterAct();
        }

    }

    public void Defense()  //����
    {
        playerScript.tmpData.Defense();
        AfterAct();
    }

    public void HolyGrail()  //����
    {
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

    //public void AIChoseAssassin()  //AIѡ��̿�ְҵ
    //{
    //    Whole.AICareer = "Assassin";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIKing()  //AIѡ�����ְҵ
    //{
    //    Whole.AICareer = "King";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIGuard()  //AIѡ����ְҵ
    //{
    //    Whole.AICareer = "Guard";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIChoseTurtle()  //AIѡ���ڹ�ְҵ
    //{
    //    Whole.AICareer = "Turtle";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIChoseRascally()  //AIѡ������ְҵ
    //{
    //    Whole.AICareer = "Rascally";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIChoseArrogance()  //AIѡ�����ְҵ
    //{
    //    Whole.AICareer = "Arrogance";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIChoseThief()  //ѡ�����ְҵ
    //{
    //    Whole.AICareer = "Thief";
    //    SceneManager.LoadScene("PK&AI");
    //}

    //public void AIChosePangolin()  //AIѡ��ɽ��ְҵ
    //{
    //    Whole.AICareer = "Pangolin";
    //    SceneManager.LoadScene("PK&AI");
    //}

    public void VocationalSkills()  //ʹ��ְҵ����
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
