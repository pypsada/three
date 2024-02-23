using NetGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    /*�Ի����������ڶԻ������ϣ���д�ض��ı�*/

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
        dialogManager.theContent = myContent;  //�����ı�
        if (myName != "")
            dialogManager.theName = myName;  //��������
        else
            dialogManager.theName = SaveGameManager.Nickname;
        dialogManager.AnswerChoices = myAnswer;  //����ѡ��

        dialogManager.OnClick();  //��ʼ����

        Pictures = GameObject.Find("Pictures");  //����ͼƬ
        pictures = Pictures.GetComponent<Image>();
        if (picPath != "")
        {
            pictures.color = Color.white;
            pictures.sprite = Resources.Load<Sprite>(picPath);
        }
        else
            pictures.color = Color.clear;

        if (needDestroy != null)  //�����һЩ����
            Destroy(needDestroy);
    }
}
