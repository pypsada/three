using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    /*此代码用于显示出鼠标悬停按键框*/

    private bool isOver = false;  //检测鼠标是否悬停，配合EventTrigger使用

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

    private void UpdateUI()
    {
        Image image = GetComponent<Image>();  //先获取图像信息

        if (isOver)
        {
            image.color = new Color(1f, 1f, 1f, 0.5f); //鼠标进入，设置显示颜色（为灰色）
        }
        else
        {
            image.color = Color.white; //鼠标退出，设置透明颜色（优化一下，用预设的）
        }
    }
}
