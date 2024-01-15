using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public int Energy;
    private Animator myAnim;
    public int Priority = 0;
    public bool Rebounding = false;
    public bool Defensing = false;
    public Player player;
    public int chose;

    void Start()
    {
        Energy = 0;
        myAnim = GetComponent<Animator>();
        player = FindObjectOfType<Player>(); // 获取Player脚本的引用
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
        Rebounding = true;
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

    public void AIplaying()
    {
        if (Energy==0 && player.Energy==0)
        {
            RubbingEnergy();
            Debug.Log("AI:RubbingEnergy");
        }
        else if (Energy==0)
        {
            chose=Random.Range(0, 2);
            if (chose==0)
            {
                RubbingEnergy();
                Debug.Log("AI:RubbingEnergy");
            }
            else if (chose==1)
            {
                Defense();
                Debug.Log("AI:Defense");
            }
        }
        else if(Energy<2)
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
            else if (chose==2)
            {
                Gun();
                Debug.Log("AI:Gun");
            }
        }
        else if(Energy<4)
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
            else if(chose==3)
            {
                Rebound();
                Debug.Log("AI:Rebound");
            }
        }
        else
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
        }
    }
}
