using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    /*�˴���������ʾ�������ͣ������*/

    private bool isOver = false;  //�������Ƿ���ͣ�����EventTriggerʹ��

    public void OnMouseEnter()
    {
        isOver = true;  //�����룬���Ϊtrue
        UpdateUI();
    }

    public void OnMouseExit()
    {
        isOver = false;  //����˳������Ϊfalse
        UpdateUI();
    }

    private void UpdateUI()
    {
        Image image = GetComponent<Image>();  //�Ȼ�ȡͼ����Ϣ

        if (isOver)
        {
            image.color = new Color(1f, 1f, 1f, 0.5f); //�����룬������ʾ��ɫ��Ϊ��ɫ��
        }
        else
        {
            image.color = Color.white; //����˳�������͸����ɫ���Ż�һ�£���Ԥ��ģ�
        }
    }
}
