using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetBase : MonoBehaviour
{
    public int Energy;
    public int Priority = 0;
    public bool Rebounding = false;
    public bool Defensing = false;

    public Animator myAnim;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Energy = 0;
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        return;
    }

    public virtual void RubbingEnergy()
    {
        Energy += 1;
        //Debug.Log("You:RubbingEnergy");
        Priority = 0;
    }

    public virtual void Gun()
    {
        Priority = 1;
        Energy -= 1;
        //Debug.Log("You:Gun");
    }

    public virtual void Rebound()
    {
        Priority = 100;
        Rebounding = true;
        Energy -= 2;
        //Debug.Log("You:Rebound");
    }

    public virtual void Defense()
    {
        Priority = 1;
        Defensing = true;
        //Debug.Log("You:Defense");
    }

    public virtual void HolyGrail()
    {
        Priority = 2;
        Energy -= 2;
        //Debug.Log("You:HolyGrail");
    }

    //public void Despare()
    //{
    //    if (Priority < AI.Priority && AI.Rebounding == false && AI.Defensing == false)  //AI�������ȼ�����
    //    {
    //        Destroy(gameObject);
    //        Debug.Log("LOSE");
    //    }
    //    else if (Priority < AI.Priority && AI.Rebounding == true && Defensing == false && Priority != 0) //AI�����ɹ�
    //    {
    //        Destroy(gameObject);
    //        Debug.Log("LOSE");
    //    }
    //    else if (Priority == AI.Priority)  //���ȼ�һ�����໥����
    //    {
    //        Chose = false;
    //        Priority = 0;
    //        Defensing = false;
    //        Rebounding = false;
    //        AI.Rebounding = false;
    //        countDownTimer = 5f;
    //        AI.Priority = 0;
    //        AI.Defensing = false;
    //        Debug.Log("Continue");
    //    }
    //    else if (Defensing == true && AI.Priority != 2)  //��ҷ�����AI���ô��У�������Ϸ
    //    {
    //        Chose = false;
    //        Priority = 0;
    //        Defensing = false;
    //        Rebounding = false;
    //        AI.Rebounding = false;
    //        countDownTimer = 5f;
    //        AI.Priority = 0;
    //        AI.Defensing = false;
    //        Debug.Log("Continue");
    //    }
    //    else if (Rebounding == true && (AI.Priority == 0 || (AI.Priority != 0 && AI.Defensing == true)))  //��ҷ���ʧ��
    //    {
    //        Chose = false;
    //        Priority = 0;
    //        Defensing = false;
    //        Rebounding = false;
    //        AI.Rebounding = false;
    //        countDownTimer = 5f;
    //        AI.Priority = 0;
    //        AI.Defensing = false;
    //        Debug.Log("Continue");
    //    }
    //    else if (Priority == 0 && (AI.Defensing == true || AI.Priority == 0 || AI.Rebounding == true))  //��Ҵ�������AI������
    //    {
    //        Chose = false;
    //        Priority = 0;
    //        Defensing = false;
    //        Rebounding = false;
    //        AI.Rebounding = false;
    //        countDownTimer = 5f;
    //        AI.Priority = 0;
    //        AI.Defensing = false;
    //        Debug.Log("Continue");
    //    }
    //    else  //����������Ӯ
    //    {
    //        Destroy(AIgameobject);
    //        Debug.Log("WIN");
    //    }
    //}
}
