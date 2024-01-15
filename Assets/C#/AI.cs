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
        
    }
}
