using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Menu : MonoBehaviour
{
    /*此代码挂在在空物体，用于处理各个场景跳转的按键*/

    public void StartGame()  //开始游戏，进入选择场景界面
    {
        SoundsManager.PlayClick();
        if (SaveGameManager.SaveData == null)
        {
            SceneManager.LoadScene("Users");
        }
        else
        {
            if (SaveGameManager.SaveData.record == 0)
            {
                SceneManager.LoadScene("ActOption");
            }
            else if (SaveGameManager.SaveData.record >= 150 && SaveGameManager.SaveData.record < 160)
            {
                SaveGameManager.SaveData.record = 150;
                SceneManager.LoadScene("Act15");
            }
            else if (SaveGameManager.SaveData.record >= 240 && SaveGameManager.SaveData.record < 250)
            {
                SaveGameManager.SaveData.record = 240;
                SceneManager.LoadScene("Act24");
            }
            else if (SaveGameManager.SaveData.record >= 250)
            {
                SceneManager.LoadScene("BaseFinal");
            }
            else if (SaveGameManager.SaveData.record >= 60)
            {
                SceneManager.LoadScene("Base");
            }
            else if (SaveGameManager.SaveData.record < 20)
            {
                SaveGameManager.SaveData.record = 10;
                SceneManager.LoadScene("Act1");
            }
            else if (SaveGameManager.SaveData.record < 30)
            {
                SaveGameManager.SaveData.record = 20;
                SceneManager.LoadScene("Act2");
            }
            else if (SaveGameManager.SaveData.record < 40)
            {
                SaveGameManager.SaveData.record = 30;
                SceneManager.LoadScene("Act3");
            }
            else if (SaveGameManager.SaveData.record < 50)
            {
                SaveGameManager.SaveData.record = 40;
                SceneManager.LoadScene("Act4");
            }
            else if (SaveGameManager.SaveData.record < 60)
            {
                SaveGameManager.SaveData.record = 50;
                SceneManager.LoadScene("Act5");
            }
        }
    }
    public void QuitGame()  //退出程序代码
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("MainMenu");
    }

    /*下面是进入各个重UI场景的跳转代码*/

    public void Toturial()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("Toturial");
    }
    public void Illustration()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("Illustration");
    }
    public void GameAbout()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("GameAbout");
    }
    public void GameCulture()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("GameCulture");
    }
    public void UsersScene()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("Users");
    }
    public void GoToFight()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("FightPrepare");
    }
    public void GoToBase()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("Base");
    }
    public void PVP()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("Login");
    }
    public void Memory()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("Memory");
    }
    public void AI()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("ChoseCareer");
    }
    public void Base2()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("BaseFinal");
    }
}
