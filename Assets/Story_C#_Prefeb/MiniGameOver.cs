using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameOver : MonoBehaviour
{
    public GameObject pannel;
    public Text scoreText;
    public GameObject numberDisplay;
    private int a;

    private void OnDestroy()
    {
        a = numberDisplay.GetComponent<NumberDisplayGame1>().Number;
        if (a > SaveGameManager.SaveData.mathematics)
        {
            scoreText.text = "本次应变得分：<color=blue>" + a.ToString() + "</color>\n得分新记录！";
            SaveGameManager.SaveData.mathematics = a;
            PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
            PlayerPrefs.Save();
        }
        else
        {
            scoreText.text = "本次应变得分：<color=blue>" + a.ToString() + "</color>";
        }
        pannel.SetActive(true);
    }
}
