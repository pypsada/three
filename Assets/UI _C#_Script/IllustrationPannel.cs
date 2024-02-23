using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustrationPannel : MonoBehaviour
{
    /*此代码用于调用按键上写好的字符串填入选定的框*/

    public Text Text;
    public string text;

    public void ShowText()
    {
        Text.GetComponent<Text>().text = text;
        Text.text = Text.text.Replace("\\n", "\n");
    }
}
