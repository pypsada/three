using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePannel1 : MonoBehaviour
{
    /*此代码挂在在空物体，用于处理所有Esc弹出窗口*/

    private bool isOpen = false;
    public GameObject pause;
    private void Update()  //用Bool变量循环开关
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpen)
            {
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
        SoundsManager.PlayPauseClip();
        pause.SetActive(true);
        isOpen = true;
    }
    public void GameContinue()
    {
        SoundsManager.PlayClick();
        pause.SetActive(false);
        isOpen = false;
    }
    public void BackToMenu()
    {
        SoundsManager.PlayClick();
        SceneManager.LoadScene("MainMenu");
    }
    public void ReloadScene()  //如果需要重开关卡的时候使用
    {
        SoundsManager.PlayClick();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
