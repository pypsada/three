using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Button : MonoBehaviour
{
    public Player Player;
    internal object image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RubbingEnergy()
    {
        Player.RubbingEnergy();
        Player.Despare();
    }

    public void Gun()
    {
        if (Player.Energy>=1)
        {
            Player.Gun();
            Player.Despare();
        }

    }

    public void Rebound()
    {
        if (Player.Energy>=2)
        {
            Player.Rebound();
            Player.Despare();
        }

    }

    public void Defense()
    {
        Player.Defense();
        Player.Despare();
    }

    public void HolyGrail()
    {
        if (Player.Energy>=4)
        {
            Player.HolyGrail();
            Player.Despare();
        }
    }
}
