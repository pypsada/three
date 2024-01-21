using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int Energy;
    //private Animator myAnim;
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

    public Text countDownText; // 倒计时文本
    public float countDownTimer = 10f; // 倒计时时间
    public Text GroundText; //回合数文本

    void Start()
    {
        countDownTimer = 10f;
        Ground = 1;
        RascallyNumber = 1;
        ArroganceNumber = 0;
        Thiefing = false;
        PangolinNumber = 0;
        StringCareer = Whole.PlayerCareer;
        Career = 0;
        Energy = 0;   //初始化
        //myAnim = GetComponent<Animator>();
        countDownText = countDownText.GetComponent<Text>();
        GroundText = GroundText.GetComponent<Text>();
        AI = FindObjectOfType<AI>();
        if (StringCareer == "Thief" || StringCareer == "Assassin" || StringCareer=="Guard" || 
            StringCareer== "Rascally" || StringCareer== "Arrogance" ||StringCareer== "Pangolin")
        {
            Career = 1;
        }
        if (StringCareer =="Turtle")
        {
            Energy = 1;
            Career = 1;
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
        }
    }

    public void RubbingEnergy()   //能量
    {
        Chose = true;
        countDownTimer = 2f;
        Energy += 1;
        Debug.Log("You:RubbingEnergy");
        Priority = 0;
    }

    public void Gun()  //枪
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 1;
        Energy -= 1;
        Debug.Log("You:Gun");
    }

    public void Rebound()   //反弹
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 100;
        Rebounding = true;
        Energy -= 2;
        Debug.Log("You:Rebound");
    }

    public void Defense()   //防御
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 1;
        Defensing = true;
        Debug.Log("You:Defense");
    }

    public void HolyGrail()   //大招
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 2;
        Energy -= 4;
        Debug.Log("You:HolyGrail");
    }

    public void Assassinate()  // 刺客技能：暗杀
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 1;
        Career -= 1;
        Debug.Log("You:Assassinate");
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
        countDownTimer = 2f;
        Priority = 2;
        Energy -= 2;
        Debug.Log("You:King");
    }

    public void Guard()  // 护卫技能：能防
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 1;
        Defensing = true;
        Energy += 1;
        Debug.Log("You:Guard");
    }

    public void Turtle()  //乌龟技能：龟缩
    {
        Chose = true;
        countDownTimer = 2f;
        Energy -= 1;
        Priority = 100;
        Rebounding = true;
        Debug.Log("You:Turtle");
    }

    public void Rascally()  //老赖技能：汲能
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 0;
        Energy += RascallyNumber;
        RascallyNumber += 1;
    }

    public void Arrogance()  //傲慢技能：嘲讽
    {
        Chose = true;
        countDownTimer = 2f;
        ArroganceNumber += 1;
        Priority = 0;
    }

    public void Thief()  //盗贼技能：神偷
    {
        Chose = true;
        countDownTimer = 2f;
        Career -= 1;
        Thiefing = true;
        Priority = 1;
    }

    public void Pangolin()
    {
        Chose = true;
        countDownTimer = 2f;
        PangolinNumber += 1;
        Priority = 0;
        IsPangolin=true;
    }

    public void Despare()   //判定
    {
        Ground += 1;
        if (IsPangolin)  //穿山甲职业判定
        {
            if (PangolinNumber >= 5)  //秒杀
            {
                Destroy(AIgameobject);
                Debug.Log("WIN");
            }
            else if (PangolinNumber >= 3)  //免疫普攻判定
            {
                if (AI.Priority == 2)
                {
                    Destroy(gameObject);
                    Debug.Log("LOSE");
                }
                else
                {
                    Debug.Log("Continue");
                }
            }
            else  //否则就相当于普通搓
            {
                if (AI.Defensing==false && AI.Rebounding==false && AI.Priority>=2)
                {
                    Destroy(gameObject) ;
                    Debug.Log("LOSE");
                }
                else if (AI.Defensing == false && AI.Rebounding == false && AI.Priority == 1 && Defensing==false)
                {
                    Destroy(gameObject);
                    Debug.Log("LOSE");
                }
                else
                {
                    Debug.Log("Continue");
                }
            }
        }
        else if (Thiefing)
        {
            if (AI.Defensing==true || AI.Rebounding==true)  
            {
                Career -= 1;
            }
            else if (AI.Defensing==false && AI.Priority>0)
            {
                Destroy(AIgameobject);
                Debug.Log("WIN");
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
                Destroy(gameObject);
                Debug.Log("LOSE");
            }
            else if (Priority < AI.Priority && AI.Rebounding==true && Defensing==false && Priority!=0) //AI反弹成功
            {
                Destroy(gameObject);
                Debug.Log("LOSE");
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
                    Destroy(gameObject);
                    Debug.Log("LOSE");
                }
                else
                {
                    Destroy(AIgameobject);
                    Debug.Log("WIN");
                }

            }           
        }

        Chose = false;  //初始化
        Priority = 0;
        Defensing = false;
        Rebounding = false;
        Thiefing=false;
        AI.Rebounding = false;
        AI.Priority = 0;
        AI.Defensing = false;
        AI.Thiefing = false;


        if (ArroganceNumber>=3 && AI.ArroganceNumber>=3)
        {
            ArroganceNumber = 0;
            AI.ArroganceNumber = 0;
            Debug.Log("Continue");
        }
        else if (ArroganceNumber>=3)
        {
            Destroy(AIgameobject);
            Debug.Log("WIN");
        }
        else if(AI.ArroganceNumber>=3) 
        {
            Destroy(gameObject);
            Debug.Log("LOSE");
        }

        //AI职业点数判定
        if (AI.StringCareer== "Assassin" || AI.StringCareer=="Thief")   //刺客,盗贼职业点数判定
        {
            if (Ground%2==1)
            {
                AI.Career += 1;
            }
        }
        else if (AI.StringCareer=="King")  //国王职业点数判定
        {
            if (AI.Energy%2==0)
            {
                AI.Career = AI.Energy / 2;
            }
            else
            {
                AI.Career = (AI.Energy-1) / 2;
            }
        }
        else if (AI.StringCareer== "Guard" || AI.StringCareer== "Rascally" || AI.StringCareer== "Arrogance")  //卫士等职业点数判定
        {
            AI.Career = 1;
        }
        else if (AI.StringCareer == "Turtle")  //乌龟职业点数判定
        {
            AI.Career = Energy;
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

        countDownTimer = 10f;
    }
}
