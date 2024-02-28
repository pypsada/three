using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    /*�˴���������ʾ�������ͣ������*/

    private bool isOver = false;  //�������Ƿ���ͣ�����EventTriggerʹ��
    private Color originalColor;
    private Image image;

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

    private void Start()
    {
        image = GetComponent<Image>();  //��ȡͼ����Ϣ
        originalColor = image.color;
    }

    private void UpdateUI()
    {
        if (isOver)
        {
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
        }
        else
        {
            image.color = originalColor;  //�ָ�ԭʼ��ɫ
        }
    }
}
