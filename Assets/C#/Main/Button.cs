using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class Button : MonoBehaviour
{
    public Player Player;
    public AI ai;
    internal object image;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Player>(); // ��ȡPlayer�ű�������
        ai = FindObjectOfType<AI>();  //��ȡAI�ű�������
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
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseKing()  //ѡ�����ְҵ
    {
        Whole.PlayerCareer = "King";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseGuard()  //ѡ����ְҵ
    {
        Whole.PlayerCareer = "Guard";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseTurtle()  //ѡ���ڹ�ְҵ
    {
        Whole.PlayerCareer = "Turtle";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseRascally()  //ѡ������ְҵ
    {
        Whole.PlayerCareer = "Rascally";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseArrogance()  //ѡ�����ְҵ
    {
        Whole.PlayerCareer = "Arrogance";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseThief()  //ѡ�����ְҵ
    {
        Whole.PlayerCareer = "Thief";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChosePangolin()  //ѡ��ɽ��ְҵ
    {
        Whole.PlayerCareer = "Pangolin";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void AIChoseAssassin()  //AIѡ��̿�ְҵ
    {
        Whole.AICareer = "Assassin";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIKing()  //AIѡ�����ְҵ
    {
        Whole.AICareer = "King";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIGuard()  //AIѡ����ְҵ
    {
        Whole.AICareer = "Guard";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIChoseTurtle()  //AIѡ���ڹ�ְҵ
    {
        Whole.AICareer = "Turtle";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIChoseRascally()  //AIѡ������ְҵ
    {
        Whole.AICareer = "Rascally";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIChoseArrogance()  //AIѡ�����ְҵ
    {
        Whole.AICareer = "Arrogance";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIChoseThief()  //ѡ�����ְҵ
    {
        Whole.AICareer = "Thief";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIChosePangolin()  //AIѡ��ɽ��ְҵ
    {
        Whole.AICareer = "Pangolin";
        SceneManager.LoadScene("FightModleAI");
    }

    public void VocationalSkills()  //ʹ��ְҵ����
    {
        if (Player.Career>0)
        {
            if (Player.StringCareer== "Assassin")
            {
                Player.Assassinate();
            }
            if (Player.StringCareer=="King")
            {
                Player.King();
            }
            if(Player.StringCareer=="Guard")
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
    public void Back()
    {
        SceneManager.LoadScene("ChoseCareer");
    }

    public void SmallGame1()
    {
        if (Whole.Game1==0)
        {
            SceneManager.LoadScene("Game1Teach");
        }
        else
        {
            SceneManager.LoadScene("Game1");
        }
    }
    public void SmallGame2()
    {
        if (Whole.Game1 == 0)
        {
            SceneManager.LoadScene("Game2Teach");
        }
        else
        {
            SceneManager.LoadScene("Game2");
        }
    }
    public void SmallGame3()
    {
        if (Whole.Game1 == 0)
        {
            SceneManager.LoadScene("Game3Teach");
        }
        else
        {
            SceneManager.LoadScene("Game3");
        }
    }

    public void Backto()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ChoseCareer");
    }
}
