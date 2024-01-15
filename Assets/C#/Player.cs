using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int Energy;
    private Animator myAnim;
    public int Priority=0;
    public bool Rebounding=false;
    public bool Defensing=false;
    public AI AI;
    public GameObject AIgameobject;
    public bool Chose=false;

    public Text countDownText; // ����ʱ�ı�
    public float countDownTimer = 5f; // ����ʱʱ��

    void Start()
    {
        Energy = 0;
        myAnim = GetComponent<Animator>();
        countDownText = countDownText.GetComponent<Text>();
        AI = FindObjectOfType<AI>();
    }

    void Update()
    {
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

    public void RubbingEnergy()
    { 
        Energy += 1;
        Debug.Log("You:RubbingEnergy");
        Priority = 0;
    } 

    public void Gun()
    {
        Priority = 1;
        Energy -= 1;
        Debug.Log("You:Gun");
    }

    public void Rebound()
    {
        Priority = 100;
        Rebounding=true;
        Energy -= 2;
        Debug.Log("You:Rebound");
    }

    public void Defense()
    {
        Priority = 1;
        Defensing = true;
        Debug.Log("You:Defense");
    }

    public void HolyGrail()
    {
        Priority = 2;
        Energy -= 2;
        Debug.Log("You:HolyGrail");
    }

    public void Despare()
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
}
