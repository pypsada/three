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
            UsersScene();
        }
        else
        {
            if (SaveGameManager.SaveData.record == 0)
            {
                SceneManager.LoadScene("ActOption");
            }
            else if (SaveGameManager.SaveData.record > 0)
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
