using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersChoice : MonoBehaviour
{
    /*用于存档中几个面板的开关*/

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
