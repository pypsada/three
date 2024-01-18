using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Menu : MonoBehaviour
{
    /*�˴�������ڿ����壬���ڴ������������ת�İ���*/

    public void StartGame()  //��ʼ��Ϸ������ѡ�񳡾�����
    {
        SceneManager.LoadScene("ActOption");
    }
    public void QuitGame()  //�˳��������
    {
        Application.Quit();
    }

    /*�����ǽ��������������ת����*/

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
