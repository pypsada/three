using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichVisibile : MonoBehaviour
{
    /*�˴������ڸ���ͼ�����ͼƬ��ʾ*/
    /*���ڰ�ť�ϣ���EventTrigger����*/

    public GameObject parentObject;  //�л����������������
    public GameObject childObject;  //Ҫ��ʾ������

    public void Swich()
    {
        // ���ظ������µ�����������
        foreach (Transform child in parentObject.transform)
        {
            child.gameObject.SetActive(false);
        }

        // ��ʾָ����������
        if (childObject != false)
        {
            childObject.SetActive(true);
        }
    }
}
