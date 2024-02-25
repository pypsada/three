using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Menu : MonoBehaviour
{
    /*�˴�������ڿ����壬���ڴ������������ת�İ���*/

    public void StartGame()  //��ʼ��Ϸ������ѡ�񳡾�����
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
    public void QuitGame()  //�˳��������
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /*�����ǽ��������������ת����*/

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
