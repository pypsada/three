using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayInvoke : MonoBehaviour
{
    /*延时弹出对话，外加延迟弹出的克隆物体*/

    public GameObject dialog;
    public float delayTime;  //延时时间

    public GameObject[] cloneThing;
    public float[] cloneTime;  //克隆时间

    public GameObject canvas;

    public GameObject[] destroyThing;

    void Start()
    {
        if (dialog != null)
        {
            StartCoroutine(InvokeDialog());
        }
        if (canvas != null && cloneThing != null && cloneTime != null && cloneThing.Length == cloneTime.Length)
        {
            StartCoroutine(CloneWithDelay());
        }
        if (destroyThing != null)
        {
            for (int i = 0; i < destroyThing.Length; i++)
            {
                Destroy(destroyThing[i]);
            }
        }
    }

    IEnumerator InvokeDialog()
    {
        yield return new WaitForSeconds(delayTime);
        dialog.GetComponent<Dialog>().StartDialog();
    }

    IEnumerator CloneWithDelay()  //依次克隆
    {
        for (int i = 0; i < cloneThing.Length; i++)
        {
            yield return new WaitForSeconds(cloneTime[i]);
            Instantiate(cloneThing[i], canvas.transform);
        }
    }
}
