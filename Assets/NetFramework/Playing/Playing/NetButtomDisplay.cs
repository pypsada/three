using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NetGame
{
    public class ButtonDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Text buttonText; // 按钮文本组件
        public Color grayColor; // 灰色颜色
        [Header("ref")]
        public Player player; // Player脚本的引用
        [Header("set")]
        public int Energy;
        private bool Click = false;
        public bool Career;

        void Start()
        {
            buttonText = GetComponentInChildren<Text>(); // 获取按钮文本组件
            //player = FindObjectOfType<Player>(); // 获取Player脚本的引用
            return;
        }

        void Update()
        {
            if (Career)
            {
                if (player.tmpData.Career == 0)
                {
                    Click = false;
                    buttonText.color = grayColor;  // 设置按钮颜色为灰色
                }
                else
                {
                    Click = true;// 启用按钮交互性
                    if (buttonText.color == grayColor)
                    {
                        buttonText.color = Color.white; // 设置按钮颜色为白色
                    }
                }
            }
            else
            {
                if (player.tmpData.Energy < Energy) // 根据Energy值判断按钮颜色和可点击状态
                {
                    Click = false;
                    buttonText.color = grayColor;  // 设置按钮颜色为灰色
                }
                else
                {
                    Click = true;// 启用按钮交互性
                    if (buttonText.color == grayColor)
                    {
                        buttonText.color = Color.white; // 设置按钮颜色为白色
                    }
                }
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Click)
            {
                buttonText.color = Color.yellow;// 鼠标悬停时设置按钮颜色为黄色
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Click)
            {
                buttonText.color = Color.white; // 鼠标离开时设置按钮颜色为白色
            }
        }
    }

}
