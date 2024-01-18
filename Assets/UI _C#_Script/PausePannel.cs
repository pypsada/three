using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePannel : MonoBehaviour
{
    /*�˴�������ڿ����壬���ڴ�������Esc��������*/

    private bool isOpen = false;
    public GameObject pause;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //��һ֡����Esc��ʱ��
        {
            if (!isOpen)  //��!�� ��ȡ��
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
        Time.timeScale = 0;  //�����顤ʱ���㡹
        pause.SetActive(true);
        isOpen = true;
    }
    public void GameContinue()
    {
        Time.timeScale = 1;  //ʱ�����ٷ���
        pause.SetActive(false);
        isOpen = false;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void ReloadScene()  //�����Ҫ�ؿ��ؿ���ʱ��ʹ��
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
