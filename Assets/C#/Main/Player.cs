using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string zhaoshi;
    public int health;  //Ѫ��
    public int Energy;
    private Animator myAnim;
    public int Priority = 0;  //���ȼ�
    public bool Rebounding = false;  //�Ƿ񷴵�
    public bool Defensing = false;  //�Ƿ����
    public AI AI;
    public GameObject AIgameobject;
    public bool Chose = false;    //�Ƿ�ѡ���˼���
    public int Career;  //���ܵ���(ְҵ���ܵ���)
    public string StringCareer;  //ְҵ����
    public int Ground;  //�غ���
    public int RascallyNumber;  //�������ܻ�ȡ������
    public int ArroganceNumber;  //��������
    public bool Thiefing;  //��͵�ж�
    public int PangolinNumber; //��ɽ�׵��Ӳ���
    public bool IsPangolin;  //�����ж�
    public bool Continue;

    public Text countDownText; // ����ʱ�ı�

    public Text WinOrLose;  //��Ӯ�ı�
    public GameObject TanChuang;

    public float countDownTimer = 10f; // ����ʱʱ��
    public Text GroundText; //�غ����ı�

    public bool Win=true;  //�Ƿ��ʤ

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
        Energy = 0;   //��ʼ��
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

            // ��ʣ��ʱ����ʾ��UI�����ϵĵ���ʱ�ı���
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
    public void RubbingEnergy()   //����
    {
        Chose = true;
        zhaoshi = "RubbingEnergy";
        countDownTimer = 2f;
    }
    public void RubbingEnergy1()   //����
    {
        Energy += 1;
        Debug.Log("You:RubbingEnergy");
        Priority = 0;
        myAnim.SetTrigger("Cuo");
    }
    public void Gun()  //ǹ
    {
        Chose = true;
        zhaoshi = "Gun";
        countDownTimer = 2f;
    }
    public void Gun1()  //ǹ
    {
        Priority = 1;
        Energy -= 1;
        Debug.Log("You:Gun");
        myAnim.SetTrigger("Gun");
    }
    public void Rebound()   //����
    {
        Chose = true;
        zhaoshi = "Rebound";
        countDownTimer = 2f;
    }
    public void Rebound1()   //����
    {
        Priority = 100;
        Rebounding = true;
        Energy -= 2;
        Debug.Log("You:Rebound");
        myAnim.SetTrigger("Rebound");
    }
    public void Defense()   //����
    {
        Chose = true;
        zhaoshi = "Defense";
        countDownTimer = 2f;
    }
    public void Defense1()   //����
    {
        Priority = 1;
        Defensing = true;
        Debug.Log("You:Defense");
        myAnim.SetTrigger("Defense");
    }
    public void HolyGrail()   //����
    {
        Chose = true;
        zhaoshi = "HolyGrail1";
        countDownTimer = 2f;
    }
    public void HolyGrail1()   //����
    {
        Priority = 2;
        Energy -= 4;
        Debug.Log("You:HolyGrail");
        myAnim.SetTrigger("King");
    }
    public void Assassinate()  // �̿ͼ��ܣ���ɱ
    {
        Chose = true;
        zhaoshi = "Assassinate";
        countDownTimer = 2f;
    }
    public void Assassinate1()  // �̿ͼ��ܣ���ɱ
    {
        Priority = 1;
        Career -= 1;
        Debug.Log("You:Assassinate");
        myAnim.SetTrigger("King");
    }

    //public void Steal()   // �������ܣ�͵ȡ
    //{
    //    Chose = true;
    //    countDownTimer = 2f;
    //    Priority = 1;
    //}
    public void King() // �������ܣ���Ȩ
    {
        Chose = true;
        zhaoshi = "King";
        countDownTimer = 2f;
    }
    public void King1() // �������ܣ���Ȩ
    {
        Priority = 2;
        Energy -= 2;
        Debug.Log("You:King");
        myAnim.SetTrigger("King");
    }
    public void Guard()  // �������ܣ��ܷ�
    {
        Chose = true;
        zhaoshi = "Guard";
        countDownTimer = 2f;
    }
    public void Guard1()  // �������ܣ��ܷ�
    {
        Priority = 1;
        Defensing = true;
        Energy += 1;
        Debug.Log("You:Guard");
        myAnim.SetTrigger("Cuo");
    }
    public void Turtle()  //�ڹ꼼�ܣ�����
    {
        Chose = true;
        zhaoshi = "Turtle";
        countDownTimer = 2f;
    }
    public void Turtle1()  //�ڹ꼼�ܣ�����
    {
        Energy -= 1;
        Priority = 100;
        Rebounding = true;
        Debug.Log("You:Turtle");
        myAnim.SetTrigger("Turtle");
    }
    public void Rascally()  //�������ܣ�����
    {
        Chose = true;
        zhaoshi = "Rascally";
        countDownTimer = 2f;
    }
    public void Rascally1()  //�������ܣ�����
    {
        Priority = 0;
        Energy += RascallyNumber;
        RascallyNumber += 1;
        myAnim.SetTrigger("Cuo");
    }
    public void Arrogance()  //�������ܣ�����
    {
        Chose = true;
        zhaoshi = "Arrogance";
        countDownTimer = 2f;
    }
    public void Arrogance1()  //�������ܣ�����
    {
        ArroganceNumber += 1;
        Priority = 0;
        myAnim.SetTrigger("Arrogant");
    }
    public void Thief()  //�������ܣ���͵
    {
        Chose = true;
        zhaoshi = "Thief";
        countDownTimer = 2f;
    }
    public void Thief1()  //�������ܣ���͵
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

    public void Despare()   //�ж�
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
            if (Priority < AI.Priority && AI.Rebounding==false && AI.Defensing==false)  //AI�������ȼ�����
            {
                Win = false;
            }
            else if (Priority < AI.Priority && AI.Rebounding==true && Defensing==false && Priority!=0) //AI�����ɹ�
            {
                Win = false;
            }
            else if(Priority==AI.Priority)  //���ȼ�һ�����໥����
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
            else if (Defensing==true && AI.Priority!=2)  //��ҷ�����AI���ô��У�������Ϸ
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
            else if(Rebounding==true && (AI.Priority==0 || (AI.Priority!=0 && AI.Defensing==true)))  //��ҷ���ʧ��
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
            else if (Priority==0 && (AI.Defensing==true || AI.Priority==0 || AI.Rebounding==true))  //��Ҵ�������AI������
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
            else  //����������Ӯ
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

    public void Sum()  //����
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
            //ʱ����ͣ
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
        Chose = false;  //��ʼ��
        Priority = 0;
        Defensing = false;
        Rebounding = false;
        Thiefing = false;
        AI.Rebounding = false;
        AI.Priority = 0;
        AI.Defensing = false;
        AI.Thiefing = false;

        //AIְҵ�����ж�
        if (AI.StringCareer == "Assassin" || AI.StringCareer == "Thief")   //�̿�,����ְҵ�����ж�
        {
            if (Ground % 2 == 1)
            {
                AI.Career += 1;
            }
        }
        else if (AI.StringCareer == "King")  //����ְҵ�����ж�
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
        else if (AI.StringCareer == "Guard" || AI.StringCareer == "Rascally" || AI.StringCareer == "Arrogance")  //��ʿ��ְҵ�����ж�
        {
            AI.Career = 1;
        }
        else if (AI.StringCareer == "Turtle")  //�ڹ�ְҵ�����ж�
        {
            AI.Career = AI.Energy;
        }


        if (StringCareer == "Assassin" || StringCareer == "Thief")   //�̿�,����ְҵ�����ж�
        {
            if (Ground % 2 == 1)
            {
                Career += 1;
            }
        }
        else if (StringCareer == "King")  //����ְҵ�����ж�
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
        else if (StringCareer == "Guard" || StringCareer == "Rascally" || StringCareer == "Arrogance")  //��ʿ��ְҵ�����ж�
        {
            Career = 1;
        }
        else if (StringCareer == "Turtle")  //�ڹ�ְҵ�����ж�
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
