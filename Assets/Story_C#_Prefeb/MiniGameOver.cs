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
            scoreText.text = "����Ӧ��÷֣�<color=blue>" + a.ToString() + "</color>\n�÷��¼�¼��";
            SaveGameManager.SaveData.mathematics = a;
            PlayerPrefs.SetString(SaveGameManager.Nickname, JsonUtility.ToJson(SaveGameManager.SaveData));
            PlayerPrefs.Save();
        }
        else
        {
            scoreText.text = "����Ӧ��÷֣�<color=blue>" + a.ToString() + "</color>";
        }
        pannel.SetActive(true);
    }
}
