using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustrationPannel : MonoBehaviour
{
    /*�˴������ڵ��ð�����д�õ��ַ�������ѡ���Ŀ�*/

    public Text Text;
    public string text;

    public void ShowText()
    {
        Text.GetComponent<Text>().text = text;
        Text.text = Text.text.Replace("\\n", "\n");
    }
}
