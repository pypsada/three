using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmallGames : MonoBehaviour
{
    public GameObject game1_first;
    public GameObject game1;
    public GameObject game2_first;
    public GameObject game2;
    public GameObject game3_first;
    public GameObject game3;

    private SaveData saveData;

    private void Start()
    {
        saveData = SaveGameManager.SaveData;
    }
    public void ToGame1()
    {
        if (saveData.stealth == 0)
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
        if (saveData.agility == 0)
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
        if (saveData.mathematics == 0) 
        {
            game3_first.GetComponent<Dialog>().StartDialog();
        }
        else
        {
            game3.GetComponent<Dialog>().StartDialog();
        }
    }
    public void SmallGame1()
    {
        if (saveData.stealth == 0)
        {
            SceneManager.LoadScene("Game1Teach");
        }
        else
        {
            SceneManager.LoadScene("Game1");
        }
    }
    public void SmallGame2()
    {
        if (saveData.agility == 0)
        {
            SceneManager.LoadScene("Game2Teach");
        }
        else
        {
            SceneManager.LoadScene("Game2");
        }
    }
    public void SmallGame3()
    {
        if (saveData.mathematics == 0)
        {
            SceneManager.LoadScene("Game3Teach");
        }
        else
        {
            SceneManager.LoadScene("Game3");
        }
    }
}
