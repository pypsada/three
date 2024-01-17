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
    public int ArroganceNumber;
    public bool Thiefing;  //神偷判定

    public Text countDownText; // 倒计时文本
    public float countDownTimer = 5f; // 倒计时时间
    public Text GroundText; //回合数文本

    void Start()
    {
        Ground = 1;
        RascallyNumber = 1;
        ArroganceNumber = 0;
        StringCareer = Whole.PlayerCareer;
        Career = 0;
        Energy = 0;   //初始化
        //myAnim = GetComponent<Animator>();
        countDownText = countDownText.GetComponent<Text>();
        GroundText = GroundText.GetComponent<Text>();
        AI = FindObjectOfType<AI>();
        if (StringCareer == "Thief" || StringCareer == "Assassin" || StringCareer=="Guard" || StringCareer== "Rascally" || StringCareer== "Arrogance")
        {
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
            RubbingEnergy();
            Despare();
        }
    }

    public void RubbingEnergy()   //能量
    {
        Energy += 1;
        Debug.Log("You:RubbingEnergy");
        Priority = 0;
    }

    public void Gun()  //枪
    {
        Priority = 1;
        Energy -= 1;
        Debug.Log("You:Gun");
    }

    public void Rebound()   //反弹
    {
        Priority = 100;
        Rebounding = true;
        Energy -= 2;
        Debug.Log("You:Rebound");
    }

    public void Defense()   //防御
    {
        Priority = 1;
        Defensing = true;
        Debug.Log("You:Defense");
    }

    public void HolyGrail()   //大招
    {
        Priority = 2;
        Energy -= 4;
        Debug.Log("You:HolyGrail");
    }

    public void Assassinate()  // 刺客技能：暗杀
    {
        Priority = 1;
        Career -= 1;
        Debug.Log("You:Assassinate");
    }

    public void Steal()   // 盗贼技能：偷取
    {
        Priority = 1;
    }

    public void King() // 国王技能：王权
    {
        Priority = 2;
        Energy -= 2;
        Debug.Log("You:King");
    }

    public void Guard()  // 护卫技能：能防
    {
        Priority = 1;
        Defensing = true;
        Energy += 1;
        Debug.Log("You:Guard");
    }

    public void Turtle()  //乌龟技能：龟缩
    {
        Energy -= 1;
        Priority = 100;
        Rebounding = true;
        Debug.Log("You:Turtle");
    }

    public void Rascally()  //老赖技能：汲能
    {
        Priority = 0;
        Energy += RascallyNumber;
        RascallyNumber += 1;
    }

    public void Arrogance()  //傲慢技能：嘲讽
    {
        ArroganceNumber += 1;
        Priority = 0;
    }

    public void Thief()  //盗贼技能：神偷
    {
        Career -= 1;
        Thiefing = true;
    }

    public void Despare()   //判定
    {
        Ground += 1;
        if (Thiefing)
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
                AI.Energy -= 1;
                Energy += 1;
                Career += 1;
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
                Chose = false;
                Priority = 0;
                Defensing = false;
                Rebounding= false;
                AI.Rebounding = false;
                countDownTimer = 5f;
                AI.Priority = 0;
                AI.Defensing = false;
                Debug.Log("Continue");
            }
            else if (Defensing==true && AI.Priority!=2)  //玩家防御，AI不用大招，继续游戏
            {
                Chose = false;
                Priority = 0;
                Defensing = false;
                Rebounding = false;
                AI.Rebounding = false;
                countDownTimer = 5f;
                AI.Priority = 0;
                AI.Defensing = false;
                Debug.Log("Continue");
            }
            else if(Rebounding==true && (AI.Priority==0 || (AI.Priority!=0 && AI.Defensing==true)))  //玩家反弹失败
            {
                Chose = false;
                Priority = 0;
                Defensing = false;
                Rebounding = false;
                AI.Rebounding = false;
                countDownTimer = 5f;
                AI.Priority = 0;
                AI.Defensing = false;
                Debug.Log("Continue");
            }
            else if (Priority==0 && (AI.Defensing==true || AI.Priority==0 || AI.Rebounding==true))  //玩家搓能量，AI不攻击
            {
                Chose = false;
                Priority = 0;
                Defensing = false;
                Rebounding = false;
                AI.Rebounding = false;
                countDownTimer = 5f;
                AI.Priority = 0;
                AI.Defensing = false;
                Debug.Log("Continue");
            }
            else  //否则就是玩家赢
            {
                Destroy(AIgameobject);
                Debug.Log("WIN");
            }           
        }

        if (ArroganceNumber>=3)
        {
            Destroy(AIgameobject);
            Debug.Log("WIN");
        }
        if (StringCareer== "Assassin" || StringCareer=="Thief")   //刺客,盗贼职业点数判定
        {
            if (Ground%2==1)
            {
                Career += 1;
            }
        }
        else if (StringCareer=="King")  //国王职业点数判定
        {
            Career = Energy / 2;
        }
        else if (StringCareer== "Guard" || StringCareer== "Rascally" || StringCareer== "Arrogance")  //卫士职业点数判定
        {
            Career = 1;
        }
        else if (StringCareer == "Turtle")  //乌龟职业点数判定
        {
            Career = Energy;
        }
    }
}
