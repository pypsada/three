using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ButtonNet : MonoBehaviour
{
    public Main main;
    public LocalPlayer Player;
    internal object image;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<LocalPlayer>(); // 获取Player脚本的引用
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RubbingEnergy()
    {
        if (!main.chose) return;
        Player.RubbingEnergy();
        main.Despare();
    }

    public void Gun()
    {
        if (!main.chose) return;
        if (Player.Energy >= 1)
        {
            Player.Gun();
            main.Despare();
        }
    }

    public void Rebound()
    {
        if (!main.chose) return;
        if (Player.Energy>=2)
        {
            Player.Rebound();
            main.Despare();
        }

    }

    public void Defense()
    {
        if (!main.chose) return;
        Player.Defense();
        main.Despare();
    }

    public void HolyGrail()
    {
        if (!main.chose) return;
        if (Player.Energy>=4)
        {
            Player.HolyGrail();
           
            main.Despare();
        }
    }
}
