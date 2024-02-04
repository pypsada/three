using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichVisibile : MonoBehaviour
{
    /*此代码用于更改图鉴里的图片显示*/
    /*放在按钮上，让EventTrigger调用*/

    public GameObject parentObject;  //切换的物体的总体所在
    public GameObject childObject;  //要显示的物体

    public void Swich()
    {
        // 隐藏父物体下的所有子物体
        foreach (Transform child in parentObject.transform)
        {
            child.gameObject.SetActive(false);
        }

        // 显示指定的子物体
        if (childObject != false)
        {
            childObject.SetActive(true);
        }
    }
}
