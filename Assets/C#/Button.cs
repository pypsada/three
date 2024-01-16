using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class Button : MonoBehaviour
{
    public Player Player;
    public AI ai;
    internal object image;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Player>(); // 获取Player脚本的引用
        ai = FindObjectOfType<AI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RubbingEnergy()
    {
        ai.AIplaying();
        Player.RubbingEnergy();
        Player.Despare();
    }

    public void Gun()
    {
        if (Player.Energy>=1)
        {
            ai.AIplaying();
            Player.Gun();
            Player.Despare();
        }

    }

    public void Rebound()
    {
        if (Player.Energy>=2)
        {
            ai.AIplaying();
            Player.Rebound();
            Player.Despare();
        }

    }

    public void Defense()
    {
        ai.AIplaying();
        Player.Defense();
        Player.Despare();
    }

    public void HolyGrail()
    {
        if (Player.Energy>=4)
        {
            ai.AIplaying();
            Player.HolyGrail();
           
            Player.Despare();
        }
    }

    public void ChoseAssassin()
    {
        Whole.PlayerCareer = "Assassin";
        SceneManager.LoadScene("PK&AI");
    }

    public void VocationalSkills()
    {
        if (Player.StringCareer== "Assassin")
        {
            ai.AIplaying();
            Player.Assassinate();
            Player.Despare();
        }
    }

}
