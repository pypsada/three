using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGames : MonoBehaviour
{
    public GameObject game1_first;
    public GameObject game1;
    public GameObject game2_first;
    public GameObject game2;
    public GameObject game3_first;
    public GameObject game3;
    public void ToGame1()
    {
        if (SaveGameManager.SaveData.stealth == 0)
        {
            game1_first.GetComponent<Dialog>().StartDialog();
        }
        else
        {
            game1.GetComponent<Dialog>().StartDialog();
        }
    }
    public void ToGame2()
    {
        if (SaveGameManager.SaveData.agility == 0)
        {
            game2_first.GetComponent<Dialog>().StartDialog();
        }
        else
        {
            game2.GetComponent<Dialog>().StartDialog();
        }
    }
    public void ToGame3()
    {
        if (SaveGameManager.SaveData.mathematics == 0) 
        {
            game3_first.GetComponent<Dialog>().StartDialog();
        }
        else
        {
            game3.GetComponent<Dialog>().StartDialog();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
