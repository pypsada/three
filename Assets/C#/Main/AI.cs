using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float Laolai;

    public int health;
    public bool Win;
    public int Energy;
    private Animator myAnim;
    public int Priority = 0;
    public bool Rebounding = false;
    public bool Defensing = false;
    public Player player;
    public int chose;
    public string StringCareer;
    public int Career;
    public int ArroganceNumber;  //傲慢点数
    public bool Thiefing;
    public int RascallyNumber;
    public int PangolinNumber;
    public bool IsPangolin;
    public bool CiSha;
    void Start()
    {
        Laolai = 1f;
        health = 8 * Whole.AICharacterlevel + 60;
        Win = true;
        PangolinNumber = 0;
        Thiefing = false; Defensing = false;
        StringCareer =Whole.AICareer;
        Career = 0;
        RascallyNumber = 1;
        ArroganceNumber = 0;
        Energy = 0;   //初始化
        myAnim = GetComponent<Animator>();
        player = FindObjectOfType<Player>(); // 获取Player脚本的引用

        if (StringCareer == "Thief" || StringCareer == "Assassin" || StringCareer == "Guard" ||
            StringCareer == "Arrogance" || StringCareer == "Pangolin")
        {
            Career = 1;
        }
        if (StringCareer == "Rascally")
        {
            Career = 1;
            health = 6 * Whole.Characterlevel + 50;
        }
        if (StringCareer == "Turtle")
        {
            Energy = 1;
            Career = 1;
            health = 10 * Whole.AICharacterlevel + 80;
        }
        if (StringCareer == "Assassin")
        {
            health = 6 * Whole.AICharacterlevel + 50;
        }
    }

    void Update()
    {

    }

    public void RubbingEnergy()
    {
        Energy += 1;
        Priority = 0;
        myAnim.SetTrigger("Cuo");
    }

    public void Gun()
    {
        Priority = 1;
        Energy -= 1;
        myAnim.SetTrigger("Gun");
    }

    public void Rebound()
    {
        Priority = 100;
        Rebounding = true;
        Energy -= 2;
        myAnim.SetTrigger("Rebound");
    }

    public void Defense()
    {
        Priority = 1;
        Defensing = true;
        myAnim.SetTrigger("Defense");
    }

    public void HolyGrail()
    {
        Priority = 2;
        Energy -= 2;
        myAnim.SetTrigger("King");
    }

    public void Assassinate()  // 刺客技能：暗杀
    {
        Priority = 1;
        Career -= 1;
        myAnim.SetTrigger("King");
    }

    public void Steal()   // 盗贼技能：偷取
    {
        Priority = 1;
        myAnim.SetTrigger("Steal");
    }

    public void King() // 国王技能：王权
    {
        Priority = 2;
        Energy -= 2;
        myAnim.SetTrigger("King");
    }

    public void Guard()  // 护卫技能：能防
    {
        Priority = 1;
        Defensing = true;
        Energy += 1;
        myAnim.SetTrigger("Cuo");
    }

    public void Turtle()  //乌龟技能：龟缩
    {
        Energy -= 1;
        Priority = 100;
        Rebounding = true;
        myAnim.SetTrigger("Turtle");
    }

    public void Rascally()  //老赖技能：汲能
    {
        Priority = 0;
        Energy += RascallyNumber;
        RascallyNumber =(int)Laolai;
        Laolai += 0.5f;
        myAnim.SetTrigger("Cuo");
    }

    public void Arrogance()  //傲慢技能：嘲讽
    {
        ArroganceNumber += 1;
        Priority = 0;
        myAnim.SetTrigger("Arrogant");
    }

    public void Thief()  //盗贼技能：神偷
    {
        Career -= 1;
        Thiefing = true;
        Priority = 0;
        myAnim.SetTrigger("Steal");
    }

    public void Pangolin() //穿山甲技能：叠甲
    {
        PangolinNumber += 1;
        Priority = 0;
        IsPangolin = true;
        myAnim.SetTrigger("King");
    }

    public void AIplaying()
    {
        if (StringCareer== "Assassin")  //刺客职业判定
        {
            if (Career!=0)
            {
                if(Energy <= 0 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //0能量，玩家有攻击手段
                {
                    chose = Random.Range(0, 3);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Assassinate();
                        Debug.Log("AI:Assassinate");
                    }
                    else if (chose == 2)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                }
                else if (Energy <= 0)  //这样这里就是能量为0，玩家没有攻击手段
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Assassinate();
                        Debug.Log("AI:Assassinate");
                    }
                }
                else if (Energy<2 && (player.Energy!=0 || (Whole.PlayerCareer== "Assassin" && player.Career!=0)))  //能量<2,玩家有攻击手段
                {
                    chose = Random.Range(0, 4);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Assassinate();
                        Debug.Log("AI:Assassinate");
                    }
                    else if (chose == 2)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 3)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy < 2)
                {
                    chose = Random.Range(0, 3);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Assassinate();
                        Debug.Log("AI:Assassinate");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy<4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<4，玩家有攻击手段
                {
                    chose = Random.Range(0, 5);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Assassinate();
                        Debug.Log("AI:Assassinate");
                    }
                    else if (chose == 2)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 3)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose==4)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                }
                else if (Energy<4)  //能量<4，玩家无攻击手段（不用防御和反弹）
                {
                    chose = Random.Range(0, 3);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Assassinate();
                        Debug.Log("AI:Assassinate");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy>=4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))
                {
                    chose = Random.Range(0, 6);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Assassinate();
                        Debug.Log("AI:Assassinate");
                    }
                    else if (chose == 2)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 3)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 4)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                    else if (chose == 5)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
                else
                {
                    chose = Random.Range(0, 4);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Assassinate();
                        Debug.Log("AI:Assassinate");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 3)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
            }
            else
            {
                if (Energy <= 0 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //0能量，玩家有攻击手段
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                }
                else if (Energy <= 0)  //这样这里就是能量为0，玩家没有攻击手段
                {
                    RubbingEnergy();
                    Debug.Log("AI:RubbingEnergy");
                }
                else if (Energy < 2 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<2,玩家有攻击手段
                {
                    chose = Random.Range(0, 3);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy < 2)
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy < 4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<4，玩家有攻击手段
                {
                    chose = Random.Range(0, 4);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 3)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                }
                else if (Energy < 4)  //能量<4，玩家无攻击手段（不用防御和反弹）
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy >= 4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))
                {
                    chose = Random.Range(0, 5);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 3)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                    else if (chose == 4)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
                else
                {
                    chose = Random.Range(0, 3);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 2)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
            }
        }
        else if (StringCareer == "Thief")
        {
            if (Career != 0)
            {
                if (Energy <= 0 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //0能量，玩家有攻击手段
                {
                    chose = Random.Range(1, 3);
                    if (chose == 1)
                    {
                        Thief();
                        Debug.Log("AI:Thief");
                    }
                    else if (chose == 2)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                }
                else if (Energy <= 0)  //这样这里就是能量为0，玩家没有攻击手段
                {
                    chose = Random.Range(1, 2);
                    if (chose == 1)
                    {
                        Thief();
                        Debug.Log("AI:Thief");
                    }
                }
                else if (Energy < 2 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<2,玩家有攻击手段
                {
                    chose = Random.Range(1, 4);
                    if (chose == 1)
                    {
                        Thief();
                        Debug.Log("AI:Thief");
                    }
                    else if (chose == 2)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 3)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy < 2)
                {
                    chose = Random.Range(1, 3);
                    if (chose == 1)
                    {
                        Thief();
                        Debug.Log("AI:Thief");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy < 4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<4，玩家有攻击手段
                {
                    chose = Random.Range(1, 5);
                    if (chose == 1)
                    {
                        Thief();
                        Debug.Log("AI:Thief");
                    }
                    else if (chose == 2)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 3)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 4)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                }
                else if (Energy < 4)  //能量<4，玩家无攻击手段（不用防御和反弹）
                {
                    chose = Random.Range(1, 3);
                    if (chose == 1)
                    {
                        Thief();
                        Debug.Log("AI:Thief");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy >= 4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))
                {
                    chose = Random.Range(1, 6);
                    if (chose == 1)
                    {
                        Thief();
                        Debug.Log("AI:Thief");
                    }
                    else if (chose == 2)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 3)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 4)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                    else if (chose == 5)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
                else
                {
                    chose = Random.Range(1, 4);
                    if (chose == 1)
                    {
                        Thief();
                        Debug.Log("AI:Thief");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 3)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
            }
            else
            {
                if (Energy <= 0 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //0能量，玩家有攻击手段
                {
                    chose = Random.Range(1, 2);
                    if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                }
                else if (Energy == 0)  //这样这里就是能量为0，玩家没有攻击手段
                {
                    Defense();
                    Debug.Log("AI:Defense");
                }
                else if (Energy < 2 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<2,玩家有攻击手段
                {
                    chose = Random.Range(1, 3);
                    if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy < 2)
                {
                    chose = Random.Range(1, 2);
                    if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy < 4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<4，玩家有攻击手段
                {
                    chose = Random.Range(1, 4);
                    if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 3)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                }
                else if (Energy < 4)  //能量<4，玩家无攻击手段（不用防御和反弹）
                {
                    chose = Random.Range(1, 2);
                    if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy >= 4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))
                {
                    chose = Random.Range(1, 5);
                    if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 3)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                    else if (chose == 4)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
                else
                {
                    chose = Random.Range(1, 3);
                    if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 2)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
            }
        }
        else if (StringCareer=="King")
        {
            if (Career != 0)
            {
                if (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0))  //玩家有攻击手段
                {
                    chose = Random.Range(0, 4);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        King();
                        Debug.Log("AI:King");
                    }
                    else if (chose == 2)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 3)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                }
                else  //玩家无攻击手段（不用防御和反弹）
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        King();
                        Debug.Log("AI:King");
                    }
                }
            }
            else
            {
                if (Energy <= 0 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //0能量，玩家有攻击手段
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                }
                else if (Energy <= 0)  //这样这里就是能量为0，玩家没有攻击手段
                {
                    RubbingEnergy();
                    Debug.Log("AI:RubbingEnergy");
                }
                else if (Energy < 2 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<2,玩家有攻击手段
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                }
                else if (Energy < 2)
                {
                    chose = Random.Range(0, 1);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                }
            }
        }
        else if (StringCareer== "Turtle")
        {
            if (Career != 0)
            {
                if (Energy < 4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<4，玩家有攻击手段
                {
                    chose = Random.Range(0, 4);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Turtle();
                        Debug.Log("AI:Turtle");
                    }
                    else if (chose == 2)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 3)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy < 4)  //能量<4，玩家无攻击手段（不用防御和反弹）
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy >= 4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))
                {
                    chose = Random.Range(0, 5);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Turtle();
                        Debug.Log("AI:Turtle");
                    }
                    else if (chose == 2)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 3)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 4)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
                else
                {
                    chose = Random.Range(0, 3);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 2)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
            }
            else
            {
                if (Energy <= 0 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //0能量，玩家有攻击手段
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        RubbingEnergy();
                        Debug.Log("AI:RubbingEnergy");
                    }
                    else if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                }
                else if (Energy <= 0)  //这样这里就是能量为0，玩家没有攻击手段
                {
                    RubbingEnergy();
                    Debug.Log("AI:RubbingEnergy");
                }
            }
        }
        else if (StringCareer=="Guard")
        {
            if (Career != 0)
            {
                if (Energy <= 0 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //0能量，玩家有攻击手段
                {
                    chose = Random.Range(1, 2);
                    if (chose == 1)
                    {
                        Guard();
                        Debug.Log("AI:Guard");
                    }
                }
                else if (Energy <= 0)  //这样这里就是能量为0，玩家没有攻击手段
                {
                    chose = Random.Range(0, 1);
                    if (chose == 0)
                    {
                        Guard();
                        Debug.Log("AI:Guard");
                    }
                }
                else if (Energy < 2 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<2,玩家有攻击手段
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        Guard();
                        Debug.Log("AI:Guard");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy < 2)
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        Guard();
                        Debug.Log("AI:Guard");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy < 4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<4，玩家有攻击手段
                {
                    chose = Random.Range(0, 3);
                    if (chose == 0)
                    {
                        Guard();
                        Debug.Log("AI:Guard");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 2)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                }
                else if (Energy < 4)  //能量<4，玩家无攻击手段（不用防御和反弹）
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        Guard();
                        Debug.Log("AI:Guard");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy >= 4 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))
                {
                    chose = Random.Range(0, 4);
                    if (chose == 0)
                    {
                        Guard();
                        Debug.Log("AI:Guard");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 2)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                    else if (chose == 3)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
                else
                {
                    chose = Random.Range(0, 3);
                    if (chose == 0)
                    {
                        Guard();
                        Debug.Log("AI:Guard");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 2)
                    {
                        HolyGrail();
                        Debug.Log("AI:HolyGrail");
                    }
                }
            }
        }
        else if (StringCareer== "Rascally")
        {
            if (Career != 0)
            {
                if (Energy <= 0 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //0能量，玩家有攻击手段
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        Rascally();
                        Debug.Log("AI:Rascally");
                    }
                    else if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                }
                else if (Energy <= 0)  //这样这里就是能量为0，玩家没有攻击手段
                {
                    chose = Random.Range(0, 1);
                    if (chose == 0)
                    {
                        Rascally();
                        Debug.Log("AI:Rascally");
                    }
                }
                else if (Energy < 2 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量<2,玩家有攻击手段
                {
                    chose = Random.Range(0, 3);
                    if (chose == 0)
                    {
                        Rascally();
                        Debug.Log("AI:Rascally");
                    }
                    else if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy < 2)
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        Rascally();
                        Debug.Log("AI:Rascally");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
                else if (Energy >= 2 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //能量>=2，玩家有攻击手段
                {
                    chose = Random.Range(0, 4);
                    if (chose == 0)
                    {
                        Rascally();
                        Debug.Log("AI:Rascally");
                    }
                    else if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                    else if (chose == 2)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                    else if (chose == 3)
                    {
                        Rebound();
                        Debug.Log("AI:Rebound");
                    }
                }
                else if (Energy >=2 )  //能量>=2，玩家无攻击手段（不用防御和反弹）
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        Rascally();
                        Debug.Log("AI:Rascally");
                    }
                    else if (chose == 1)
                    {
                        Gun();
                        Debug.Log("AI:Gun");
                    }
                }
            }
        }
        else if (StringCareer == "Arrogance")
        {
            if (Career != 0)
            {
                if (Energy <= 0 && (player.Energy != 0 || (Whole.PlayerCareer == "Assassin" && player.Career != 0)))  //0能量，玩家有攻击手段
                {
                    chose = Random.Range(0, 2);
                    if (chose == 0)
                    {
                        Arrogance();
                        Debug.Log("AI:Arrogance");
                    }
                    else if (chose == 1)
                    {
                        Defense();
                        Debug.Log("AI:Defense");
                    }
                }
                else if (Energy <= 0)  //这样这里就是能量为0，玩家没有攻击手段
                {
                    Arrogance();
                    Debug.Log("AI:Arrogance");
                }
            }
        }
    }

    public void Sum()
    {
        myAnim.SetTrigger("Idle");
    }
}
