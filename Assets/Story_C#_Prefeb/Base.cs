using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    /*����ҳ�����SaveGameManager����*/

    public Text level;
    public Text game1;
    public Text game2;
    public Text game3;

    void Start()
    {
        SaveData saveData = SaveGameManager.SaveData;
        if (saveData != null)
        {
            level.text = "��͵ȼ���<color=yellow>" + saveData.level.ToString() + "</color>";
            game1.text = "Ǳ���ؾ���¼��<color=yellow>" + saveData.stealth.ToString() + "</color>";
            game2.text = "���ؾ���¼��<color=yellow>" + saveData.agility.ToString() + "</color>";
            game3.text = "Ӧ���ؾ���¼��<color=yellow>" + saveData.mathematics.ToString() + "</color>";
        }
    }
}
