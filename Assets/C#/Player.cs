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
    public int ArroganceNumber;
    public bool Thiefing;  //��͵�ж�

    public Text countDownText; // ����ʱ�ı�
    public float countDownTimer = 5f; // ����ʱʱ��
    public Text GroundText; //�غ����ı�

    void Start()
    {
        Ground = 1;
        RascallyNumber = 1;
        ArroganceNumber = 0;
        StringCareer = Whole.PlayerCareer;
        Career = 0;
        Energy = 0;   //��ʼ��
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

            // ��ʣ��ʱ����ʾ��UI�����ϵĵ���ʱ�ı���
            countDownText.text = Mathf.RoundToInt(countDownTimer).ToString();
        }
        else
        {
            AI.AIplaying();
            RubbingEnergy();
            Despare();
        }
    }

    public void RubbingEnergy()   //����
    {
        Energy += 1;
        Debug.Log("You:RubbingEnergy");
        Priority = 0;
    }

    public void Gun()  //ǹ
    {
        Priority = 1;
        Energy -= 1;
        Debug.Log("You:Gun");
    }

    public void Rebound()   //����
    {
        Priority = 100;
        Rebounding = true;
        Energy -= 2;
        Debug.Log("You:Rebound");
    }

    public void Defense()   //����
    {
        Priority = 1;
        Defensing = true;
        Debug.Log("You:Defense");
    }

    public void HolyGrail()   //����
    {
        Priority = 2;
        Energy -= 4;
        Debug.Log("You:HolyGrail");
    }

    public void Assassinate()  // �̿ͼ��ܣ���ɱ
    {
        Priority = 1;
        Career -= 1;
        Debug.Log("You:Assassinate");
    }

    public void Steal()   // �������ܣ�͵ȡ
    {
        Priority = 1;
    }

    public void King() // �������ܣ���Ȩ
    {
        Priority = 2;
        Energy -= 2;
        Debug.Log("You:King");
    }

    public void Guard()  // �������ܣ��ܷ�
    {
        Priority = 1;
        Defensing = true;
        Energy += 1;
        Debug.Log("You:Guard");
    }

    public void Turtle()  //�ڹ꼼�ܣ�����
    {
        Energy -= 1;
        Priority = 100;
        Rebounding = true;
        Debug.Log("You:Turtle");
    }

    public void Rascally()  //�������ܣ�����
    {
        Priority = 0;
        Energy += RascallyNumber;
        RascallyNumber += 1;
    }

    public void Arrogance()  //�������ܣ�����
    {
        ArroganceNumber += 1;
        Priority = 0;
    }

    public void Thief()  //�������ܣ���͵
    {
        Career -= 1;
        Thiefing = true;
    }

    public void Despare()   //�ж�
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
            else if (Defensing==true && AI.Priority!=2)  //��ҷ�����AI���ô��У�������Ϸ
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
            else if(Rebounding==true && (AI.Priority==0 || (AI.Priority!=0 && AI.Defensing==true)))  //��ҷ���ʧ��
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
            else if (Priority==0 && (AI.Defensing==true || AI.Priority==0 || AI.Rebounding==true))  //��Ҵ�������AI������
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
            else  //����������Ӯ
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
        if (StringCareer== "Assassin" || StringCareer=="Thief")   //�̿�,����ְҵ�����ж�
        {
            if (Ground%2==1)
            {
                Career += 1;
            }
        }
        else if (StringCareer=="King")  //����ְҵ�����ж�
        {
            Career = Energy / 2;
        }
        else if (StringCareer== "Guard" || StringCareer== "Rascally" || StringCareer== "Arrogance")  //��ʿְҵ�����ж�
        {
            Career = 1;
        }
        else if (StringCareer == "Turtle")  //�ڹ�ְҵ�����ж�
        {
            Career = Energy;
        }
    }
}
