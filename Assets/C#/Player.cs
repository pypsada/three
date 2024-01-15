using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Energy;
    private Animator myAnim;
    public int Priority=0;
    public bool Rebounding=false;
    public bool Defensing=false;
    public AI AI;
    public GameObject AIgameobject;
    
    void Start()
    {
        Energy = 0;
        myAnim = GetComponent<Animator>();

    }

    void Update()
    {
        
    }

    public void RubbingEnergy()
    { 
        Energy += 1;
        Priority = 0;
    } 

    public void Gun()
    {
        Priority = 1;
        Energy -= 1;
    }

    public void Rebound()
    {
        Priority = 100;
        Rebounding=true;
        Energy -= 2;
    }

    public void Defense()
    {
        Priority = 1;
        Defensing = true;
    }

    public void HolyGrail()
    {
        Priority = 2;
        Energy -= 2;
    }

    public void Despare()
    {
        if (Priority < AI.Priority && AI.Rebounding==false)  //AI攻击优先级更高
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
            Priority = 0;
            Defensing = false;
            Rebounding= false;
            AI.Rebounding = false;
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

}
