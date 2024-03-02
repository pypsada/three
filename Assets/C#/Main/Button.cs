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
        Player.RubbingEnergy();
    }

    public void Gun()  //枪
    {
        if (Player.Energy >= 1)
        {
            Player.Gun();
        }

    }

    public void Rebound()  //反弹
    {
        if (Player.Energy >= 2)
        {
            Player.Rebound();
        }

    }

    public void Defense()  //防御
    {
        Player.Defense();
    }

    public void HolyGrail()  //大招
    {
        if (Player.Energy >= 4)
        {
            Player.HolyGrail();

        }
    }

    public void ChoseAssassin()  //选择刺客职业
    {
        Whole.PlayerCareer = "Assassin";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseKing()  //选择国王职业
    {
        Whole.PlayerCareer = "King";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseGuard()  //选择护卫职业
    {
        Whole.PlayerCareer = "Guard";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseTurtle()  //选择乌龟职业
    {
        Whole.PlayerCareer = "Turtle";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseRascally()  //选择老赖职业
    {
        Whole.PlayerCareer = "Rascally";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseArrogance()  //选择傲慢职业
    {
        Whole.PlayerCareer = "Arrogance";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChoseThief()  //选择盗贼职业
    {
        Whole.PlayerCareer = "Thief";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void ChosePangolin()  //选择穿山甲职业
    {
        Whole.PlayerCareer = "Pangolin";
        SceneManager.LoadScene("AIChoseCareer");
    }

    public void AIChoseAssassin()  //AI选择刺客职业
    {
        Whole.AICareer = "Assassin";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIKing()  //AI选择国王职业
    {
        Whole.AICareer = "King";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIGuard()  //AI选择护卫职业
    {
        Whole.AICareer = "Guard";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIChoseTurtle()  //AI选择乌龟职业
    {
        Whole.AICareer = "Turtle";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIChoseRascally()  //AI选择老赖职业
    {
        Whole.AICareer = "Rascally";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIChoseArrogance()  //AI选择傲慢职业
    {
        Whole.AICareer = "Arrogance";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIChoseThief()  //选择盗贼职业
    {
        Whole.AICareer = "Thief";
        SceneManager.LoadScene("FightModleAI");
    }

    public void AIChosePangolin()  //AI选择穿山甲职业
    {
        Whole.AICareer = "Pangolin";
        SceneManager.LoadScene("FightModleAI");
    }

    public void VocationalSkills()  //使用职业技能
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
