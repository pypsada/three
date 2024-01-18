using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePannel : MonoBehaviour
{
    /*此代码挂在在空物体，用于处理所有Esc弹出窗口*/

    private bool isOpen = false;
    public GameObject pause;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //这一帧按下Esc的时候
        {
            if (!isOpen)  //“!” 是取反
            {
                //SoundsManager.PlayPauseClip();
                Pause();
            }
            else if (isOpen)
            {
                GameContinue();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;  //「言灵·时间零」
        pause.SetActive(true);
        isOpen = true;
    }
    public void GameContinue()
    {
        Time.timeScale = 1;  //时间流速返回
        pause.SetActive(false);
        isOpen = false;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void ReloadScene()  //如果需要重开关卡的时候使用
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
