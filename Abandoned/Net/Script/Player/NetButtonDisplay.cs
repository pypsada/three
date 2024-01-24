using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NetButtonDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text buttonText; // ��ť�ı����
    public Color grayColor; // ��ɫ��ɫ
    public LocalPlayer player; // Player�ű�������
    public int Energy;
    public bool Click = false;
    public bool Career;

    void Start()
    {
        buttonText = GetComponentInChildren<Text>(); // ��ȡ��ť�ı����
        player = FindObjectOfType<LocalPlayer>(); // ��ȡPlayer�ű�������
    }

    void Update()
    {
        //if (Career)
        //{
        //    if (player.Career==0)
        //    {
        //        Click = false;
        //        buttonText.color = grayColor;  // ���ð�ť��ɫΪ��ɫ
        //    }
        //    else
        //    {
        //        Click = true;// ���ð�ť������
        //        if (buttonText.color == grayColor)
        //        {
        //            buttonText.color = Color.white; // ���ð�ť��ɫΪ��ɫ
        //        }
        //    }
        //}
        //else
        //{
            if (player.Energy < Energy) // ����Energyֵ�жϰ�ť��ɫ�Ϳɵ��״̬
            {
                Click = false;
                buttonText.color = grayColor;  // ���ð�ť��ɫΪ��ɫ
            }
            else
            {
                Click = true;// ���ð�ť������
                if (buttonText.color==grayColor)
                {
                    buttonText.color = Color.white; // ���ð�ť��ɫΪ��ɫ
                }
            }
        //}

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Click)
        {
            buttonText.color = Color.yellow;// �����ͣʱ���ð�ť��ɫΪ��ɫ
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Click)
        {
            buttonText.color = Color.white; // ����뿪ʱ���ð�ť��ɫΪ��ɫ
        }
    }
}


//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;

//public class ButtonTextEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
//{
//    private Text buttonText; // ��ť�ı����
//    private Color originalColor; // ԭʼ�ı���ɫ
//    private Color highlightColor = Color.yellow; // ������ɫ
//    public Player player; // Player�ű�������
//    public int Energy;
//    public bool Click;

//    private void Start()
//    {
//        buttonText = GetComponentInChildren<Text>(); // ��ȡ��ť�ı����
//        originalColor = buttonText.color; // ��¼ԭʼ�ı���ɫ
//        player = FindObjectOfType<Player>(); // ��ȡPlayer�ű�������
//    }
//    void Update()
//    {
//        if (player.Energy < Energy) // ����Energyֵ�жϰ�ť��ɫ�Ϳɵ��״̬
//        {
//            Click = false;
//            buttonText.color = Color.gray;  // ���ð�ť��ɫΪ��ɫ
//        }
//        else
//        {
//            Click = true;// ���ð�ť������
//            buttonText.color = Color.white; // ���ð�ť��ɫΪ��ɫ
//        }
//    }
//    public void OnPointerEnter(PointerEventData eventData)
//    {
//        buttonText.color = highlightColor; // ������ʱ�������ı�������ɫ
//    }

//    public void OnPointerExit(PointerEventData eventData)
//    {
//        buttonText.color = originalColor; // ����뿪ʱ���ָ��ı�ԭʼ��ɫ
//    }
//}
