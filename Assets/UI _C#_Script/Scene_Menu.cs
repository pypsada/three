using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Menu : MonoBehaviour
{
    /*此代码挂在在空物体，用于处理各个场景跳转的按键*/

    public void StartGame()  //开始游戏，进入选择场景界面
    {
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
            else if (SaveGameManager.SaveData.record >= 60)
            {
                SceneManager.LoadScene("Base");
            }
        }
    }
    public void QuitGame()  //退出程序代码
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /*下面是进入各个场景的跳转代码*/

    public void StartPVP()
    {
        SceneManager.LoadScene("Net");
    }
    public void Toturial()
    {
        SceneManager.LoadScene("Toturial");
    }
    public void Illustration()
    {
        SceneManager.LoadScene("Illustration");
    }
    public void GameAbout()
    {
        SceneManager.LoadScene("GameAbout");
    }
    public void GameCulture()
    {
        SceneManager.LoadScene("GameCulture");
    }
    public void UsersScene()
    {
        SceneManager.LoadScene("Users");
    }
    public void GoToBase()
    {
        SceneManager.LoadScene("Base");
    }
}
