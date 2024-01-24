using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NetButtonDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text buttonText; // 按钮文本组件
    public Color grayColor; // 灰色颜色
    public LocalPlayer player; // Player脚本的引用
    public int Energy;
    public bool Click = false;
    public bool Career;

    void Start()
    {
        buttonText = GetComponentInChildren<Text>(); // 获取按钮文本组件
        player = FindObjectOfType<LocalPlayer>(); // 获取Player脚本的引用
    }

    void Update()
    {
        //if (Career)
        //{
        //    if (player.Career==0)
        //    {
        //        Click = false;
        //        buttonText.color = grayColor;  // 设置按钮颜色为灰色
        //    }
        //    else
        //    {
        //        Click = true;// 启用按钮交互性
        //        if (buttonText.color == grayColor)
        //        {
        //            buttonText.color = Color.white; // 设置按钮颜色为白色
        //        }
        //    }
        //}
        //else
        //{
            if (player.Energy < Energy) // 根据Energy值判断按钮颜色和可点击状态
            {
                Click = false;
                buttonText.color = grayColor;  // 设置按钮颜色为灰色
            }
            else
            {
                Click = true;// 启用按钮交互性
                if (buttonText.color==grayColor)
                {
                    buttonText.color = Color.white; // 设置按钮颜色为白色
                }
            }
        //}

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


//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;

//public class ButtonTextEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
//{
//    private Text buttonText; // 按钮文本组件
//    private Color originalColor; // 原始文本颜色
//    private Color highlightColor = Color.yellow; // 高亮颜色
//    public Player player; // Player脚本的引用
//    public int Energy;
//    public bool Click;

//    private void Start()
//    {
//        buttonText = GetComponentInChildren<Text>(); // 获取按钮文本组件
//        originalColor = buttonText.color; // 记录原始文本颜色
//        player = FindObjectOfType<Player>(); // 获取Player脚本的引用
//    }
//    void Update()
//    {
//        if (player.Energy < Energy) // 根据Energy值判断按钮颜色和可点击状态
//        {
//            Click = false;
//            buttonText.color = Color.gray;  // 设置按钮颜色为灰色
//        }
//        else
//        {
//            Click = true;// 启用按钮交互性
//            buttonText.color = Color.white; // 设置按钮颜色为白色
//        }
//    }
//    public void OnPointerEnter(PointerEventData eventData)
//    {
//        buttonText.color = highlightColor; // 鼠标进入时，设置文本高亮颜色
//    }

//    public void OnPointerExit(PointerEventData eventData)
//    {
//        buttonText.color = originalColor; // 鼠标离开时，恢复文本原始颜色
//    }
//}
