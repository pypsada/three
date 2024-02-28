using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    /*此代码用于显示出鼠标悬停按键框*/

    private bool isOver = false;  //检测鼠标是否悬停，配合EventTrigger使用
    private Color originalColor;
    private Image image;

    public void OnMouseEnter()
    {
        isOver = true;  //鼠标进入，检测为true
        UpdateUI();
    }

    public void OnMouseExit()
    {
        isOver = false;  //鼠标退出，检测为false
        UpdateUI();
    }

    private void Start()
    {
        image = GetComponent<Image>();  //获取图像信息
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
            image.color = originalColor;  //恢复原始颜色
        }
    }
}
