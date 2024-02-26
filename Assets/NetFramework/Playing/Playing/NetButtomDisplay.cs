using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NetGame
{
    public class ButtonDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Text buttonText; // ��ť�ı����
        public Color grayColor; // ��ɫ��ɫ
        [Header("ref")]
        public Player player; // Player�ű�������
        [Header("set")]
        public int Energy;
        private bool Click = false;
        public bool Career;

        void Start()
        {
            buttonText = GetComponentInChildren<Text>(); // ��ȡ��ť�ı����
            //player = FindObjectOfType<Player>(); // ��ȡPlayer�ű�������
            return;
        }

        void Update()
        {
            if (Career)
            {
                if (player.tmpData.Career == 0)
                {
                    Click = false;
                    buttonText.color = grayColor;  // ���ð�ť��ɫΪ��ɫ
                }
                else
                {
                    Click = true;// ���ð�ť������
                    if (buttonText.color == grayColor)
                    {
                        buttonText.color = Color.white; // ���ð�ť��ɫΪ��ɫ
                    }
                }
            }
            else
            {
                if (player.tmpData.Energy < Energy) // ����Energyֵ�жϰ�ť��ɫ�Ϳɵ��״̬
                {
                    Click = false;
                    buttonText.color = grayColor;  // ���ð�ť��ɫΪ��ɫ
                }
                else
                {
                    Click = true;// ���ð�ť������
                    if (buttonText.color == grayColor)
                    {
                        buttonText.color = Color.white; // ���ð�ť��ɫΪ��ɫ
                    }
                }
            }

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

}
