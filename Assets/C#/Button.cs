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
        ai.AIplaying();
        Player.RubbingEnergy();
        Player.Despare();
    }

    public void Gun()  //ǹ
    {
        if (Player.Energy >= 1)
        {
            ai.AIplaying();
            Player.Gun();
            Player.Despare();
        }

    }

    public void Rebound()  //����
    {
        if (Player.Energy >= 2)
        {
            ai.AIplaying();
            Player.Rebound();
            Player.Despare();
        }

    }

    public void Defense()  //����
    {
        ai.AIplaying();
        Player.Defense();
        Player.Despare();
    }

    public void HolyGrail()  //����
    {
        if (Player.Energy >= 4)
        {
            ai.AIplaying();
            Player.HolyGrail();

            Player.Despare();
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
        SceneManager.LoadScene("PK&AI");
    }

    public void AIKing()  //AIѡ�����ְҵ
    {
        Whole.AICareer = "King";
        SceneManager.LoadScene("PK&AI");
    }

    public void AIGuard()  //AIѡ����ְҵ
    {
        Whole.AICareer = "Guard";
        SceneManager.LoadScene("PK&AI");
    }

    public void AIChoseTurtle()  //AIѡ���ڹ�ְҵ
    {
        Whole.AICareer = "Turtle";
        SceneManager.LoadScene("PK&AI");
    }

    public void AIChoseRascally()  //AIѡ������ְҵ
    {
        Whole.AICareer = "Rascally";
        SceneManager.LoadScene("PK&AI");
    }

    public void AIChoseArrogance()  //AIѡ�����ְҵ
    {
        Whole.AICareer = "Arrogance";
        SceneManager.LoadScene("PK&AI");
    }

    public void AIChoseThief()  //ѡ�����ְҵ
    {
        Whole.AICareer = "Thief";
        SceneManager.LoadScene("PK&AI");
    }

    public void AIChosePangolin()  //AIѡ��ɽ��ְҵ
    {
        Whole.AICareer = "Pangolin";
        SceneManager.LoadScene("PK&AI");
    }

    public void VocationalSkills()  //ʹ��ְҵ����
    {
        if (Player.Career>0)
        {
            if (Player.StringCareer== "Assassin")
            {
                ai.AIplaying();
                Player.Assassinate();
                Player.Despare();
            }
            if (Player.StringCareer=="King")
            {
                ai.AIplaying();
                Player.King();
                Player.Despare();
            }
            if(Player.StringCareer=="Guard")
            {
                ai.AIplaying();
                Player.Guard();
                Player.Despare();
            }
            if (Player.StringCareer == "Turtle")
            {
                ai.AIplaying();
                Player.Turtle();
                Player.Despare();
            }
            if (Player.StringCareer == "Rascally")
            {
                ai.AIplaying();
                Player.Rascally();
                Player.Despare();
            }
            if (Player.StringCareer == "Arrogance")
            {
                ai.AIplaying();
                Player.Arrogance();
                Player.Despare();
            }
            if (Player.StringCareer == "Thief")
            {
                ai.AIplaying();
                Player.Thief();
                Player.Despare();
            }
            if (Player.StringCareer == "Pangolin")
            {
                ai.AIplaying();
                Player.Pangolin();
                Player.Despare();
            }
        }

    }

}
