using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersChoice : MonoBehaviour
{
    /*���ڴ浵�м������Ŀ���*/

    public GameObject LoadPannel;

    public void OpenPannel()
    {
        SoundsManager.PlayClick();
        LoadPannel.SetActive(true);
    }
    public void ClosePannel()
    {
        SoundsManager.PlayClick();
        LoadPannel.SetActive(false);
    }
}
