using NetGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    /*�Ի��������Ĵ���*/

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
                Name.text = theName;  //�������ֿ������
            }
            if (currentSignIndex < theContent.Length)  //������һ���ı�
            {
                Content.text = "";
                StartCoroutine(TypeText());
                currentSignIndex++;
            }
        }
    }

    public void NextSentence()
    {
        if (isTextComplete)  //��һ��
        {
            letterPause = 0.1f;
            isTextComplete = false;
            if (currentSignIndex < theContent.Length)  //ȷ������δ��ʾ�ĶԻ��ı�������ַ�����ʼ����
            {
                Content.text = "";
                StartCoroutine(TypeText());
                currentSignIndex++;
            }
            else  //���жԻ��ı����Ѿ���ʾ�����´ε�������Ի�ѡ��
            {
                AnswerChoices.SetActive(true);
                currentSignIndex = 0;
            }
        }
        else  //������ʾ
        {
            letterPause = 0.0f;
        }
    }

    IEnumerator TypeText()  //��������
    {
        foreach (char letter in theContent[currentSignIndex].ToCharArray())
        {
            Content.text += letter;  //��ӵ�ǰ�ַ����ı���
            yield return new WaitForSeconds(letterPause);
        }
        isTextComplete = true;  //�����ַ���������
    }
}
