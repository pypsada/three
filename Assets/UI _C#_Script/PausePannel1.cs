using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePannel1 : MonoBehaviour
{
    /*�˴�������ڿ����壬���ڴ�������Esc��������*/

    private bool isOpen = false;
    public GameObject pause;
    private void Update()  //��Bool����ѭ������
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
    public void ReloadScene()  //�����Ҫ�ؿ��ؿ���ʱ��ʹ��
    {
        SoundsManager.PlayClick();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
