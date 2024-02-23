using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayInvoke : MonoBehaviour
{
    /*��ʱ�����Ի�������ӳٵ����Ŀ�¡����*/

    public GameObject dialog;
    public float delayTime;  //��ʱʱ��

    public GameObject[] cloneThing;
    public float[] cloneTime;  //��¡ʱ��

    public GameObject canvas;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        if (dialog != null)
        {
            StartCoroutine(InvokeDialog());
        }
        if (cloneThing != null && cloneTime != null && cloneThing.Length == cloneTime.Length)
        {
            StartCoroutine(CloneWithDelay());
        }
    }

    IEnumerator InvokeDialog()
    {
        yield return new WaitForSeconds(delayTime);
        dialog.GetComponent<Dialog>().StartDialog();
    }

    IEnumerator CloneWithDelay()  //���ο�¡
    {
        for (int i = 0; i < cloneThing.Length; i++)
        {
            yield return new WaitForSeconds(cloneTime[i]);
            Instantiate(cloneThing[i], canvas.transform);
        }
    }
}
