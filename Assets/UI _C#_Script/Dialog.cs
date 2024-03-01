using NetGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    /*对话交互，绑定在对话物体上，编写特定文本*/

    public GameObject DialogManager;
    private DialogManager dialogManager;
    public GameObject Pictures;
    public Image pictures;

    public string myName;
    public string[] myContent;
    public GameObject myAnswer;

    public string picPath;

    public GameObject needDestroy;

    void Start()
    {
        DialogManager = GameObject.Find("DialogManager");
        dialogManager = DialogManager.GetComponent<DialogManager>();
    }

    public void StartDialog()
    {
        dialogManager.theContent = myContent;  //调入文本
        if (myName != "")
            dialogManager.theName = myName;  //调入名字
        else
            dialogManager.theName = SaveGameManager.Nickname;
        dialogManager.AnswerChoices = myAnswer;  //调入选项

        dialogManager.OnClick();  //开始播放

        Pictures = GameObject.Find("Pictures");  //调入图片
        pictures = Pictures.GetComponent<Image>();
        if (picPath == "null")
            pictures.color = Color.clear;
        else if (picPath == "ZJ")
        {
            if (SaveGameManager.SaveData.avatarPath == "MrWu")
            {
                pictures.color = Color.white;
                pictures.sprite = Resources.Load<Sprite>("NanZhu1");
            }
            else
            {
                pictures.color = Color.white;
                pictures.sprite = Resources.Load<Sprite>("NvZhu1");
            }
        }
        else if (picPath != "")
        {
            pictures.color = Color.white;
            pictures.sprite = Resources.Load<Sprite>(picPath);
        }
        else
        {
            if (SaveGameManager.SaveData.avatarPath == "MrWu")
            {
                pictures.color = Color.white;
                pictures.sprite = Resources.Load<Sprite>("NanZhu");
            }
            else
            {
                pictures.color = Color.white;
                pictures.sprite = Resources.Load<Sprite>("NvZhu");
            }
        }

        if (needDestroy != null)  //可清除一些东西
            Destroy(needDestroy);
    }
}
