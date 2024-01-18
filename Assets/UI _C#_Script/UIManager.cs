using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
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
            image.color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 110 / 255f); // �����룬������ʾ��ɫ��Ϊ��ɫ��
        }
        else
        {
            image.color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0 / 255f); // ����˳�������͸����ɫ
        }
    }
}
