using NetGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    /*对话交互核心代码*/

    public GameObject DialogPrefab;
    public Text Name;
    public Text Content;

    public GameObject AnswerChoices;
    public string theName;
    public string[] theContent;

    public float letterPause = 0.1f;
    private bool isTextComplete = false;

    private int currentSignIndex = 0;

    void Start()
    {

    }


    void Update()
    {

    }

    public void OnClick()
    {
        if (DialogPrefab != null)
        {
            DialogPrefab.SetActive(true);
            if (Name != null)
            {
                Name.text = theName;  //更改名字框的内容
            }
            if (currentSignIndex < theContent.Length)  //调出第一句文本
            {
                Content.text = "";
                StartCoroutine(TypeText());
                currentSignIndex++;
            }
        }
    }

    public void NextSentence()
    {
        if (isTextComplete)  //下一句
        {
            letterPause = 0.1f;
            isTextComplete = false;
            if (currentSignIndex < theContent.Length)  //确保还有未显示的对话文本，清除字符，开始跳字
            {
                Content.text = "";
                StartCoroutine(TypeText());
                currentSignIndex++;
            }
            else  //所有对话文本都已经显示过，下次点击跳出对话选项
            {
                AnswerChoices.SetActive(true);
                currentSignIndex = 0;
            }
        }
        else  //快速显示
        {
            letterPause = 0.0f;
        }
    }

    IEnumerator TypeText()  //逐字跳字
    {
        foreach (char letter in theContent[currentSignIndex].ToCharArray())
        {
            Content.text += letter;  //添加当前字符到文本框
            yield return new WaitForSeconds(letterPause);
        }
        isTextComplete = true;  //所有字符都输出完毕
    }
}
