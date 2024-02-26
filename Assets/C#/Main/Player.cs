using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string zhaoshi;
    public int health;  //血量
    public int Energy;
    private Animator myAnim;
    public int Priority = 0;  //优先级
    public bool Rebounding = false;  //是否反弹
    public bool Defensing = false;  //是否防御
    public AI AI;
    public GameObject AIgameobject;
    public bool Chose = false;    //是否选择了技能
    public int Career;  //技能点数(职业技能点数)
    public string StringCareer;  //职业名字
    public int Ground;  //回合数
    public int RascallyNumber;  //老赖技能获取能量数
    public int ArroganceNumber;  //傲慢点数
    public bool Thiefing;  //神偷判定
    public int PangolinNumber; //穿山甲叠加层数
    public bool IsPangolin;  //叠加判定
    public bool Continue;

    public Text countDownText; // 倒计时文本

    public Text WinOrLose;  //输赢文本
    public GameObject TanChuang;

    public float countDownTimer = 10f; // 倒计时时间
    public Text GroundText; //回合数文本

    public bool Win=true;  //是否获胜

    void Start()
    {
        Continue = true;
        Win = true;


        countDownTimer = 10f;
        Ground = 1;
        RascallyNumber = 1;
        ArroganceNumber = 0;
        Thiefing = false;
        PangolinNumber = 0;
        StringCareer = Whole.PlayerCareer;
        Career = 0;
        Energy = 0;   //初始化
        myAnim = GetComponent<Animator>();
        countDownText = countDownText.GetComponent<Text>();
        GroundText = GroundText.GetComponent<Text>();
        AI = FindObjectOfType<AI>();
        health = (int)0.5 * Whole.Characterlevel + 1;
        if (StringCareer == "Thief" || StringCareer == "Assassin" || StringCareer=="Guard" || 
            StringCareer== "Rascally" || StringCareer== "Arrogance" ||StringCareer== "Pangolin")
        {
            Career = 1;
        }
        if (StringCareer =="Turtle")
        {
            Energy = 1;
            Career = 1;
            health = 1 * Whole.Characterlevel + 1;
        }
    }

    void Update()
    {
        GroundText.text = Ground.ToString();
        if (countDownTimer > 0f)
        {
            countDownTimer -= Time.deltaTime;

            // 将剩余时间显示在UI界面上的倒计时文本中
            countDownText.text = Mathf.RoundToInt(countDownTimer).ToString();
        }
        else
        {
            if (Continue)
            {
                AI.AIplaying();
                if (Chose==false)
                {
                    if (StringCareer== "Pangolin" || StringCareer=="Thief")
                    {
                        Defense();
                    }
                    else
                    {
                        RubbingEnergy();
                    }
                }
                Despare();
                Continue = false;
            }

        }
    }
    public void RubbingEnergy()   //能量
    {
        Chose = true;
        zhaoshi = "RubbingEnergy";
        countDownTimer = 2f;
    }
    public void RubbingEnergy1()   //能量
    {
        Energy += 1;
        Debug.Log("You:RubbingEnergy");
        Priority = 0;
        myAnim.SetTrigger("Cuo");
    }
    public void Gun()  //枪
    {
        Chose = true;
        zhaoshi = "Gun";
        countDownTimer = 2f;
    }
    public void Gun1()  //枪
    {
        Priority = 1;
        Energy -= 1;
        Debug.Log("You:Gun");
        myAnim.SetTrigger("Gun");
    }
    public void Rebound()   //反弹
    {
        Chose = true;
        zhaoshi = "Rebound";
        countDownTimer = 2f;
    }
    public void Rebound1()   //反弹
    {
        Priority = 100;
        Rebounding = true;
        Energy -= 2;
        Debug.Log("You:Rebound");
        myAnim.SetTrigger("Rebound");
    }
    public void Defense()   //防御
    {
        Chose = true;
        zhaoshi = "Defense";
        countDownTimer = 2f;
    }
    public void Defense1()   //防御
    {
        Priority = 1;
        Defensing = true;
        Debug.Log("You:Defense");
        myAnim.SetTrigger("Defense");
    }
    public void HolyGrail()   //大招
    {
        Chose = true;
        zhaoshi = "HolyGrail1";
        countDownTimer = 2f;
    }
    public void HolyGrail1()   //大招
    {
        Priority = 2;
        Energy -= 4;
        Debug.Log("You:HolyGrail");
        myAnim.SetTrigger("King");
    }
    public void Assassinate()  // 刺客技能：暗杀
    {
        Chose = true;
        zhaoshi = "Assassinate";
        countDownTimer = 2f;
    }
    public void Assassinate1()  // 刺客技能：暗杀
    {
        Priority = 1;
        Career -= 1;
        Debug.Log("You:Assassinate");
        myAnim.SetTrigger("King");
    }

    //public void Steal()   // 盗贼技能：偷取
    //{
    //    Chose = true;
    //    countDownTimer = 2f;
    //    Priority = 1;
    //}
    public void King() // 国王技能：王权
    {
        Chose = true;
        zhaoshi = "King";
        countDownTimer = 2f;
    }
    public void King1() // 国王技能：王权
    {
        Priority = 2;
        Energy -= 2;
        Debug.Log("You:King");
        myAnim.SetTrigger("King");
    }
    public void Guard()  // 护卫技能：能防
    {
        Chose = true;
        zhaoshi = "Guard";
        countDownTimer = 2f;
    }
    public void Guard1()  // 护卫技能：能防
    {
        Priority = 1;
        Defensing = true;
        Energy += 1;
        Debug.Log("You:Guard");
        myAnim.SetTrigger("Cuo");
    }
    public void Turtle()  //乌龟技能：龟缩
    {
        Chose = true;
        zhaoshi = "Turtle";
        countDownTimer = 2f;
    }
    public void Turtle1()  //乌龟技能：龟缩
    {
        Energy -= 1;
        Priority = 100;
        Rebounding = true;
        Debug.Log("You:Turtle");
        myAnim.SetTrigger("Turtle");
    }
    public void Rascally()  //老赖技能：汲能
    {
        Chose = true;
        zhaoshi = "Rascally";
        countDownTimer = 2f;
    }
    public void Rascally1()  //老赖技能：汲能
    {
        Priority = 0;
        Energy += RascallyNumber;
        RascallyNumber += 1;
        myAnim.SetTrigger("Cuo");
    }
    public void Arrogance()  //傲慢技能：嘲讽
    {
        Chose = true;
        zhaoshi = "Arrogance";
        countDownTimer = 2f;
    }
    public void Arrogance1()  //傲慢技能：嘲讽
    {
        ArroganceNumber += 1;
        Priority = 0;
        myAnim.SetTrigger("Arrogant");
    }
    public void Thief()  //盗贼技能：神偷
    {
        Chose = true;
        zhaoshi = "Thief";
        countDownTimer = 2f;
    }
    public void Thief1()  //盗贼技能：神偷
    {
        Career -= 1;
        Thiefing = true;
        Priority = 1;
        myAnim.SetTrigger("Steal");
    }

    public void Pangolin()
    {
        Chose = true;
        zhaoshi = "Pangolin";
        countDownTimer = 2f;
    }
    public void Pangolin1()
    {
        PangolinNumber += 1;
        Priority = 0;
        IsPangolin=true;
        myAnim.SetTrigger("King");
    }

    public void Despare()   //判定
    {
        Ground += 1;
        if (zhaoshi=="Thief")
        {
            Thief1();
        }
        else if (zhaoshi=="RubbingEnergy")
        {
            RubbingEnergy1();
        }
        else if (zhaoshi == "Gun")
        {
            Gun1();
        }
        else if (zhaoshi == "Guard")
        {
            Guard1();
        }
        else if (zhaoshi == "HolyGrail")
        {
            HolyGrail1();
        }
        else if (zhaoshi == "Rebound")
        {
            Rebound1();
        }
        else if (zhaoshi == "Defense")
        {
            Defense1();
        }
        else if (zhaoshi == "Assassinate")
        {
            Assassinate1();
        }
        else if (zhaoshi == "King")
        {
            King1();
        }
        else if (zhaoshi == "Pangolin")
        {
            Pangolin1();
        }
        else if (zhaoshi == "Turtle")
        {
            Turtle1();  
        }
        else if (zhaoshi == "Rascally")
        {
            Rascally1();
        }
        else if (zhaoshi == "Arrogance")
        {
            Arrogance1();
        }
        if (Thiefing)
        {
            if (AI.Defensing==true || AI.Rebounding==true)  
            {
                Career -= 1;
            }
            else if (AI.Defensing==false && AI.Priority>0)
            {
                AI.Win=false;
            }
            else if (AI.Priority==0)
            {
                if (AI.Thiefing==true)
                {
                    Debug.Log("Continue");
                }
                else
                {
                    AI.Energy -= 1;
                    Energy += 1;
                    Career += 1;
                    Debug.Log("Continue");
                }
            }
            Thiefing=false;
        }
        else
        {
            if (Priority < AI.Priority && AI.Rebounding==false && AI.Defensing==false)  //AI攻击优先级更高
            {
                Win = false;
            }
            else if (Priority < AI.Priority && AI.Rebounding==true && Defensing==false && Priority!=0) //AI反弹成功
            {
                Win = false;
            }
            else if(Priority==AI.Priority)  //优先级一样，相互抵消
            {
                if (AI.Thiefing==true)
                {
                    Energy -= 1;
                    AI.Energy += 1;
                    AI.Career += 1;
                    Debug.Log("Continue");
                }
                else
                {
                    Debug.Log("Continue");
                }

            }
            else if (Defensing==true && AI.Priority!=2)  //玩家防御，AI不用大招，继续游戏
            {
                if (AI.Thiefing==true)
                {
                    AI.Career -= 1;
                    Debug.Log("Continue");
                }
                else
                {
                    Debug.Log("Continue");
                }

            }
            else if(Rebounding==true && (AI.Priority==0 || (AI.Priority!=0 && AI.Defensing==true)))  //玩家反弹失败
            {
                if (AI.Thiefing==true)
                {
                    AI.Career -= 1;
                    Debug.Log("Continue");
                }
                else
                {
                    Debug.Log("Continue");
                }

            }
            else if (Priority==0 && (AI.Defensing==true || AI.Priority==0 || AI.Rebounding==true))  //玩家搓能量，AI不攻击
            {
                if (AI.Thiefing==true)
                {
                    AI.Career += 1;
                    AI.Energy += 1;
                }
                else
                {
                    Debug.Log("Continue");
                }

            }
            else  //否则就是玩家赢
            {
                if (AI.Thiefing==true)
                {
                    Win = false;
                }
                else
                {
                    AI.Win = false;
                }

            }           
        }

    }

    public void Sum()  //结算
    {
        if (AI.Win == false)
        {
            if (Thiefing)
            {
                if (AI.Priority == 1)
                {
                    AI.health -= 1+(int)0.2*Whole.AICharacterlevel;
                }
                else if (AI.Priority == 2)
                {
                    AI.health -= 2+(int)0.3*Whole.AICharacterlevel;
                }
            }
            else if (Rebounding==true)
            {
                if (AI.Priority==1)
                {
                    AI.health -= 1 + (int)0.2 * Whole.AICharacterlevel;
                }
                else if(AI.Priority==2)
                {
                    AI.health -= 2 + (int)0.3 * Whole.AICharacterlevel;
                }
            }
            else
            {
                if (Priority == 1)
                {
                    AI.health -= 1 + (int)0.2 * Whole.Characterlevel;
                }
                else if (Priority == 2)
                {
                    AI.health -= 2 + (int)0.3 * Whole.Characterlevel;
                }
            }
        }
        else if (Win == false)
        {
            if (AI.Thiefing)
            {
                if (Priority == 1)
                {
                    health -= 1 + (int)0.2 * Whole.Characterlevel;
                }
                else if (Priority == 2)
                {
                    health -= 2 + (int)0.3 * Whole.Characterlevel;
                }
            }
            else if (AI.Rebounding == true)
            {
                if (Priority == 1)
                {
                    health -= 1 + (int)0.2 * Whole.Characterlevel;
                }
                else if (Priority == 2)
                {
                    health -= 2 + (int)0.3 * Whole.Characterlevel;
                }
            }
            else
            {
                if (AI.Priority == 1)
                {
                    health -= 1 + (int)0.2 * Whole.AICharacterlevel;
                }
                else if (AI.Priority == 2)
                {
                    health -= 2 + (int)0.3 * Whole.AICharacterlevel;
                }
            }
        }
        else
        {
            Debug.Log("Continue");
        }

        if (health<=0)
        {
            TanChuang.SetActive(true);
            Time.timeScale = 0;
            WinOrLose.text = "Lose";
            Destroy(gameObject);
            //Debug.Log("LOST");
            //时间暂停
        }
        if (AI.health<=0)
        {
            TanChuang.SetActive(true);
            Time.timeScale = 0;
            WinOrLose.text = "Win";
            Destroy(AIgameobject);
            //Debug.Log("Win");
        }

        Win = true;
        AI.Win = true;
        Chose = false;  //初始化
        Priority = 0;
        Defensing = false;
        Rebounding = false;
        Thiefing = false;
        AI.Rebounding = false;
        AI.Priority = 0;
        AI.Defensing = false;
        AI.Thiefing = false;

        //AI职业点数判定
        if (AI.StringCareer == "Assassin" || AI.StringCareer == "Thief")   //刺客,盗贼职业点数判定
        {
            if (Ground % 2 == 1)
            {
                AI.Career += 1;
            }
        }
        else if (AI.StringCareer == "King")  //国王职业点数判定
        {
            if (AI.Energy % 2 == 0)
            {
                AI.Career = AI.Energy / 2;
            }
            else
            {
                AI.Career = (AI.Energy - 1) / 2;
            }
        }
        else if (AI.StringCareer == "Guard" || AI.StringCareer == "Rascally" || AI.StringCareer == "Arrogance")  //卫士等职业点数判定
        {
            AI.Career = 1;
        }
        else if (AI.StringCareer == "Turtle")  //乌龟职业点数判定
        {
            AI.Career = AI.Energy;
        }


        if (StringCareer == "Assassin" || StringCareer == "Thief")   //刺客,盗贼职业点数判定
        {
            if (Ground % 2 == 1)
            {
                Career += 1;
            }
        }
        else if (StringCareer == "King")  //国王职业点数判定
        {
            if (Energy % 2 == 0)
            {
                Career = Energy / 2;
            }
            else
            {
                Career = (Energy - 1) / 2;
            }
        }
        else if (StringCareer == "Guard" || StringCareer == "Rascally" || StringCareer == "Arrogance")  //卫士等职业点数判定
        {
            Career = 1;
        }
        else if (StringCareer == "Turtle")  //乌龟职业点数判定
        {
            Career = Energy;
        }
        myAnim.SetTrigger("Idle");
        countDownTimer = 10f;
        Continue = true;

        if (gameObject!=null && AIgameobject!=null)
        {
            if (ArroganceNumber >= 3 && AI.ArroganceNumber >= 3)
            {
                AI.health -= (int)(0.3 * Whole.Characterlevel + 1.5);
                health -= (int)(0.3 * Whole.AICharacterlevel + 1.5);
                ArroganceNumber -= 3;
                AI.ArroganceNumber -= 3;
                Energy += 1;
                AI.Energy += 1;
                ArroganceSum();
            }
            else if (ArroganceNumber >= 3)
            {
                AI.health -= (int)(0.3 * Whole.Characterlevel + 1.5);
                ArroganceNumber -= 3;
                Energy += 1;
                ArroganceSum();
            }
            else if (AI.ArroganceNumber >= 3)
            {
                health -= (int)(0.3 * Whole.AICharacterlevel + 1.5);
                AI.ArroganceNumber -= 3;
                AI.Energy += 1;
                ArroganceSum();
            }
        }
    }

    public void ArroganceSum()
    {
        if (AI.health <= 0)
        {
            TanChuang.SetActive(true);
            Time.timeScale = 0;
            WinOrLose.text = "Win";
            Destroy(AIgameobject);
            //Debug.Log("Win");
        }
        if (health <= 0)
        {
            TanChuang.SetActive(true);
            Time.timeScale = 0;
            WinOrLose.text = "Lose";
            Destroy(gameObject);
            //Debug.Log("LOST");
        }
    }
}
