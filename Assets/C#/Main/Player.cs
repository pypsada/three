using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int Energy;
    //private Animator myAnim;
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

    public Text countDownText; // ����ʱ�ı�
    public float countDownTimer = 10f; // ����ʱʱ��
    public Text GroundText; //�غ����ı�

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
        Energy = 0;   //��ʼ��
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

            // ��ʣ��ʱ����ʾ��UI�����ϵĵ���ʱ�ı���
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

    public void RubbingEnergy()   //����
    {
        Chose = true;
        countDownTimer = 2f;
        Energy += 1;
        Debug.Log("You:RubbingEnergy");
        Priority = 0;
    }

    public void Gun()  //ǹ
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 1;
        Energy -= 1;
        Debug.Log("You:Gun");
    }

    public void Rebound()   //����
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 100;
        Rebounding = true;
        Energy -= 2;
        Debug.Log("You:Rebound");
    }

    public void Defense()   //����
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 1;
        Defensing = true;
        Debug.Log("You:Defense");
    }

    public void HolyGrail()   //����
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 2;
        Energy -= 4;
        Debug.Log("You:HolyGrail");
    }

    public void Assassinate()  // �̿ͼ��ܣ���ɱ
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 1;
        Career -= 1;
        Debug.Log("You:Assassinate");
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
        countDownTimer = 2f;
        Priority = 2;
        Energy -= 2;
        Debug.Log("You:King");
    }

    public void Guard()  // �������ܣ��ܷ�
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 1;
        Defensing = true;
        Energy += 1;
        Debug.Log("You:Guard");
    }

    public void Turtle()  //�ڹ꼼�ܣ�����
    {
        Chose = true;
        countDownTimer = 2f;
        Energy -= 1;
        Priority = 100;
        Rebounding = true;
        Debug.Log("You:Turtle");
    }

    public void Rascally()  //�������ܣ�����
    {
        Chose = true;
        countDownTimer = 2f;
        Priority = 0;
        Energy += RascallyNumber;
        RascallyNumber += 1;
    }

    public void Arrogance()  //�������ܣ�����
    {
        Chose = true;
        countDownTimer = 2f;
        ArroganceNumber += 1;
        Priority = 0;
    }

    public void Thief()  //�������ܣ���͵
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

    public void Despare()   //�ж�
    {
        Ground += 1;
        if (IsPangolin)  //��ɽ��ְҵ�ж�
        {
            if (PangolinNumber >= 5)  //��ɱ
            {
                Destroy(AIgameobject);
                Debug.Log("WIN");
            }
            else if (PangolinNumber >= 3)  //�����չ��ж�
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
            else  //������൱����ͨ��
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
             if (Priority < AI.Priority && AI.Rebounding==false && AI.Defensing==false)  //AI�������ȼ�����
            {
                Destroy(gameObject);
                Debug.Log("LOSE");
            }
            else if (Priority < AI.Priority && AI.Rebounding==true && Defensing==false && Priority!=0) //AI�����ɹ�
            {
                Destroy(gameObject);
                Debug.Log("LOSE");
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

        Chose = false;  //��ʼ��
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

        //AIְҵ�����ж�
        if (AI.StringCareer== "Assassin" || AI.StringCareer=="Thief")   //�̿�,����ְҵ�����ж�
        {
            if (Ground%2==1)
            {
                AI.Career += 1;
            }
        }
        else if (AI.StringCareer=="King")  //����ְҵ�����ж�
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
        else if (AI.StringCareer== "Guard" || AI.StringCareer== "Rascally" || AI.StringCareer== "Arrogance")  //��ʿ��ְҵ�����ж�
        {
            AI.Career = 1;
        }
        else if (AI.StringCareer == "Turtle")  //�ڹ�ְҵ�����ж�
        {
            AI.Career = Energy;
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

        countDownTimer = 10f;
    }
}