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
        Player = FindObjectOfType<Player>(); // 获取Player脚本的引用
        ai = FindObjectOfType<AI>();  //获取AI脚本的引用
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RubbingEnergy()  //搓能量
    {
        ai.AIplaying();
        Player.RubbingEnergy();
        Player.Despare();
    }

    public void Gun()  //枪
    {
        if (Player.Energy >= 1)
        {
            ai.AIplaying();
            Player.Gun();
            Player.Despare();
        }

    }

    public void Rebound()  //反弹
    {
        if (Player.Energy >= 2)
        {
            ai.AIplaying();
            Player.Rebound();
            Player.Despare();
        }

    }

    public void Defense()  //防御
    {
        ai.AIplaying();
        Player.Defense();
        Player.Despare();
    }

    public void HolyGrail()  //大招
    {
        if (Player.Energy >= 4)
        {
            ai.AIplaying();
            Player.HolyGrail();

            Player.Despare();
        }
    }

    public void ChoseAssassin()  //选择刺客职业
    {
        Whole.PlayerCareer = "Assassin";
        SceneManager.LoadScene("PK&AI");
    }

    public void ChoseKing()  //选择国王职业
    {
        Whole.PlayerCareer = "King";
        SceneManager.LoadScene("PK&AI");
    }

    public void ChoseGuard()  //选择护卫职业
    {
        Whole.PlayerCareer = "Guard";
        SceneManager.LoadScene("PK&AI");
    }

    public void ChoseTurtle()  //选择乌龟职业
    {
        Whole.PlayerCareer = "Turtle";
        SceneManager.LoadScene("PK&AI");
    }

    public void ChoseRascally()  //选择老赖职业
    {
        Whole.PlayerCareer = "Rascally";
        SceneManager.LoadScene("PK&AI");
    }

    public void ChoseArrogance()  //选择傲慢职业
    {
        Whole.PlayerCareer = "Arrogance";
        SceneManager.LoadScene("PK&AI");
    }

    public void ChoseThief()  //选择盗贼职业
    {
        Whole.PlayerCareer = "Thief";
        SceneManager.LoadScene("PK&AI");
    }

    public void ChosePangolin()
    {
        Whole.PlayerCareer = "Pangolin";
        SceneManager.LoadScene("PK&AI");
    }

    public void VocationalSkills()  //使用职业技能
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
