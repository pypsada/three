using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Menu : MonoBehaviour
{
    /*此代码挂在在空物体，用于处理各个场景跳转的按键*/

    public void StartGame()  //开始游戏，进入选择场景界面
    {
        SceneManager.LoadScene("ActOption");
    }
    public void QuitGame()  //退出程序代码
    {
        Application.Quit();
    }

    /*下面是进入各个场景的跳转代码*/

    public void StartPVP()
    {
        SceneManager.LoadScene("PVP");
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
}
