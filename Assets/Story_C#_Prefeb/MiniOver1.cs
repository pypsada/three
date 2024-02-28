using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniOver1 : MonoBehaviour
{
    public GameObject pannel;
    public Text scoreText;
    public GameObject numberDisplay;
    private int a;

    private void OnDisable()
    {
        a = numberDisplay.GetComponent<NumberDisplayGame1>().Number;
        if (a > SaveGameManager.SaveData.stealth)
        {
            scoreText.text = "本次潜伏得分：<color=blue>" + a.ToString() + "</color>\n得分新记录！";
            SaveGameManager.SaveData.stealth = a;
            PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
            PlayerPrefs.Save();
        }
        else
        {
            scoreText.text = "本次潜伏得分：<color=blue>" + a.ToString() + "</color>";
        }
        pannel.SetActive(true);
    }
}
