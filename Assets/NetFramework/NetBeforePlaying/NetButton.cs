using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class NetButton : MonoBehaviour
{
    public Player Player;
    public AI ai;
    internal object image;

    // Start is called before the first frame update
    void Start()
    {
        //    Player = FindObjectOfType<Player>(); // ��ȡPlayer�ű�������
        //    ai = FindObjectOfType<AI>();  //��ȡAI�ű�������
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RubbingEnergy()  //������
    {
        Player.RubbingEnergy();
    }

    public void Gun()  //ǹ
    {
        if (Player.Energy >= 1)
        {
            Player.Gun();
        }

    }

    public void Rebound()  //����
    {
        if (Player.Energy >= 2)
        {
            Player.Rebound();
        }

    }

    public void Defense()  //����
    {
        Player.Defense();
    }

    public void HolyGrail()  //����
    {
        if (Player.Energy >= 4)
        {
            Player.HolyGrail();

        }
    }

    public void ChoseAssassin()  //ѡ��̿�ְҵ
    {
        Whole.PlayerCareer = "Assassin";
        NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseKing()  //ѡ�����ְҵ
    {
        Whole.PlayerCareer = "King";
        NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseGuard()  //ѡ����ְҵ
    {
        Whole.PlayerCareer = "Guard";
        NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseTurtle()  //ѡ���ڹ�ְҵ
    {
        Whole.PlayerCareer = "Turtle";
        NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseRascally()  //ѡ������ְҵ
    {
        Whole.PlayerCareer = "Rascally";
        NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseArrogance()  //ѡ�����ְҵ
    {
        Whole.PlayerCareer = "Arrogance";
        NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChoseThief()  //ѡ�����ְҵ
    {
        Whole.PlayerCareer = "Thief";
        NetManager.Send(new MsgClientReady());
        SceneManager.LoadScene("WaitPlaying");
    }

    public void ChosePangolin()  //ѡ��ɽ��ְҵ
    {
        Whole.PlayerCareer = "Pangolin";
        NetManager.Send(new MsgClientReady());
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
        if (Player.Career > 0)
        {
            if (Player.StringCareer == "Assassin")
            {
                Player.Assassinate();
            }
            if (Player.StringCareer == "King")
            {
                Player.King();
            }
            if (Player.StringCareer == "Guard")
            {
                Player.Guard();
            }
            if (Player.StringCareer == "Turtle")
            {
                Player.Turtle();
            }
            if (Player.StringCareer == "Rascally")
            {
                Player.Rascally();
            }
            if (Player.StringCareer == "Arrogance")
            {
                Player.Arrogance();
            }
            if (Player.StringCareer == "Thief")
            {
                Player.Thief();
            }
            if (Player.StringCareer == "Pangolin")
            {
                Player.Pangolin();
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
