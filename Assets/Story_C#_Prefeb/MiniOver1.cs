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
            scoreText.text = "����Ǳ���÷֣�<color=blue>" + a.ToString() + "</color>\n�÷��¼�¼��";
            SaveGameManager.SaveData.stealth = a;
            PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
            PlayerPrefs.Save();
        }
        else
        {
            scoreText.text = "����Ǳ���÷֣�<color=blue>" + a.ToString() + "</color>";
        }
        pannel.SetActive(true);
    }
}
